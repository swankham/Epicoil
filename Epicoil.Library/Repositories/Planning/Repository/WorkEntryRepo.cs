using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Frameworks;

namespace Epicoil.Library.Repositories.Planning
{
    public class WorkEntryRepo : IWorkEntryRepo
    {
        public IEnumerable<MaterialModel> GetAllMaterial(string plant)
        {
            string sql = string.Format(@"SELECT pl.PartNum
	                                        , pl.LotNum
	                                        , p.ShortChar01 as CommodityCode, cmdt.Character01 as CommodityName
	                                        , p.ShortChar02 as SpecCode, spec.Character01 as SpecName
	                                        , p.ShortChar09 as CoatingCode, coat.Character01 as CoatingName
	                                        , pl.ShortChar02 as BussinessType, busi.Character01 as BussinessTypeName
	                                        , 0 as UsingWeight, oh.Quantity * pl.Number10 as RemainWeight, (pl.Number03 / 100) as LengthM
	                                        , oh.Quantity, oh.DimCode, oh.Quantity as QuantityPack, 0 as CBSelect
	                                        , '0' as Status, '' as Note, p.Number12 as Possession
	                                        , pl.* 
                                        FROM PartLot pl
	                                        INNER JOIN Part p ON(pl.PartNum = p.PartNum)
	                                        LEFT JOIN UD29 cmdt ON(p.ShortChar01 = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(p.ShortChar01 = spec.Key2 and p.ShortChar02 = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(p.ShortChar09 = coat.Key1)
	                                        LEFT JOIN UD25 busi ON(pl.Character02 = busi.Key1)
	                                        INNER JOIN (SELECT PartNum, LotNum, sum(OnhandQty) as Quantity, DimCode FROM PartBin
				                                        GROUP BY PartNum, LotNum, DimCode) oh
				                                        ON(p.PartNum = oh.PartNum and pl.LotNum = oh.LotNum)");

            return Repository.Instance.GetMany<MaterialModel>(sql);
        }


        public IEnumerable<MaterialModel> GetAllMatByFilter(string plant, MaterialModel model)
        {
            throw new NotImplementedException();
        }
    }
}
