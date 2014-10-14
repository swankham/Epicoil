using Epicor.Mfg.BO;
using Epicor.Mfg.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Epicoil.Library.Models;
using Epicoil.Library.Models.TQA;
using Epicoil.Library.Frameworks;

namespace Epicoil.Library.Repositories.TQA
{
    public class McssRepo : IMcssRepo
    {
        public IEnumerable<MCSS> GetAll(string plant)
        {
            string sql = string.Format(@"SELECT
                            ud15.Key5 as Plant,
                            ud15.ShortChar08, --MCSSID
                            ud15.Key1 as McssNum,
                            ud15.Date01,
                            ud15.Number13 as Revision,
                            ud15.ShortChar03 as SupplierCode,
                            ven.Name as SupplierName,
                            ud15.ShortChar04 as CustID,
                            cust.Name as CustomerName,
                            ud15.ShortChar01 as MakerCode,
                            maker.Character01 as MakerName,
                            ud15.ShortChar02 as MillCode,
                            mill.Character01 as MillName,
                            ud15.ShortChar06 as CommodityCode, --CommodityCode
                            cdt.Character01 AS CommodityName, --CommodityName
                            cdt.CheckBox01 AS RequireCoating,
                            ud15.ShortChar11 as MatSpec1, --SpecCode
                            spec.Character01 as MatSpec2,
                            ud15.ShortChar09 as Coating1,
                            coat.Character01 as Coating2,
                            ud15.Number04 as CoatingWeight1,
                            ud15.Number05 as CoatingWeight2,
                            ud15.ShortChar05 as CategoryHedGroup1,
                            catg.Character01 as CategoryHedGroup2,
                            ud15.Number10 as Procession,
                            ud15.Number06 as POAllowance,
                            ud15.Number01 as Thick,
                            ud15.Number02 as Width,
                            ud15.Number03 as Length,
                            ud15.CheckBox01 as TISIFlag,
                            ud15.Character04 as TISINo,
                            ud15.Character05 as LicenseNo,

                            NULLIF(ud15.ShortChar10,'') as CustomerType,
                            ud15.ShortChar07 as CustomerTypeRemark,
                            ud15.ShortChar13 as BussinessType,
                            busi.Character01 as BussinessTypeName,
                            ud15.Number07 as QuantityPerMonth,
                            ud15.Number15 as QuantityPerPlant,
                            ud15.Character08 as BusinessRoute,
		                    ud15.Character09 as BusinessRemark,

                            ud15.Number08 as StandardRef,
                            ud15.ShortChar14 as StandardRefRemark,
                            ud15.ShortChar15 as Number,
                            ud15.Character03 as Name,
                            ud15.Number09 as ThicknessTolerance,
                            ud15.Number17 as ThicknessTolerValPos,
                            ud15.Number18 as ThicknessTolerValNeg,
                            ud15.Number11 as WidthStandard,
                            ud15.Number19 as WidthStdPos,
                            ud15.Number20 as WidthStdNeg,
                            ud15.Number12 as Oilling,
                            NULLIF(ud15.Character07,'') as OillingVal,
                            ud34.Character02 as BaseMaterial,
                            ud34.ShortChar19 as Country,
                            ud15.Character10 as Remark,

                            ud34.Number20 as DistCR,
                            ud34.ShortChar01 as DistCRRemark,
                            ud34.Number19 as DistHR,
                            ud34.ShortChar02 as DistHRRemark,
                            ud34.Number18 as DistGI,
                            ud34.ShortChar03 as DistGIRemark,
                            ud34.Number17 as Stainless,
                            ud34.ShortChar04 as StainlessRemark,
                            ud34.Number01 as ChemPersent,
                            ud34.Character07 as ChemPersentRemark,
                            ud34.Number02 as Yield,
                            ud34.ShortChar06 as YieldRemark,
                            ud34.Number03 as Tensile,
                            ud34.ShortChar07 as TensileRemark,
                            ud34.Number04 as Elongation,
                            ud34.ShortChar08 as ElongationRemark,
                            NULLIF(ud34.ShortChar05, '') as Hardness,
                            ud34.ShortChar09 as HardnessRemark,
                            ud34.Number06 as CoreLoss,
                            ud34.ShortChar10 as CoreLossRemark,
                            ud34.Number07 as Magnatic,
                            ud34.ShortChar11 as MagnaticRemark,
                            ud34.Number08 as Oriented,
                            null as OrientedRemark,

                            ud34.CheckBox01 as Welding,
                            ud34.CheckBox02 as Painting,
                            ud34.CheckBox05 as ProcessOther,
                            ud34.ShortChar12 as ProcessOtherRemark,
                            ud34.CheckBox03 as Degreasing,
                            ud34.CheckBox04 as Blanking,
                            ud34.CheckBox06 as Commercial,
                            ud34.CheckBox07 as Drawing,
                            ud34.CheckBox08 as DeepDrawing,
                            ud34.CheckBox09 as ExtraDeep,
                            ud34.CheckBox10 as Folding,
                            ud34.CheckBox11 as FormingOther,
                            ud34.ShortChar13 as FormingOtherRemark,
                            ud34.ShortChar14 as PartName,
                            ud34.ShortChar15 as ProductName,
                            ud34.ShortChar16 as CusProcessRemark,
                            ud34.ShortChar17 as EndUser1,
                            ud34.ShortChar18 as EndUser2,
                            ud34.ShortChar19 as EndUser3,
                            ud34.ShortChar20 as EndUser4,
                            ud34.Character10 as CategoryGroup1,
                            ud34.Character09 as CategoryGroup2,
                            ud34.Character08 as CategoryGroup3,
                            ud34.Character07 as CategoryGroup4,

                            ud34.Number12 as EdgeWave,
                            ud34.Character04 as EdgeWaveRemark,
                            ud34.Number16 as CenterWave,
                            ud34.Character03 as CenterWaveRemark,
                            ud34.Number15 as PackingStyle,
                            ud34.Number13 as WeightPerCoilMin,
                            ud34.Number14 as WeightPerCoilMax,
                            NULLIF(ud34.Character01, '') as IDCoil,
                            NULLIF(ud34.Character06, '') as ODCoil,
                            ud34.CheckBox12 as RoHS,
                            ud34.CheckBox13 as PFOS,
                            ud34.CheckBox14 as SOC,
                            ud34.CheckBox15 as ELV,
                            ud34.CheckBox16 as REACH,
                            ud34.CheckBox17 as Other,
                            ud34.Character05 as OtherRemark
                    FROM UD15 ud15
	                    LEFT JOIN UD34 ud34 ON(ud15.Key1 = ud34.Key1)
	                    LEFT JOIN Vendor ven ON(ud15.ShortChar03 = ven.VendorID)
	                    LEFT JOIN Customer cust ON(ud15.ShortChar04 = cust.CustID)
	                    LEFT JOIN UD19 maker ON(ud15.ShortChar01 = maker.Key1)
	                    LEFT JOIN UD14 mill ON(ud15.ShortChar01 = mill.Key1 and ud15.ShortChar02 = mill.Key2)
	                    LEFT JOIN UD30 spec ON(ud15.ShortChar06 = spec.Key2 and ud15.ShortChar11 = spec.Key1)
	                    LEFT JOIN UD31 coat ON(ud15.ShortChar09 = coat.Key1)
	                    LEFT JOIN UD22 catg ON(ud15.ShortChar05 = catg.Key1)
	                    LEFT JOIN UD25 busi ON(ud15.ShortChar13 = busi.Key1)
                        LEFT JOIN UD29 cdt ON(ud15.ShortChar06 = cdt.Key1)
                    ORDER BY ud15.Key1 ASC");

            return Repository.Instance.GetMany<MCSS>(sql);
        }

        public IEnumerable<MCSS> GetByFilter(DateTime DateFrom, DateTime DateTo, MCSS model, bool TISIFlag)
        {
            IEnumerable<MCSS> query = GetAll(model.Plant);

            if (model.Thick == 0)
            {
                query = query.Where(p => p.RequestDate.Date >= DateFrom.Date.Date && p.RequestDate.Date <= DateTo.Date.Date);
            }

            if (model.McssNum != null) { query = query.Where(p => p.McssNum.Contains(model.McssNum)); }
            if (TISIFlag == true) { query = query.Where(p => p.TISIFlag.Equals(true)); }
            if (model.Thick != 0) { query = query.Where(p => p.Thick.Equals(model.Thick)); }
            if (model.Width != 0) { query = query.Where(p => p.Width.Equals(model.Width)); }
            if (model.Length != 0) { query = query.Where(p => p.Length.Equals(model.Length)); }

            return query.ToList();
        }

        public MCSS Get(string plant, string McssNum)
        {
            string sql = string.Format(@"SELECT
                            ud15.Key5 as Plant,
                            ud15.ShortChar08, --MCSSID
                            ud15.Key1 as McssNum,
                            ud15.Date01,
                            ud15.Number13 as Revision,
                            ud15.ShortChar03 as SupplierCode,
                            ven.Name as SupplierName,
                            ud15.ShortChar04 as CustID,
                            cust.Name as CustomerName,
                            ud15.ShortChar01 as MakerCode,
                            maker.Character01 as MakerName,
                            ud15.ShortChar02 as MillCode,
                            mill.Character01 as MillName,
                            ud15.ShortChar06 as CommodityCode, --CommodityCode
                            cdt.Character01 AS CommodityName, --CommodityName
                            cdt.CheckBox01 AS RequireCoating,
                            ud15.ShortChar11 as MatSpec1, --SpecCode
                            spec.Character01 as MatSpec2,
                            ud15.ShortChar09 as Coating1,
                            coat.Character01 as Coating2,
                            ud15.Number04 as CoatingWeight1,
                            ud15.Number05 as CoatingWeight2,
                            ud15.ShortChar05 as CategoryHedGroup1,
                            catg.Character01 as CategoryHedGroup2,
                            ud15.Number10 as Procession,
                            ud15.Number06 as POAllowance,
                            ud15.Number01 as Thick,
                            ud15.Number02 as Width,
                            ud15.Number03 as Length,
                            ud15.CheckBox01 as TISIFlag,
                            ud15.Character04 as TISINo,
                            ud15.Character05 as LicenseNo,

                            NULLIF(ud15.ShortChar10,'') as CustomerType,
                            ud15.ShortChar07 as CustomerTypeRemark,
                            ud15.ShortChar13 as BussinessType,
                            busi.Character01 as BussinessTypeName,
                            ud15.Number07 as QuantityPerMonth,
                            ud15.Number15 as QuantityPerPlant,
                            ud15.Character08 as BusinessRoute,
		                    ud15.Character09 as BusinessRemark,

                            ud15.Number08 as StandardRef,
                            ud15.ShortChar14 as StandardRefRemark,
                            ud15.ShortChar15 as Number,
                            ud15.Character03 as Name,
                            ud15.Number09 as ThicknessTolerance,
                            ud15.Number17 as ThicknessTolerValPos,
                            ud15.Number18 as ThicknessTolerValNeg,
                            ud15.Number11 as WidthStandard,
                            ud15.Number19 as WidthStdPos,
                            ud15.Number20 as WidthStdNeg,
                            ud15.Number12 as Oilling,
                            NULLIF(ud15.Character07,'') as OillingVal,
                            ud34.Character02 as BaseMaterial,
                            ud34.ShortChar19 as Country,
                            ud15.Character10 as Remark,

                            ud34.Number20 as DistCR,
                            ud34.ShortChar01 as DistCRRemark,
                            ud34.Number19 as DistHR,
                            ud34.ShortChar02 as DistHRRemark,
                            ud34.Number18 as DistGI,
                            ud34.ShortChar03 as DistGIRemark,
                            ud34.Number17 as Stainless,
                            ud34.ShortChar04 as StainlessRemark,
                            ud34.Number01 as ChemPersent,
                            ud34.Character07 as ChemPersentRemark,
                            ud34.Number02 as Yield,
                            ud34.ShortChar06 as YieldRemark,
                            ud34.Number03 as Tensile,
                            ud34.ShortChar07 as TensileRemark,
                            ud34.Number04 as Elongation,
                            ud34.ShortChar08 as ElongationRemark,
                            NULLIF(ud34.ShortChar05, '') as Hardness,
                            ud34.ShortChar09 as HardnessRemark,
                            ud34.Number06 as CoreLoss,
                            ud34.ShortChar10 as CoreLossRemark,
                            ud34.Number07 as Magnatic,
                            ud34.ShortChar11 as MagnaticRemark,
                            ud34.Number08 as Oriented,
                            null as OrientedRemark,

                            ud34.CheckBox01 as Welding,
                            ud34.CheckBox02 as Painting,
                            ud34.CheckBox05 as ProcessOther,
                            ud34.ShortChar12 as ProcessOtherRemark,
                            ud34.CheckBox03 as Degreasing,
                            ud34.CheckBox04 as Blanking,
                            ud34.CheckBox06 as Commercial,
                            ud34.CheckBox07 as Drawing,
                            ud34.CheckBox08 as DeepDrawing,
                            ud34.CheckBox09 as ExtraDeep,
                            ud34.CheckBox10 as Folding,
                            ud34.CheckBox11 as FormingOther,
                            ud34.ShortChar13 as FormingOtherRemark,
                            ud34.ShortChar14 as PartName,
                            ud34.ShortChar15 as ProductName,
                            ud34.ShortChar16 as CusProcessRemark,
                            ud34.ShortChar17 as EndUser1,
                            ud34.ShortChar18 as EndUser2,
                            ud34.ShortChar19 as EndUser3,
                            ud34.ShortChar20 as EndUser4,
                            ud34.Character10 as CategoryGroup1,
                            ud34.Character09 as CategoryGroup2,
                            ud34.Character08 as CategoryGroup3,
                            '' as CategoryGroup4,

                            ud34.Number12 as EdgeWave,
                            ud34.Character04 as EdgeWaveRemark,
                            ud34.Number16 as CenterWave,
                            ud34.Character03 as CenterWaveRemark,
                            ud34.Number15 as PackingStyle,
                            ud34.Number13 as WeightPerCoilMin,
                            ud34.Number14 as WeightPerCoilMax,
                            NULLIF(ud34.Character01, '') as IDCoil,
                            NULLIF(ud34.Character06, '') as ODCoil,
                            ud34.CheckBox12 as RoHS,
                            ud34.CheckBox13 as PFOS,
                            ud34.CheckBox14 as SOC,
                            ud34.CheckBox15 as ELV,
                            ud34.CheckBox16 as REACH,
                            ud34.CheckBox17 as Other,
                            ud34.Character05 as OtherRemark
                    FROM UD15 ud15
	                    LEFT JOIN UD34 ud34 ON(ud15.Key1 = ud34.Key1)
	                    LEFT JOIN Vendor ven ON(ud15.ShortChar03 = ven.VendorID)
	                    LEFT JOIN Customer cust ON(ud15.ShortChar04 = cust.CustID)
	                    LEFT JOIN UD19 maker ON(ud15.ShortChar01 = maker.Key1)
	                    LEFT JOIN UD14 mill ON(ud15.ShortChar01 = mill.Key1 and ud15.ShortChar02 = mill.Key2)
	                    LEFT JOIN UD30 spec ON(ud15.ShortChar06 = spec.Key2 and ud15.ShortChar11 = spec.Key1)
	                    LEFT JOIN UD31 coat ON(ud15.ShortChar09 = coat.Key1)
	                    LEFT JOIN UD22 catg ON(ud15.ShortChar05 = catg.Key1)
	                    LEFT JOIN UD25 busi ON(ud15.ShortChar13 = busi.Key1)
                        LEFT JOIN UD29 cdt ON(ud15.ShortChar06 = cdt.Key1)
                    WHERE ud15.Key1 = '{0}'
                    ORDER BY ud15.Key1 ASC", McssNum);

            var result = Repository.Instance.GetOne<MCSS>(sql);
            if (result != null)
            {
                result.SpecialRefs = this.GetSpecialRefByMCSS(result.McssNum);
            }

            return result;
        }

        public int GenerateMcssID()
        {
            string sql = @" SELECT TOP 1 * FROM UD15 ORDER BY CAST(ShortChar08 AS INT) DESC";

            var MCSSID = Repository.Instance.GetOne<string>(sql, "ShortChar08");

            return Convert.ToInt32(MCSSID) + 1;
        }

        private string GetMCSSNum(string McssID)
        {
            string sql = string.Format(@" SELECT TOP 1 * FROM UD15 WHERE ShortChar08 = N'{0}'", McssID);

            return Repository.Instance.GetOne<string>(sql, "Key1");
        }

        public MCSS Save(MCSS model, Models.SessionInfo epiSession, out bool IsSucces, out string msgError)
        {
            MCSS result = new MCSS();
            bool ud15Success = false;
            string ud15msg = "";

            bool ud34Success = false;
            string ud34msg = "";

            if (model.InsertState == true) { model.MCSSID = GenerateMcssID().ToString(); }
            model.RequestDate = DateTime.Now;
            SaveUD15(model, epiSession, out ud15Success, out ud15msg);

            if (ud15Success)
            {
                model.McssNum = GetMCSSNum(model.MCSSID);
                SaveUD34(model, epiSession, out ud34Success, out ud34msg);
            }

            //if (model.InsertState == true) { GetNewPart(model, epiSession, out ud15Success, out ud15msg); }
            GetNewPart(model, epiSession, out ud15Success, out ud15msg);
            IsSucces = ud15Success && ud34Success;
            msgError = "Message UD34 : " + ud15msg + " Message UD34 : " + ud34msg;

            if (IsSucces)
            {
                result = this.Get(epiSession.PlantID, model.McssNum);
            }
            else { result = null; }

            return result;
        }

        public void SaveUD15(MCSS model, Models.SessionInfo epiSession, out bool IsSucces, out string msgError)
        {
            try
            {
                if (model.InsertState == true)
                {
                    Session currSession = new Session(epiSession.UserID, epiSession.UserPassword, epiSession.AppServer, Session.LicenseType.Default);
                    UD15 myUD15 = new UD15(currSession.ConnectionPool);

                    UD15DataSet dsUD15 = new UD15DataSet();
                    myUD15.GetaNewUD15(dsUD15);

                    DataRow drUD15 = dsUD15.Tables[0].Rows[0];
                    drUD15.BeginEdit();
                    drUD15["Key1"] = "1";
                    //drUD15["Key2"] = "1";
                    drUD15["ShortChar08"] = model.MCSSID;
                    drUD15["ShortChar20"] = epiSession.UserID;
                    drUD15["Key5"] = epiSession.PlantID;
                    drUD15["Character01"] = String.IsNullOrEmpty(model.BussinessTypeName) ? "" : model.BussinessTypeName;
                    drUD15["Character02"] = "";
                    drUD15["Character03"] = string.IsNullOrEmpty(model.Name) ? "" : model.Name;
                    drUD15["Character04"] = string.IsNullOrEmpty(model.TISINo) ? "" : model.TISINo;
                    drUD15["Character05"] = string.IsNullOrEmpty(model.LicenseNo) ? "" : model.LicenseNo;
                    drUD15["Character06"] = "";
                    drUD15["Character07"] = model.OillingVal.GetDecimal();
                    drUD15["Character08"] = string.IsNullOrEmpty(model.BusinessRoute) ? "" : model.BusinessRoute;
                    drUD15["Character09"] = string.IsNullOrEmpty(model.BusinessRemark) ? "" : model.BusinessRemark;
                    drUD15["Character10"] = string.IsNullOrEmpty(model.Remark) ? "" : model.Remark;
                    drUD15["Number01"] = model.Thick.GetDecimal();
                    drUD15["Number02"] = model.Width.GetDecimal();
                    drUD15["Number03"] = model.Length.GetDecimal();
                    drUD15["Number04"] = model.CoatingWeight1.GetDecimal();
                    drUD15["Number05"] = model.CoatingWeight2.GetDecimal();
                    drUD15["Number06"] = model.POAllowance.GetDecimal();
                    drUD15["Number07"] = model.QuantityPerMonth.GetDecimal();
                    drUD15["Number08"] = model.StandardRef.GetInt();
                    drUD15["Number09"] = model.ThicknessTolerance.GetInt();
                    drUD15["Number10"] = model.Pocession.GetInt();
                    drUD15["Number11"] = model.WidthStandard.GetInt();
                    drUD15["Number12"] = model.Oilling.GetInt();
                    drUD15["Number15"] = model.QuantityPerPlant.GetDecimal();
                    drUD15["Number17"] = model.ThicknessTolerValPos.GetDecimal();
                    drUD15["Number18"] = model.ThicknessTolerValNeg.GetDecimal();
                    drUD15["Number19"] = model.WidthStdPos.GetDecimal();
                    drUD15["Number20"] = model.WidthStdNeg.GetDecimal();
                    drUD15["Date01"] = DateTime.Now.ToLongDateString();
                    drUD15["CheckBox01"] = Convert.ToInt32(model.TISIFlag);
                    drUD15["ShortChar01"] = string.IsNullOrEmpty(model.MakerCode) ? "" : model.MakerCode;
                    drUD15["ShortChar02"] = string.IsNullOrEmpty(model.MillCode) ? "" : model.MillCode;
                    drUD15["ShortChar03"] = string.IsNullOrEmpty(model.SupplierCode) ? "" : model.SupplierCode;
                    drUD15["ShortChar04"] = string.IsNullOrEmpty(model.CustID) ? "" : model.CustID;
                    drUD15["ShortChar05"] = string.IsNullOrEmpty(model.CategoryGroupHead1) ? "" : model.CategoryGroupHead1;
                    drUD15["ShortChar06"] = string.IsNullOrEmpty(model.CommodityCode) ? "" : model.CommodityCode;
                    drUD15["ShortChar07"] = string.IsNullOrEmpty(model.CustomerTypeRemark) ? "" : model.CustomerTypeRemark;
                    drUD15["ShortChar09"] = string.IsNullOrEmpty(model.Coating1) ? "" : model.Coating1;
                    drUD15["ShortChar10"] = string.IsNullOrEmpty(model.CustomerType.ToString()) ? "" : model.CustomerType.ToString();
                    drUD15["ShortChar11"] = string.IsNullOrEmpty(model.MatSpec1) ? "" : model.MatSpec1;
                    drUD15["ShortChar13"] = string.IsNullOrEmpty(model.BussinessType) ? "" : model.BussinessType;
                    drUD15["ShortChar14"] = string.IsNullOrEmpty(model.StandardRefRemark) ? "" : model.StandardRefRemark;
                    drUD15["ShortChar15"] = string.IsNullOrEmpty(model.Number) ? "" : model.Number;
                    drUD15.EndEdit();
                    myUD15.Update(dsUD15);
                    currSession.Dispose();
                }
                else if (model.InsertState == false)
                {
                    string sql = string.Format(@"UPDATE UD15
                                                   SET Company = N'{0}'  --<Company, nvarchar(8),>
                                                      ,Key1 = N'{1}'  --<Key1, nvarchar(50),>
                                                      ,Character01 = N'{2}'  --<Character01, nvarchar(max),>
                                                      ,Character02 = N'{3}'  --<Character02, nvarchar(max),>
                                                      ,Character03 = N'{4}'  --<Character03, nvarchar(max),>
                                                      ,Character04 = N'{5}'  --<Character04, nvarchar(max),>
                                                      ,Character05 = N'{6}'  --<Character05, nvarchar(max),>
                                                      ,Character07 = N'{7}'  --<Character07, nvarchar(max),>
                                                      ,Character08 = N'{8}'  --<Character08, nvarchar(max),>
                                                      ,Character09 = N'{9}'  --<Character09, nvarchar(max),>
                                                      ,Character10 = N'{10}'  --<Character10, nvarchar(max),>
                                                      ,Number01 = {11}  --<Number01, decimal(20,9),>
                                                      ,Number02 = {12}  --<Number02, decimal(20,9),>
                                                      ,Number03 = {13}  --<Number03, decimal(20,9),>
                                                      ,Number04 = {14}  --<Number04, decimal(20,9),>
                                                      ,Number05 = {15}  --<Number05, decimal(20,9),>
                                                      ,Number06 = {16}  --<Number06, decimal(20,9),>
                                                      ,Number07 = {17}  --<Number07, decimal(20,9),>
                                                      ,Number08 = {18}  --<Number08, decimal(20,9),>
                                                      ,Number09 = {19}  --<Number09, decimal(20,9),>
                                                      ,Number10 = {20}  --<Number10, decimal(20,9),>
                                                      ,Number11 = {21}  --<Number11, decimal(20,9),>
                                                      ,Number12 = {22}  --<Number12, decimal(20,9),>
                                                      ,Number15 = {23}  --<Number15, decimal(20,9),>
                                                      ,Number17 = {24}  --<Number17, decimal(20,9),>
                                                      ,Number18 = {25}  --<Number18, decimal(20,9),>
                                                      ,Number19 = {26}  --<Number19, decimal(20,9),>
                                                      ,Number20 = {27}  --<Number20, decimal(20,9),>
                                                      ,CheckBox01 = {28}  --<CheckBox01, tinyint,>
                                                      ,ShortChar01 = N'{29}'  --<ShortChar01, nvarchar(50),>
                                                      ,ShortChar02 = N'{30}'  --<ShortChar02, nvarchar(50),>
                                                      ,ShortChar03 = N'{31}'  --<ShortChar03, nvarchar(50),>
                                                      ,ShortChar04 = N'{32}'  --<ShortChar04, nvarchar(50),>
                                                      ,ShortChar05 = N'{33}'  --<ShortChar05, nvarchar(50),>
                                                      ,ShortChar06 = N'{34}'  --<ShortChar06, nvarchar(50),>
                                                      ,ShortChar07 = N'{35}'  --<ShortChar07, nvarchar(50),>
                                                      ,ShortChar09 = N'{36}'  --<ShortChar09, nvarchar(50),>
                                                      ,ShortChar10 = N'{37}'  --<ShortChar10, nvarchar(50),>
                                                      ,ShortChar11 = N'{38}'  --<ShortChar11, nvarchar(50),>
                                                      ,ShortChar13 = N'{39}'  --<ShortChar13, nvarchar(50),>
                                                      ,ShortChar14 = N'{40}'  --<ShortChar14, nvarchar(50),>
                                                      ,ShortChar15 = N'{41}'  --<ShortChar15, nvarchar(50),>
                                                 WHERE Key1 = N'{1}'" + Environment.NewLine,
                                                      epiSession.CompanyID
                                                      , model.McssNum
                                                      , String.IsNullOrEmpty(model.BussinessTypeName) ? "" : model.BussinessTypeName
                                                      , ""
                                                      , string.IsNullOrEmpty(model.Name) ? "" : model.Name
                                                      , string.IsNullOrEmpty(model.TISINo) ? "" : model.TISINo
                                                      , string.IsNullOrEmpty(model.LicenseNo) ? "" : model.LicenseNo
                                                      , model.OillingVal.GetDecimal()
                                                      , string.IsNullOrEmpty(model.BusinessRoute) ? "" : model.BusinessRoute
                                                      , string.IsNullOrEmpty(model.BusinessRemark) ? "" : model.BusinessRemark
                                                      , string.IsNullOrEmpty(model.Remark) ? "" : model.Remark
                                                      , model.Thick.GetDecimal()
                                                      , model.Width.GetDecimal()
                                                      , model.Length.GetDecimal()
                                                      , model.CoatingWeight1.GetDecimal()
                                                      , model.CoatingWeight2.GetDecimal()           //15
                                                      , model.POAllowance.GetDecimal()
                                                      , model.QuantityPerMonth.GetDecimal()
                                                      , model.StandardRef.GetInt()
                                                      , model.ThicknessTolerance.GetInt()
                                                      , model.Pocession.GetInt()
                                                      , model.WidthStandard.GetInt()
                                                      , model.Oilling.GetInt()
                                                      , model.QuantityPerPlant.GetDecimal()
                                                      , model.ThicknessTolerValPos.GetDecimal()
                                                      , model.ThicknessTolerValNeg.GetDecimal()
                                                      , model.WidthStdPos.GetDecimal()
                                                      , model.WidthStdNeg.GetDecimal()
                                                      , Convert.ToInt32(model.TISIFlag)
                                                      , string.IsNullOrEmpty(model.MakerCode) ? "" : model.MakerCode
                                                      , string.IsNullOrEmpty(model.MillCode) ? "" : model.MillCode          //30
                                                      , string.IsNullOrEmpty(model.SupplierCode) ? "" : model.SupplierCode
                                                      , string.IsNullOrEmpty(model.CustID) ? "" : model.CustID
                                                      , string.IsNullOrEmpty(model.CategoryGroupHead1) ? "" : model.CategoryGroupHead1
                                                      , string.IsNullOrEmpty(model.CommodityCode) ? "" : model.CommodityCode
                                                      , string.IsNullOrEmpty(model.CustomerTypeRemark) ? "" : model.CustomerTypeRemark
                                                      , string.IsNullOrEmpty(model.Coating1) ? "" : model.Coating1
                                                      , string.IsNullOrEmpty(model.CustomerType.ToString()) ? "" : model.CustomerType.ToString()
                                                      , string.IsNullOrEmpty(model.MatSpec1) ? "" : model.MatSpec1
                                                      , string.IsNullOrEmpty(model.BussinessType) ? "" : model.BussinessType
                                                      , string.IsNullOrEmpty(model.StandardRefRemark) ? "" : model.StandardRefRemark
                                                      , string.IsNullOrEmpty(model.Number) ? "" : model.Number);

                    Repository.Instance.ExecuteWithTransaction(sql, "Update MCSS UD15");
                }

                IsSucces = true;
                msgError = "";
            }
            catch (Exception ex)
            {
                IsSucces = false;
                msgError = ex.Message;
            }
        }

        public void SaveUD34(MCSS model, Models.SessionInfo epiSession, out bool IsSucces, out string msgError)
        {
            try
            {
                if (model.InsertState == true)
                {
                    Session currSession = new Session(epiSession.UserID, epiSession.UserPassword, epiSession.AppServer, Session.LicenseType.Default);
                    UD34 myUD34 = new UD34(currSession.ConnectionPool);

                    UD34DataSet dsUD34 = new UD34DataSet();
                    myUD34.GetaNewUD34(dsUD34);

                    DataRow drUD34 = dsUD34.Tables[0].Rows[0];
                    drUD34.BeginEdit();
                    drUD34["Key1"] = model.McssNum;
                    drUD34["ShortChar08"] = model.MCSSID;
                    //drUD34["Key4"] = epiSession.UserID;
                    drUD34["Key5"] = epiSession.PlantID;
                    drUD34["Character01"] = String.IsNullOrEmpty(model.IDCoil.ToString()) ? "" : model.IDCoil.ToString();
                    drUD34["Character02"] = String.IsNullOrEmpty(model.BaseMaterial) ? "" : model.BaseMaterial;
                    drUD34["Character03"] = String.IsNullOrEmpty(model.CenterWaveRemark) ? "" : model.CenterWaveRemark;
                    drUD34["Character04"] = String.IsNullOrEmpty(model.EdgeWaveRemark) ? "" : model.EdgeWaveRemark;
                    drUD34["Character05"] = String.IsNullOrEmpty(model.OtherRemark) ? "" : model.OtherRemark;
                    drUD34["Character06"] = String.IsNullOrEmpty(model.ODCoil.ToString()) ? "" : model.ODCoil.ToString(); ;
                    drUD34["Character07"] = String.IsNullOrEmpty(model.ChemPersentRemark) ? "" : model.ChemPersentRemark; ;
                    drUD34["Character08"] = String.IsNullOrEmpty(model.CategoryGroup3) ? "" : model.CategoryGroup3;
                    drUD34["Character09"] = String.IsNullOrEmpty(model.CategoryGroup2) ? "" : model.CategoryGroup2;
                    drUD34["Character10"] = String.IsNullOrEmpty(model.CategoryGroup1) ? "" : model.CategoryGroup1;
                    drUD34["Number01"] = model.ChemPersent.GetInt();
                    drUD34["Number02"] = model.Yield.GetInt();
                    drUD34["Number03"] = model.Tensile.GetInt();
                    drUD34["Number04"] = model.Elongation.GetInt();
                    drUD34["Number06"] = model.CoreLoss.GetInt();
                    drUD34["Number07"] = model.Magnatic.GetInt();
                    drUD34["Number08"] = model.Oriented.GetInt();
                    drUD34["Number12"] = model.EdgeWave.GetDecimal();
                    drUD34["Number13"] = model.WeightPerCoilMin.GetDecimal();
                    drUD34["Number14"] = model.WeightPerCoilMax.GetDecimal();
                    drUD34["Number15"] = model.PackingStyle.GetInt();
                    drUD34["Number16"] = model.CenterWave.GetInt();
                    drUD34["Number17"] = model.Stainless.GetInt();
                    drUD34["Number18"] = model.DistGI.GetInt();
                    drUD34["Number19"] = model.DistHR.GetInt();
                    drUD34["Number20"] = model.DistCR.GetInt();
                    drUD34["CheckBox01"] = Convert.ToInt32(model.Welding);
                    drUD34["CheckBox02"] = Convert.ToInt32(model.Painting);
                    drUD34["CheckBox03"] = Convert.ToInt32(model.Degreasing);
                    drUD34["CheckBox04"] = Convert.ToInt32(model.Blanking);
                    drUD34["CheckBox05"] = Convert.ToInt32(model.ProcessOther);
                    drUD34["CheckBox06"] = Convert.ToInt32(model.Commercial);
                    drUD34["CheckBox07"] = Convert.ToInt32(model.Drawing);
                    drUD34["CheckBox08"] = Convert.ToInt32(model.DeepDrawing);
                    drUD34["CheckBox09"] = Convert.ToInt32(model.ExtraDeep);
                    drUD34["CheckBox10"] = Convert.ToInt32(model.Folding);
                    drUD34["CheckBox11"] = Convert.ToInt32(model.FormingOther);
                    drUD34["CheckBox12"] = Convert.ToInt32(model.RoHS);
                    drUD34["CheckBox13"] = Convert.ToInt32(model.PFOS);
                    drUD34["CheckBox14"] = Convert.ToInt32(model.SOC);
                    drUD34["CheckBox15"] = Convert.ToInt32(model.ELV);
                    drUD34["CheckBox16"] = Convert.ToInt32(model.REACH);
                    drUD34["CheckBox17"] = Convert.ToInt32(model.Other);
                    drUD34["ShortChar01"] = string.IsNullOrEmpty(model.DistCRRemark) ? "" : model.DistCRRemark;
                    drUD34["ShortChar02"] = string.IsNullOrEmpty(model.DistHRRemark) ? "" : model.DistHRRemark;
                    drUD34["ShortChar03"] = string.IsNullOrEmpty(model.DistGIRemark) ? "" : model.DistGIRemark;
                    drUD34["ShortChar04"] = string.IsNullOrEmpty(model.StainlessRemark) ? "" : model.StainlessRemark;
                    drUD34["ShortChar05"] = string.IsNullOrEmpty(model.Hardness.GetString()) ? "" : model.Hardness.GetString(); //ChemPersentRemark
                    drUD34["ShortChar06"] = string.IsNullOrEmpty(model.YieldRemark) ? "" : model.YieldRemark;
                    drUD34["ShortChar07"] = string.IsNullOrEmpty(model.TensileRemark) ? "" : model.TensileRemark;
                    drUD34["ShortChar08"] = string.IsNullOrEmpty(model.ElongationRemark) ? "" : model.ElongationRemark;
                    drUD34["ShortChar09"] = string.IsNullOrEmpty(model.HardnessRemark) ? "" : model.HardnessRemark;
                    drUD34["ShortChar10"] = string.IsNullOrEmpty(model.CoreLossRemark) ? "" : model.CoreLossRemark;
                    drUD34["ShortChar11"] = string.IsNullOrEmpty(model.MagnaticRemark) ? "" : model.MagnaticRemark;
                    drUD34["ShortChar12"] = string.IsNullOrEmpty(model.ProcessOtherRemark) ? "" : model.ProcessOtherRemark;
                    drUD34["ShortChar13"] = string.IsNullOrEmpty(model.FormingOtherRemark) ? "" : model.FormingOtherRemark;
                    drUD34["ShortChar14"] = string.IsNullOrEmpty(model.PartName) ? "" : model.PartName;
                    drUD34["ShortChar15"] = string.IsNullOrEmpty(model.ProductName) ? "" : model.ProductName;
                    drUD34["ShortChar16"] = string.IsNullOrEmpty(model.CusProcessRemark) ? "" : model.CusProcessRemark;
                    drUD34["ShortChar17"] = string.IsNullOrEmpty(model.EndUser1) ? "" : model.EndUser1;
                    drUD34["ShortChar18"] = string.IsNullOrEmpty(model.EndUser2) ? "" : model.EndUser2;
                    drUD34["ShortChar19"] = string.IsNullOrEmpty(model.EndUser3) ? "" : model.EndUser3;
                    drUD34["ShortChar20"] = string.IsNullOrEmpty(model.EndUser4) ? "" : model.EndUser4;
                    drUD34.EndEdit();
                    myUD34.Update(dsUD34);
                    currSession.Dispose();
                }
                else if (model.InsertState == false)
                {
                    string sql = string.Format(@"UPDATE UD34
                                                   SET Company = N'{0}'
                                                      ,Character01 = N'{2}'
                                                      ,Character02 = N'{3}'
                                                      ,Character03 = N'{4}'
                                                      ,Character04 = N'{64}'
                                                      ,Character05 = N'{5}'
                                                      ,Character06 = N'{6}'
                                                      ,Character07 = N'{7}'
                                                      ,Character08 = N'{8}'
                                                      ,Character09 = N'{9}'
                                                      ,Character10 = N'{10}'
                                                      ,Number01 = {11}
                                                      ,Number02 = {12}
                                                      ,Number03 = {13}
                                                      ,Number04 = {14}
                                                      ,Number06 = {15}
                                                      ,Number07 = {16}
                                                      ,Number08 = {17}
                                                      ,Number12 = {63}
                                                      ,Number13 = {18}
                                                      ,Number14 = {19}
                                                      ,Number15 = {20}
                                                      ,Number16 = {21}
                                                      ,Number17 = {22}
                                                      ,Number18 = {23}
                                                      ,Number19 = {24}
                                                      ,Number20 = {25}
                                                      ,CheckBox01 = {26}
                                                      ,CheckBox02 = {27}
                                                      ,CheckBox03 = {28}
                                                      ,CheckBox04 = {29}
                                                      ,CheckBox05 = {30}
                                                      ,CheckBox06 = {31}
                                                      ,CheckBox07 = {32}
                                                      ,CheckBox08 = {33}
                                                      ,CheckBox09 = {34}
                                                      ,CheckBox10 = {35}
                                                      ,CheckBox11 = {36}
                                                      ,CheckBox12 = {37}
                                                      ,CheckBox13 = {38}
                                                      ,CheckBox14 = {39}
                                                      ,CheckBox15 = {40}
                                                      ,CheckBox16 = {41}
                                                      ,CheckBox17 = {42}
                                                      ,ShortChar01 = N'{43}'
                                                      ,ShortChar02 = N'{44}'
                                                      ,ShortChar03 = N'{45}'
                                                      ,ShortChar04 = N'{46}'
                                                      ,ShortChar05 = N'{47}'
                                                      ,ShortChar06 = N'{48}'
                                                      ,ShortChar07 = N'{49}'
                                                      ,ShortChar08 = N'{50}'
                                                      ,ShortChar09 = N'{51}'
                                                      ,ShortChar10 = N'{52}'
                                                      ,ShortChar11 = N'{53}'
                                                      ,ShortChar12 = N'{54}'
                                                      ,ShortChar13 = N'{55}'
                                                      ,ShortChar14 = N'{56}'
                                                      ,ShortChar15 = N'{57}'
                                                      ,ShortChar16 = N'{58}'
                                                      ,ShortChar17 = N'{59}'
                                                      ,ShortChar18 = N'{60}'
                                                      ,ShortChar19 = N'{61}'
                                                      ,ShortChar20 = N'{62}'
                                                 WHERE Key1 = N'{1}'" + Environment.NewLine,
                                                      epiSession.CompanyID
                                                      , model.McssNum
                                                      , String.IsNullOrEmpty(model.IDCoil.ToString()) ? "" : model.IDCoil.ToString()
                                                      , String.IsNullOrEmpty(model.BaseMaterial) ? "" : model.BaseMaterial
                                                      , String.IsNullOrEmpty(model.CenterWaveRemark) ? "" : model.CenterWaveRemark
                                                      , String.IsNullOrEmpty(model.OtherRemark) ? "" : model.OtherRemark
                                                      , String.IsNullOrEmpty(model.ODCoil.ToString()) ? "" : model.ODCoil.ToString()
                                                      , String.IsNullOrEmpty(model.ChemPersentRemark) ? "" : model.ChemPersentRemark
                                                      , String.IsNullOrEmpty(model.CategoryGroup3) ? "" : model.CategoryGroup3
                                                      , String.IsNullOrEmpty(model.CategoryGroup2) ? "" : model.CategoryGroup2
                                                      , String.IsNullOrEmpty(model.CategoryGroup1) ? "" : model.CategoryGroup1          //10
                                                      , model.ChemPersent.GetInt()
                                                      , model.Yield.GetInt()
                                                      , model.Tensile.GetInt()
                                                      , model.Elongation.GetInt()
                                                      , model.CoreLoss.GetInt()          //15
                                                      , model.Magnatic.GetInt()
                                                      , model.Oriented.GetInt()
                                                      , model.WeightPerCoilMin.GetDecimal()
                                                      , model.WeightPerCoilMax.GetDecimal()
                                                      , model.PackingStyle.GetInt()            //20
                                                      , model.CenterWave.GetInt()
                                                      , model.Stainless.GetInt()
                                                      , model.DistGI.GetInt()
                                                      , model.DistHR.GetInt()
                                                      , model.DistCR.GetInt()            //25
                                                      , Convert.ToInt32(model.Welding)
                                                      , Convert.ToInt32(model.Painting)
                                                      , Convert.ToInt32(model.Degreasing)
                                                      , Convert.ToInt32(model.Blanking)
                                                      , Convert.ToInt32(model.ProcessOther)              //30
                                                      , Convert.ToInt32(model.Commercial)
                                                      , Convert.ToInt32(model.Drawing)
                                                      , Convert.ToInt32(model.DeepDrawing)
                                                      , Convert.ToInt32(model.ExtraDeep)
                                                      , Convert.ToInt32(model.Folding)                   //35
                                                      , Convert.ToInt32(model.FormingOther)
                                                      , Convert.ToInt32(model.RoHS)
                                                      , Convert.ToInt32(model.PFOS)
                                                      , Convert.ToInt32(model.SOC)
                                                      , Convert.ToInt32(model.ELV)                      //40
                                                      , Convert.ToInt32(model.REACH)
                                                      , Convert.ToInt32(model.Other)
                                                      , string.IsNullOrEmpty(model.DistCRRemark) ? "" : model.DistCRRemark
                                                      , string.IsNullOrEmpty(model.DistHRRemark) ? "" : model.DistHRRemark
                                                      , string.IsNullOrEmpty(model.DistGIRemark) ? "" : model.DistGIRemark            //45
                                                      , string.IsNullOrEmpty(model.StainlessRemark) ? "" : model.StainlessRemark
                                                      , string.IsNullOrEmpty(model.Hardness.GetString()) ? "" : model.Hardness.GetString()
                                                      , string.IsNullOrEmpty(model.YieldRemark) ? "" : model.YieldRemark
                                                      , string.IsNullOrEmpty(model.TensileRemark) ? "" : model.TensileRemark
                                                      , string.IsNullOrEmpty(model.ElongationRemark) ? "" : model.ElongationRemark            //50
                                                      , string.IsNullOrEmpty(model.HardnessRemark) ? "" : model.HardnessRemark
                                                      , string.IsNullOrEmpty(model.CoreLossRemark) ? "" : model.CoreLossRemark
                                                      , string.IsNullOrEmpty(model.MagnaticRemark) ? "" : model.MagnaticRemark
                                                      , string.IsNullOrEmpty(model.ProcessOtherRemark) ? "" : model.ProcessOtherRemark
                                                      , string.IsNullOrEmpty(model.FormingOtherRemark) ? "" : model.FormingOtherRemark            //55
                                                      , string.IsNullOrEmpty(model.PartName) ? "" : model.PartName
                                                      , string.IsNullOrEmpty(model.ProductName) ? "" : model.ProductName
                                                      , string.IsNullOrEmpty(model.CusProcessRemark) ? "" : model.CusProcessRemark
                                                      , string.IsNullOrEmpty(model.EndUser1) ? "" : model.EndUser1
                                                      , string.IsNullOrEmpty(model.EndUser2) ? "" : model.EndUser2            //60
                                                      , string.IsNullOrEmpty(model.EndUser3) ? "" : model.EndUser3
                                                      , string.IsNullOrEmpty(model.EndUser4) ? "" : model.EndUser4
                                                      , model.EdgeWave.GetDecimal()
                                                      , String.IsNullOrEmpty(model.EdgeWaveRemark) ? "" : model.EdgeWaveRemark);

                    Repository.Instance.ExecuteWithTransaction(sql, "Update MCSS UD34");
                }

                IsSucces = true;
                msgError = "";
            }
            catch (Exception ex)
            {
                IsSucces = false;
                msgError = ex.Message;
            }
        }

        public InitialModel GetInitial()
        {
            string sql = @"SELECT TOP 1 ud17.Key1, ud17.number01 as Thick, ud17.Number02 as Width, ud17.Number03 as Length
	                                        , ud17.Company, ud17.Key2 as UserID, ud17.ShortChar08 as SupplierCode, ven.Name as SupplierName
	                                        , ud17.ShortChar07 as CustID, cust.Name as CustomerName, ud17.ShortChar05 as MakerCode, maker.Character01 as MakerName
	                                        , ud17.ShortChar05 as MillCode, mill.Character01 as MillName, ud17.ShortChar01 as CommodityCode, cdt.Character01 AS CommodityName
	                                        , ud17.ShortChar02 as MatSpec1, spec.Character01 as MatSpec2, ud17.ShortChar03 as BussinessType, busi.Character01 as BussinessTypeName
	                                        FROM UD17 ud17
	                                        LEFT JOIN Vendor ven ON(ud17.ShortChar08 = ven.VendorID)
	                                        LEFT JOIN Customer cust ON(ud17.ShortChar07 = cust.CustID)
	                                        LEFT JOIN UD19 maker ON(ud17.ShortChar05 = maker.Key1)
	                                        LEFT JOIN UD14 mill ON(ud17.ShortChar05 = mill.Key1 and ud17.ShortChar06 = mill.Key2)
	                                        LEFT JOIN UD30 spec ON(ud17.ShortChar01 = spec.Key2 and ud17.ShortChar02 = spec.Key1)
	                                        LEFT JOIN UD25 busi ON(ud17.ShortChar03 = busi.Key1)
                                            LEFT JOIN UD29 cdt ON(ud17.ShortChar01 = cdt.Key1)
	                                        ORDER BY ud17.date01 DESC";

            return Repository.Instance.GetOne<InitialModel>(sql);
        }

        public void ClearInitial(string Key)
        {
            string sql = string.Format(@"DELETE FROM UD17 WHERE Key1 = N'{0}'", Key);
            //string sql = "TRANCATE TABLE UD17";
            Repository.Instance.ExecuteWithTransaction(sql, "ClearInitial MCSS");
        }

        public bool GetNewPart(MCSS model, Models.SessionInfo epiSession, out bool IsSucces, out string msgError)
        {
            try
            {
                Session currSession = new Session(epiSession.UserID, epiSession.UserPassword, epiSession.AppServer, Session.LicenseType.Default);
                Part myPart = new Part(currSession.ConnectionPool);
               
                bool partExst = false;
                string whereClausePart = string.Format(@"Part.PartNum='{0}'", model.McssNum);
                //whereClausePart.Replace("=\"","");
                PartDataSet dsPart = new PartDataSet();
                if(myPart.PartExists(model.McssNum))
                {
                    dsPart = myPart.GetRows(whereClausePart, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, 1, out partExst);
                }
                else
                {
                    myPart.GetNewPart(dsPart);
                }
                
                DataRow drPart = dsPart.Tables[0].Rows[0];
                drPart.BeginEdit();
                drPart["PartNum"] = model.McssNum;
                drPart["PartDescription"] = model.McssNum;
                drPart["UOMClassID"] = "COUNT";
                drPart["TypeCode"] = "P";
                drPart["IUM"] = "KG";  //Our UOM
                drPart["PUM"] = "KG";   //Purchasing UOM
                drPart["TypeCode"] = "M";
                drPart["SalesUM"] = "KG";   //Sale UOM
                drPart["Character02"] = "";  //NCR No.
                drPart["Character08"] = string.IsNullOrEmpty(model.CustID) ? "" : model.CustID;
                drPart["Character10"] = ""; //Pack No.
                drPart["ShortChar01"] = string.IsNullOrEmpty(model.CommodityCode) ? "" : model.CommodityCode;
                drPart["ShortChar02"] = string.IsNullOrEmpty(model.MatSpec1) ? "" : model.MatSpec1;
                drPart["ShortChar07"] = "";  //Old Stock No.
                drPart["ShortChar09"] = string.IsNullOrEmpty(model.Coating1) ? "" : model.Coating1;
                drPart["ShortChar10"] = "6";  //PartStatus = MCSS
                drPart["Number01"] = model.Thick;
                drPart["Number02"] = model.Width;
                drPart["Number03"] = model.Length;
                drPart["TrackLots"] = 1;
                drPart["Number11"] = 1;
                drPart["Number12"] = model.Pocession.GetInt();
                drPart["Character10"] = "N";

                drPart["NetWeight"] = model.WeightPerCoilMin.GetDecimal();
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
            }
            return IsSucces;
        }

        public IEnumerable<SpecialRef> SaveSpecailRef(SpecialRef model, Models.SessionInfo epiSession, Dictionary<int, string> Refs)
        {
            foreach (var pair in Refs)
            {
                string sql = string.Format(@"IF NOT EXISTS
									    (
										    SELECT * FROM ucc_tqa_McssSpecialRef (NOLOCK)
										    WHERE  LineNum = {2} AND MCSSNum = N'{1}'
									    )
									    BEGIN
                                            INSERT INTO ucc_tqa_McssSpecialRef
                                                   (Plant
                                                   ,MCSSNum
                                                   ,LineNum
                                                   ,Description
                                                   ,CreationDate
                                                   ,LastUpdateDate
                                                   ,CreatedBy
                                                   ,UpdatedBy)
                                             VALUES
                                                   (N'{0}' --<Plant, nvarchar(18),>
                                                   ,N'{1}' --<MCSSNum, nvarchar(100),>
                                                   ,{2}  --<LineNum, int,>
                                                   ,N'{3}' --<Description, nvarchar(max),>
                                                   ,GETDATE() --<CreationDate, datetime,>
                                                   ,GETDATE() --<LastUpdateDate, datetime,>
                                                   ,N'{4}' --<CreatedBy, nvarchar(45),>
                                                   ,N'{4}' --<UpdatedBy, nvarchar(45),>)
                                                 )
                                        END
                                        ELSE
                                        BEGIN
                                            UPDATE ucc_tqa_McssSpecialRef
                                               SET Description = N'{3}' 
                                                  ,LastUpdateDate = GETDATE()
                                                  ,UpdatedBy = N'{4}'
                                             WHERE  LineNum = {2} AND MCSSNum = N'{1}'
                                        END" + Environment.NewLine,
                                                                epiSession.PlantID,
                                                                model.McssNum,
                                                                pair.Key,
                                                                pair.Value,
                                                                epiSession.UserID);

                Repository.Instance.ExecuteWithTransaction(sql, "Update Specail Ref");
            }
            return GetSpecialRefByMCSS(model.McssNum);
        }

        public IEnumerable<SpecialRef> GetSpecialRefByMCSS(string mcssno)
        {
            string sql = string.Format(@"SELECT * FROM ucc_tqa_McssSpecialRef WHERE MCSSNum = N'{0}' ORDER BY LineID ASC", mcssno);

            return Repository.Instance.GetMany<SpecialRef>(sql);
        }
        
        public int GenerateRefNo(string mcssno)
        {
            string sql = string.Format(@"SELECT TOP 1 * FROM ucc_tqa_McssSpecialRef WHERE MCSSNum = N'{0}' ORDER BY LineID DESC", mcssno);

            var line = Repository.Instance.GetOne<int>(sql, "LineNum");

            return Convert.ToInt32(line) + 1;
        }

        public IEnumerable<SpecialRef> DeleteSpecailRef(SpecialRef model)
        {
            string sql = string.Format(@"DELETE FROM ucc_tqa_McssSpecialRef WHERE LineID = {0}" + Environment.NewLine, model.RefId);

            Repository.Instance.ExecuteWithTransaction(sql, "Delete Specail Ref");
            return GetSpecialRefByMCSS(model.McssNum);
        }

    }
}