using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Sales;
using Epicoil.Library.Models.Planning;

namespace Epicoil.Library.Repositories.Sales
{
    public class SaleOrderRepo : ISaleOrderRepo
    {
        public readonly IClassMasterRepo _repoCls;

        public SaleOrderRepo()
        {
            this._repoCls = new ClassMasterRepo();
        }
        public IEnumerable<OrderHeadModel> GetOrderHeadAll()
        {
            string sql = @"SELECT soh.OrderNum, soh.OrderDate, soh.RequestDate, cust.CustID, cust.Name as CustomerName
	                            , ensr.CustID as EndUserCode, ensr.Name as EndUserName, vend.CustID as ShipTo, vend.Name as ShipToName
	                            , soh.PONum, socd.Key1 as SOCode, socd.Character01 as SOCodeName, soh.TermsCode
	                            , busi.Key1 as BussinessType, busi.Character01 as BussinessTypeName, soh.ShortChar04
	                            , soh.EntryPerson, soh.Number03 as TotalWeight, soh.Number04 as TotalAmount
                            FROM OrderHed soh
                             LEFT JOIN Customer cust ON(soh.CustNum = cust.CustNum)
                             LEFT JOIN Customer ensr ON(soh.ShortChar07 = ensr.CustID)
                             LEFT JOIN Customer vend ON(soh.ShortChar09 = vend.CustID)
                             LEFT JOIN UD33 socd ON(soh.ShortChar02 = socd.Key1)
                             LEFT JOIN UD25 busi ON(soh.ShortChar03 = busi.Key1)
                            WHERE soh.OpenOrder = 1 AND soh.VoidOrder = 0";

            return Repository.Instance.GetMany<OrderHeadModel>(sql);
        }

