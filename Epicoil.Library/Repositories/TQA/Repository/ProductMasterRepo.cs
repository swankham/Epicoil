using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.TQA;
using System;
using System.Collections.Generic;

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
    }
}