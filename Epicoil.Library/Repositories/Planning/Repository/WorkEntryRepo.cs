using Epicoil.Library.Frameworks;
using Epicoil.Library.Models.Planning;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories.Planning
{
    public class WorkEntryRepo : IWorkEntryRepo
    {
        public IEnumerable<MaterialModel> GetAllMaterial(string plant)
        {
            string sql = string.Format(@"SELECT pl.PartNum
	                                        , pl.LotNum
	                                        , p.ShortChar01 as CommodityCode, cmdt.Character01 as CommodityName
	                                        , p.ShortChar02 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                        , p.ShortChar09 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                        , pl.Character02 as BussinessType, ISNULL(busi.Character01, '') as BussinessTypeName
	                                        , 0 as UsingWeight, oh.Quantity * pl.Number10 as RemainWeight, (pl.Number03 / 100) as LengthM
	                                        , oh.Quantity, oh.Quantity as RemainQty, oh.DimCode, oh.Quantity as QuantityPack, 0 as CBSelect
	                                        , '0' as Status, '' as Note, p.Number12 as Possession, pln.Plant
	                                        , pl.Number01, pl.Number02, pl.Number03, pl.Number04, 0 as ProductStatus
	                                        , pl.ShortChar03 as SupplierCode, ISNULL(ven.Name, '') as SupplierName
	                                        , cust.CustID, ISNULL(cust.Name, '') as CustomerName
	                                        , pl.ShortChar01 as MakerCode, ISNULL(maker.Character01, '') as MakerName
	                                        , pl.ShortChar02 as MillCode, ISNULL(mill.Character01, '') as MillName
                                        FROM PartLot pl
	                                        INNER JOIN Part p ON(pl.PartNum = p.PartNum)
                                            INNER JOIN PartPlant pln ON(p.PartNum = pln.PartNum)
	                                        LEFT JOIN UD29 cmdt ON(p.ShortChar01 = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(p.ShortChar01 = spec.Key2 and p.ShortChar02 = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(p.ShortChar09 = coat.Key1)
	                                        LEFT JOIN UD25 busi ON(pl.Character02 = busi.Key1)
	                                        INNER JOIN (SELECT PartNum, LotNum, sum(OnhandQty) as Quantity, DimCode FROM PartBin
				                                        GROUP BY PartNum, LotNum, DimCode) oh
				                                        ON(p.PartNum = oh.PartNum and pl.LotNum = oh.LotNum)
	                                        LEFT JOIN Vendor ven ON(pl.ShortChar03 = ven.VendorID)
	                                        LEFT JOIN UD19 maker ON(pl.ShortChar01 = maker.Key1)
	                                        LEFT JOIN UD14 mill ON(pl.ShortChar01 = mill.Key1 and pl.ShortChar02 = mill.Key2)
	                                        LEFT JOIN Customer cust ON(p.Character08 = cust.CustID)
                                        WHERE pln.Plant = N'{0}'", plant);

            return Repository.Instance.GetMany<MaterialModel>(sql);
        }

        public IEnumerable<MaterialModel> GetAllMatByFilter(string plant, PlaningHeadModel model)
        {
            IEnumerable<MaterialModel> query = this.GetAllMaterial(plant);

            if (!string.IsNullOrEmpty(model.MaterialPattern.CustID) && Convert.ToBoolean(model.CurrentClass.CustomerReq.GetInt())) query = query.Where(p => p.CustID.ToString().ToUpper().Equals(model.MaterialPattern.CustID.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(model.MaterialPattern.CommodityCode) && Convert.ToBoolean(model.CurrentClass.ComudityReq.GetInt())) query = query.Where(p => p.CommodityCode.ToString().ToUpper().Equals(model.MaterialPattern.CommodityCode.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(model.MaterialPattern.SpecCode) && Convert.ToBoolean(model.CurrentClass.SpecCodeReq.GetInt())) query = query.Where(p => p.SpecCode.ToString().ToUpper().Equals(model.MaterialPattern.SpecCode.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(model.MaterialPattern.CoatingCode) && Convert.ToBoolean(model.CurrentClass.PlateCodeReq.GetInt())) query = query.Where(p => p.CoatingCode.ToString().ToUpper().Equals(model.MaterialPattern.CoatingCode.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(model.MaterialPattern.MakerCode) && Convert.ToBoolean(model.CurrentClass.MakerCodeReq.GetInt())) query = query.Where(p => p.MakerCode.ToString().ToUpper().Equals(model.MaterialPattern.MakerCode.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(model.MaterialPattern.MillCode) && Convert.ToBoolean(model.CurrentClass.MillCodeReq.GetInt())) query = query.Where(p => p.MillCode.ToString().ToUpper().Equals(model.MaterialPattern.MillCode.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(model.MaterialPattern.SupplierCode) && Convert.ToBoolean(model.CurrentClass.SupplierReq.GetInt())) query = query.Where(p => p.SupplierCode.ToString().ToUpper().Equals(model.MaterialPattern.SupplierCode.ToString().ToUpper()));

            query = query.Where(p => p.Possession.Equals(Convert.ToInt32(model.Possession)));

            if (Convert.ToBoolean(model.CurrentClass.ThicknessReq.GetInt())) query = query.Where(p => p.Thick.Equals(model.MaterialPattern.Thick));
            if (Convert.ToBoolean(model.CurrentClass.WidthReq.GetInt())) query = query.Where(p => p.Width.Equals(model.MaterialPattern.Width));
            if (Convert.ToBoolean(model.CurrentClass.LengthReq.GetInt())) query = query.Where(p => p.Length.Equals(model.MaterialPattern.Length));

            return query;
        }
    }
}