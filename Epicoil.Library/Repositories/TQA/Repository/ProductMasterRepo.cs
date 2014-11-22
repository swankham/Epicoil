using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.TQA;
using Epicor.Mfg.BO;
using Epicor.Mfg.Core;
using System;
using System.Collections.Generic;
using System.Data;

namespace Epicoil.Library.Repositories.TQA
{
    public class ProductMasterRepo : IProductMasterRepo
    {
        public IEnumerable<ProductsMasterModel> GetAll(string plant, bool waitingflag, bool inactiveflag, bool activeflag, bool rejectflag)
        {   //TODO:
            string whereCluase = "1";
            if (waitingflag == true) { whereCluase += ",6"; }
            if (inactiveflag == true) { whereCluase += ",5"; }
            if (activeflag == true) { whereCluase += ",2"; }
            if (rejectflag == true) { whereCluase += ",3"; }

            string sql = string.Format(@"SELECT ud.*, ext.*, cust.Name as CustomerName, dest.Name as DestinationName, endu.Name as EndUserName, pkg.StyleImg
                                        , cmdt.Character01 AS CommodityName, spec.Character01 AS SpecName, coat.Character01 AS CoatingName,ud35.Key1 as SaleSection, ud35.Character01 as SaleSectionName
                                        FROM UD12 ud
                                        LEFT JOIN ucc_tqa_NorExtension ext ON(ud.Key1 = ext.NorNum)
	                                    LEFT JOIN Customer cust ON(ud.ShortChar04 = cust.CustID)
	                                    LEFT JOIN Customer endu ON(ud.ShortChar05 = endu.CustID)
	                                    LEFT JOIN Customer dest ON(ud.ShortChar06 = dest.CustID)
                                        LEFT JOIN ucc_tqa_PackingStyle pkg ON(ud.ShortChar11 = pkg.CodeNum)
	                                    LEFT JOIN UD29 cmdt ON(ud.ShortChar07 = cmdt.Key1)
	                                    LEFT JOIN UD30 spec ON(ud.ShortChar08 = spec.Key1 AND cmdt.Key1 = spec.Key2)
	                                    LEFT JOIN UD31 coat ON(ud.ShortChar10 = coat.Key1)
                                        LEFT JOIN UserFile uf ON(ud.ShortChar01 = uf.DcdUserID)
                                        LEFT JOIN UD35 ud35 ON(uf.ShortChar01 = ud35.Key1)
                                        WHERE ud.Key2 = 'NOR' AND ud.Number16 IN ({0})
                                        ORDER BY ud.PROGRESS_RECID DESC", whereCluase);

            return Repository.Instance.GetMany<ProductsMasterModel>(sql);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ProductsMasterModel Get(ProductsMasterModel data)
        {
            string sql = string.Format(@"IF NOT EXISTS (
                                        SELECT * FROM ucc_tqa_NorExtension (NOLOCK)
                                        WHERE NorNum = N'{0}'
                                        )
                                    BEGIN
                                        INSERT INTO ucc_tqa_NorExtension
                                            ( NorNum
                                            , Company
                                            , Plant)
                                        VALUES (  N'{0}'
                                            , N'{1}'
                                            , N'{2}'
                                            )
                                    END" + Environment.NewLine
                                            , data.NorNum
                                            , data.Company
                                            , data.Plant
                                            );

            sql += string.Format(@"SELECT ud.*, ext.*, cust.Name as CustomerName, dest.Name as DestinationName, endu.Name as EndUserName, pkg.StyleImg
                                        , cmdt.Character01 AS CommodityName, spec.Character01 AS SpecName, coat.Character01 AS CoatingName,ud35.Key1 as SaleSection, ud35.Character01 as SaleSectionName
                                        FROM UD12 ud
                                        LEFT JOIN ucc_tqa_NorExtension ext ON(ud.Key1 = ext.NorNum)
	                                    LEFT JOIN Customer cust ON(ud.ShortChar04 = cust.CustID)
	                                    LEFT JOIN Customer endu ON(ud.ShortChar05 = endu.CustID)
	                                    LEFT JOIN Customer dest ON(ud.ShortChar06 = dest.CustID)
                                        LEFT JOIN ucc_tqa_PackingStyle pkg ON(ud.ShortChar11 = pkg.CodeNum)
	                                    LEFT JOIN UD29 cmdt ON(ud.ShortChar07 = cmdt.Key1)
	                                    LEFT JOIN UD30 spec ON(ud.ShortChar08 = spec.Key1 AND cmdt.Key1 = spec.Key2)
	                                    LEFT JOIN UD31 coat ON(ud.ShortChar10 = coat.Key1)
                                        LEFT JOIN UserFile uf ON(ud.ShortChar01 = uf.DcdUserID)
                                        LEFT JOIN UD35 ud35 ON(uf.ShortChar01 = ud35.Key1)
                                        WHERE ud.Number16 NOT IN (0)
                                        and ud.Key1 = '{0}'
                                        ORDER BY ud.PROGRESS_RECID DESC", data.NorNum);

            return Repository.Instance.GetOne<ProductsMasterModel>(sql);
        }

        public IEnumerable<ProductsMasterModel> GetByFilter(ProductsMasterModel data)
        {
            throw new NotImplementedException();
        }

        public bool CheckPartExisting(string PartNum)
        {
            string Part;
            string sql = string.Format("SELECT TOP 1 * FROM Part WHERE PartNum = N'{0}'", PartNum);

            Part = Repository.Instance.GetOne<string>(sql, "PartNum");

            return string.IsNullOrEmpty(Part);
        }

        public bool CheckMaterialExisting(string norCode, string matCode)
        {
            string result;
            string sql = string.Format("SELECT TOP 1 * FROM UD02 (NOLOCK) WHERE Key1 = N'{0}' AND ShortChar02 = N'{1}'", norCode, matCode);

            result = Repository.Instance.GetOne<string>(sql, "Key1");

            return string.IsNullOrEmpty(result);
        }

        public bool CheckUsedLine(string norNum)
        {
            string Part;
            string sql = string.Format(@"SELECT ord.* FROM OrderDtl ord
                                                WHERE ord.OpenLine = 1 and ord.PartNum = N'{0}'", norNum);

            Part = Repository.Instance.GetOne<string>(sql, "PartNum");

            //Return 'TRUE' are not using. And can be inactive.
            return string.IsNullOrEmpty(Part);
        }

        public ProductsMasterModel Save(SessionInfo _session, ProductsMasterModel data)
        {
            ProductsMasterModel result = new ProductsMasterModel();

            #region Update Script

            string sql = string.Format(@"UPDATE dbo.UD12
                                           SET [Character03] = N'{1}' --Part Num
                                              ,[Character04] = N'{2}' --Part Name
                                              ,[Character05] = N'{3}' --Part Model
                                              ,[Character06] = N'{4}' --Brand Name
                                              ,[Character08] = N'{5}' --BusinessRoute
                                              ,[Character09] = N'{6}' --Consumption
                                              ,[Number18] = {7} --CoilWeigthMin
                                              ,[Number05] = {8} --CoilWeigthMax
                                              ,[Number06] = {9} --CoilWeigthPackMin
                                              ,[Number07] = {10} --CoilWeigthPackMax
                                              ,[Number08] = {11} --CoilPerPackMin
                                              ,[Number09] = {12} --CoilPerPackMax
                                              ,[Number10] = {13} --CoilID
                                              ,[Number11] = {14} --CoilOD
                                              ,[Number12] = {15} --SheetPackMin
                                              ,[Number13] = {16} --SheetPackMax
                                              ,[Number19] = {17} --FrequencyOfUse
                                              ,[Number20] = {18} --ProductStatus
                                              ,[CheckBox01] = {19} --PartInner
                                              ,[CheckBox02] = {20} --PartOuter
                                              ,[ShortChar02] = N'{21}' --SheetStyleCode
                                              ,[ShortChar11] = N'{22}' --CoilStyleCode
                                              ,[ShortChar12] = N'{23}' --OriginalProcess
                                              ,[ShortChar14] = N'{24}' --Process_
                                              ,[ShortChar15] = N'{25}' --MassProPlan
                                              ,[ShortChar16] = N'{26}' --FinalCoil
                                              ,[ShortChar17] = N'{27}' --OrderPeriodFirst
                                              ,[ShortChar19] = N'{28}' --OrderPeriodSecond
                                              ,[Number16] = {29} --ActiveStatus
                                              ,[CheckBox20] = {30}
                                              ,[Number17] = {31}  --Revision
                                         WHERE Key1 = '{0}' " + Environment.NewLine,
                                               data.NorNum
                                              , data.PartNum
                                              , data.PartName
                                              , data.PartModel
                                              , data.BrandName
                                              , data.BusinessRoute
                                              , data.Consumption
                                              , data.CoilWeigthMin
                                              , data.CoilWeigthMax
                                              , data.CoilWeigthPackMin
                                              , data.CoilWeigthPackMax
                                              , data.CoilPerPackMin
                                              , data.CoilPerPackMax
                                              , data.CoilID
                                              , data.CoilOD
                                              , data.SheetPackMin
                                              , data.SheetPackMax
                                              , data.FrequencyOfUse
                                              , data.ProductStatus
                                              , Convert.ToInt32(data.PartInner)
                                              , Convert.ToInt32(data.PartOuter)
                                              , data.SheetStyleCode
                                              , data.CoilStyleCode
                                              , data.OriginalProcess
                                              , data.Process_
                                              , data.MassProPlan
                                              , data.FinalCoil
                                              , data.OrderPeriodFirst
                                              , data.OrderPeriodSecond
                                              , data.NorStatus
                                              , data.SetStatus
                                              , data.Revision
                                              );

            sql += string.Format(@"UPDATE [dbo].[ucc_tqa_NorExtension]
                                    SET [TolrThickPos] = {1}
                                        ,[TolrThickNeg] = {2}
                                        ,[TolrWidthPos] = {3}
                                        ,[TolrWidthNeg] = {4}
                                        ,[TolrLengthPos] = {5}
                                        ,[TolrLengthNeg] = {6}
                                        ,[FixDirection] = {7}
                                        ,[FixSide] = {8}
                                        ,[BurrCoil] = {9}
                                        ,[BurrSheet] = {10}
                                        ,[EdgeWaveVal] = {11}
                                        ,[EdgeWavePercent] = {12}
                                        ,[Camber] = {13}
                                        ,[BurrTrimmber] = {14}
                                        ,[CenterWaveVal] = {15}
                                        ,[CenterWavePercent] = {16}
                                        ,[Bow] = {17}
                                        ,[Bending] = {18}
                                        ,[Telescope] = {19}
                                        ,[DiffThick] = {20}
                                        ,[Diagonal] = {21}
                                        ,[Overlap] = {22}
                                        ,[Instruction1] = N'{23}'
                                        ,[Instruction2] = N'{24}'
                                        ,[Instruction3] = N'{25}'
                                        ,[Instruction4] = N'{26}'
                                        ,[Instruction5] = N'{27}'
                                        ---,[InstructionImg] = N'{28}'
                                        ---,[InstructionFile] = N'{29}'
                                        ,[InstructionFormat] = N'{30}'
                                        ,[History] = N'{31}'
                                        ,[PackingRow] = {32}
                                        ,[PackingRowSelected] = {33}
                                        ,[PackingColumn] = {34}
                                        ,[PackingID] = {35}
                                        ,[PackingOD] = {36}
                                        ,[NoteCoil] = N'{37}'
                                        ,[NoteSheet] = N'{38}'
                                        ,[KnifeSpecialStatus] = {39}
                                        ,[Clearance] = N'{40}'
                                        ,[FixedKnifeSet] = N'{41}'
                                        ,[PlateName] = N'{42}'
                                    WHERE NorNum = N'{0}'" + Environment.NewLine,
                                        data.NorNum
                                       , data.TolrThickPos
                                       , data.TolrThickNeg
                                       , data.TolrWidthPos
                                       , data.TolrWidthNeg
                                       , data.TolrLengthPos
                                       , data.TolrLengthNeg
                                       , Convert.ToInt32(data.FixDirection)
                                       , data.FixSide.GetInt()
                                       , data.BurrCoil
                                       , data.BurrSheet
                                       , data.EdgeWaveVal
                                       , data.EdgeWavePercent
                                       , data.Camber
                                       , data.BurrTrimmber
                                       , data.CenterWaveVal
                                       , data.CenterWavePercent
                                       , data.Bow
                                       , data.Bending
                                       , data.Telescope
                                       , data.DiffThick
                                       , data.Diagonal
                                       , data.Overlap
                                       , data.Instruction1
                                       , data.Instruction2
                                       , data.Instruction3
                                       , data.Instruction4
                                       , data.Instruction5
                                       , data.InstructionImgPath
                                       , "" //data.InstructionImgFile
                                       , data.InstructionFormat
                                       , data.History
                                       , data.PackingRow
                                       , Convert.ToInt32(data.PackingRowSelected)
                                       , data.PackingColumn
                                       , data.PackingID
                                       , data.PackingOD
                                       , data.NoteCoil
                                       , data.NoteSheet
                                       , Convert.ToInt32(data.KnifeSpecialStatus)
                                       , data.Clearance
                                       , data.FixedKnifeSet
                                       , data.PlateName);

            if (!string.IsNullOrEmpty(data.InstructionImgPath))
            {
                sql += string.Format(@"UPDATE [dbo].[ucc_tqa_NorExtension]
                                    SET [InstructionImg] = N'{1}'
                                    WHERE NorNum = N'{0}'" + Environment.NewLine,
                             data.NorNum
                           , data.InstructionImgPath);
            }

            if (!string.IsNullOrEmpty(data.InstructionFilePath))
            {
                sql += string.Format(@"UPDATE [dbo].[ucc_tqa_NorExtension]
                                    SET [InstructionFile] = N'{1}'
                                    WHERE NorNum = N'{0}'" + Environment.NewLine,
                             data.NorNum
                           , data.InstructionFilePath);
            }

            #endregion Update Script

            Repository.Instance.ExecuteWithTransaction(sql, "Save");
            if (data.NorStatus == 2)
            {
                if (CheckPartExisting(data.NorNum))
                {
                    bool isSucc;
                    string msgErr;
                    isSucc = NewPart(_session, data, out isSucc, out msgErr);
                }
            }

            result = this.Get(data);
            return result;
        }

        public bool NewPart(SessionInfo _session, ProductsMasterModel model, out bool IsSucces, out string msgError)
        {
            //int iRunning = RunningPart();
            //string PartNum = GetSerialByFormat(iRunning);
            try
            {
                ///TODO: Fix Epicor AppServer to workaround.
                Session currSession = new Session(_session.UserID, _session.UserPassword, _session.AppServer, Session.LicenseType.Default);
                Part myPart = new Part(currSession.ConnectionPool);

                PartDataSet dsPart = new PartDataSet();
                myPart.GetNewPart(dsPart);

                DataRow drPart = dsPart.Tables[0].Rows[0];
                drPart.BeginEdit();
                drPart["PartNum"] = model.NorNum;
                drPart["PartDescription"] = model.NorNum;
                drPart["UOMClassID"] = "UCC";
                drPart["IUM"] = (model.SizeLength > 0) ? "PCS" : "KG";  //Our UOM
                drPart["PUM"] = (model.SizeLength > 0) ? "PCS" : "KG"; ;   //Purchasing UOM
                drPart["TypeCode"] = "M";
                drPart["SalesUM"] = (model.SizeLength > 0) ? "PCS" : "KG"; ;   //Sale UOM
                drPart["ShortChar05"] = "M";
                drPart["Number01"] = model.SizeThick;
                drPart["Number02"] = model.SizeWidth;
                drPart["Number03"] = model.SizeLength;
                drPart["Number11"] = 1;
                drPart["Number12"] = 1;
                drPart["Character10"] = "N";

                drPart["NetWeight"] = model.CoilWeigthMin;
                drPart.EndEdit();
                myPart.Update(dsPart);

                currSession.Dispose();

                IsSucces = true;
                msgError = "";
            }
            catch (Exception ex)
            {
                IsSucces = false;
                msgError = ex.Message;
                return false;
            }
            return true;
        }
    }
}