        public OrderHeadModel GetOrderByID(string orderId)
        {
            string sql = string.Format(@"SELECT soh.OrderNum, soh.OrderDate, soh.RequestDate, cust.CustID, cust.Name as CustomerName
	                            , ensr.CustID as EndUserCode, ensr.Name as EndUserName, vend.CustID as ShipTo, vend.Name as ShipToName
	                            , soh.PONum, socd.Key1 as SOCode, socd.Character01 as SOCodeName, soh.TermsCode
	                            , busi.Key1 as BussinessType, busi.Character01 as BussinessTypeName
	                            , soh.EntryPerson, soh.Number03 as TotalWeight, soh.Number04 as TotalAmount
                            FROM OrderHed soh
                             LEFT JOIN Customer cust ON(soh.CustNum = cust.CustNum)
                             LEFT JOIN Customer ensr ON(soh.ShortChar07 = ensr.CustID)
                             LEFT JOIN Customer vend ON(soh.ShortChar09 = vend.CustID)
                             LEFT JOIN UD33 socd ON(soh.ShortChar02 = socd.Key1)
                             LEFT JOIN UD25 busi ON(soh.ShortChar03 = busi.Key1)
                            WHERE soh.OpenOrder = 1 AND soh.VoidOrder = 0 AND soh.OrderNum = {0} ", orderId);

            return Repository.Instance.GetOne<OrderHeadModel>(sql);
        }

        public IEnumerable<OrderHeadModel> GetOrderHeadByFilter(OrderHeadModel data)
        {
            IEnumerable<OrderHeadModel> query = GetOrderHeadAll();

            return query;
        }

        public IEnumerable<OrderDetailModel> GetOrderDtlAll(string OrderId)
        {
            string sql = string.Format(@"SELECT sol.OrderLine, sol.OrderNum, sol.PartNum
	                                        , cmdt.Key1 as CommodityCode, cmdt.Character01 as CommodityName
	                                        , spec.Key1 as SpecCode, spec.Character01 as SpecName
	                                        , coat.Key1 as CoatingCode, coat.Character01 as CoatingName
	                                        , nor.ShortChar13 as Possession, busi.Key1 as BussinessType, busi.Character01 as BussinessTypeName
	                                        , cust.CustID, cust.Name as CustomerName, sol.SellingQuantity, sol.DocUnitPrice
	                                        , sol.Number01, sol.Number02, sol.Number03, sol.Number04, sol.Number06, sol.Number07
                                            , nor.Number17
                                        FROM OrderDtl sol
	                                        LEFT JOIN UD29 cmdt ON(sol.ShortChar01 = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(sol.ShortChar01 = spec.Key2 and sol.ShortChar02 = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(sol.ShortChar03 = coat.Key1)
	                                        INNER JOIN UD12 nor ON(sol.PartNum = nor.Key1)
	                                        LEFT JOIN UD25 busi ON(sol.ShortChar10 = busi.Key1)
	                                        LEFT JOIN Customer cust ON(sol.Character01 = cust.CustID)
                                        WHERE sol.OpenLine = 1 AND sol.VoidLine = 0 
	                                        AND sol.CheckBox06 = 0 AND sol.OrderNum = {0} ORDER BY sol.OrderLine", OrderId);

            return Repository.Instance.GetMany<OrderDetailModel>(sql);
        }

        public OrderDetailModel GetOrderDtlByID(string orderId, int lineId)
        {
            string sql = string.Format(@"SELECT sol.OrderLine, sol.OrderNum, sol.PartNum
	                                        , cmdt.Key1 as CommodityCode, cmdt.Character01 as CommodityName
	                                        , spec.Key1 as SpecCode, spec.Character01 as SpecName
	                                        , coat.Key1 as CoatingCode, coat.Character01 as CoatingName
	                                        , nor.ShortChar13 as Possession, busi.Key1 as BussinessType, busi.Character01 as BussinessTypeName
	                                        , cust.CustID, cust.Name as CustomerName, sol.SellingQuantity, sol.DocUnitPrice
	                                        , sol.Number01, sol.Number02, sol.Number03, sol.Number04, sol.Number06, sol.Number07
                                            , nor.Number17
                                        FROM OrderDtl sol
	                                        LEFT JOIN UD29 cmdt ON(sol.ShortChar01 = cmdt.Key1)
	                                        LEFT JOIN UD30 spec ON(sol.ShortChar01 = spec.Key2 and sol.ShortChar02 = spec.Key1)
	                                        LEFT JOIN UD31 coat ON(sol.ShortChar03 = coat.Key1)
	                                        INNER JOIN UD12 nor ON(sol.PartNum = nor.Key1)
	                                        LEFT JOIN UD25 busi ON(sol.ShortChar10 = busi.Key1)
	                                        LEFT JOIN Customer cust ON(sol.Character01 = cust.CustID)
                                        WHERE sol.OpenLine = 1 AND sol.VoidLine = 0 
	                                        AND sol.CheckBox06 = 0 
                                            AND sol.OrderNum = {0} 
                                            AND sol.OrderLine = {1}", orderId, lineId);

            var result = Repository.Instance.GetOne<OrderDetailModel>(sql);
            if(result != null)
            {
                result.ClassDetail = _repoCls.GetByID("MfgSys", result.ClassID);
            }

            return result;
        }

        public IEnumerable<OrderDetailModel> GetOrderDtlByFilter(OrderDetailModel data, PlanningHeadModel model)
        {
            IEnumerable<OrderDetailModel> query = this.GetOrderDtlAll(data.OrderNum);
            if (model.CurrentClass != null)
            {
                if (!string.IsNullOrEmpty(model.MaterialPattern.CustID) && Convert.ToBoolean(model.CurrentClass.CustomerReq.GetInt())) query = query.Where(p => p.CustID.ToString().ToUpper().Equals(model.MaterialPattern.CustID.ToString().ToUpper()));
                if (!string.IsNullOrEmpty(model.MaterialPattern.CommodityCode) && Convert.ToBoolean(model.CurrentClass.ComudityReq.GetInt())) query = query.Where(p => p.CommodityCode.ToString().ToUpper().Equals(model.MaterialPattern.CommodityCode.ToString().ToUpper()));
                if (!string.IsNullOrEmpty(model.MaterialPattern.SpecCode) && Convert.ToBoolean(model.CurrentClass.SpecCodeReq.GetInt())) query = query.Where(p => p.SpecCode.ToString().ToUpper().Equals(model.MaterialPattern.SpecCode.ToString().ToUpper()));
                if (!string.IsNullOrEmpty(model.MaterialPattern.CoatingCode) && Convert.ToBoolean(model.CurrentClass.PlateCodeReq.GetInt())) query = query.Where(p => p.CoatingCode.ToString().ToUpper().Equals(model.MaterialPattern.CoatingCode.ToString().ToUpper()));
                if (!string.IsNullOrEmpty(model.MaterialPattern.MakerCode) && Convert.ToBoolean(model.CurrentClass.MakerCodeReq.GetInt())) query = query.Where(p => p.MakerCode.ToString().ToUpper().Equals(model.MaterialPattern.MakerCode.ToString().ToUpper()));
                if (!string.IsNullOrEmpty(model.MaterialPattern.MillCode) && Convert.ToBoolean(model.CurrentClass.MillCodeReq.GetInt())) query = query.Where(p => p.MillCode.ToString().ToUpper().Equals(model.MaterialPattern.MillCode.ToString().ToUpper()));
                if (!string.IsNullOrEmpty(model.MaterialPattern.SupplierCode) && Convert.ToBoolean(model.CurrentClass.SupplierReq.GetInt())) query = query.Where(p => p.SupplierCode.ToString().ToUpper().Equals(model.MaterialPattern.SupplierCode.ToString().ToUpper()));

                //if (Convert.ToBoolean(model.CurrentClass.ThicknessReq.GetInt())) query = query.Where(p => p.Thick.Equals(model.MaterialPattern.Thick));
                if (model.CuttingDesign.ToList().Count != 0) query = query.Where(p => p.Thick.Equals(model.MaterialPattern.Thick));
                if (Convert.ToBoolean(model.CurrentClass.WidthReq.GetInt())) query = query.Where(p => p.Width.Equals(model.MaterialPattern.Width));
                if (Convert.ToBoolean(model.CurrentClass.LengthReq.GetInt())) query = query.Where(p => p.Length.Equals(model.MaterialPattern.Length));
            }

            if (!string.IsNullOrEmpty(model.Possession)) query = query.Where(p => p.Possession.Equals(Convert.ToInt32(model.Possession)));

            //if (model.ProcessLineDetail.ThickMin != 0)
            query = query.Where(p => p.Thick >= model.ProcessLineDetail.ThickMin);
            //if (model.ProcessLineDetail.ThickMax != 0)
            query = query.Where(p => p.Thick <= model.ProcessLineDetail.ThickMax);
            //if (model.ProcessLineDetail.WidthMin != 0)
            query = query.Where(p => p.Width >= model.ProcessLineDetail.WidthMin);
            //if (model.ProcessLineDetail.WidthMax != 0)
            query = query.Where(p => p.Width <= model.ProcessLineDetail.WidthMax);
            //if (model.ProcessLineDetail.LengthMin != 0)
            query = query.Where(p => p.Length >= model.ProcessLineDetail.LengthMin);
            //if (model.ProcessLineDetail.LengthMax != 0)
            query = query.Where(p => p.Length <= model.ProcessLineDetail.LengthMax);

            //if machine is not Sliter and Leveller must be filter for sheet only.
            if (model.ProcessLineDetail.LengthMin > 0 || model.ProcessLineDetail.LengthMax > 0)
                query = query.Where(p => p.Length > 0);

            //if machine is Sliter and Leveller must be filter for coil only.
            if (model.ProcessLineDetail.LengthMin == 0 && model.ProcessLineDetail.LengthMax == 0)
                query = query.Where(p => p.Length.Equals(0));

            return query;
        }
    }
}
