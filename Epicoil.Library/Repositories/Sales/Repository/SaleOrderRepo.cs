using Epicoil.Library.Frameworks;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;

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
	                            , busi.Key1 as BussinessType, busi.Character01 as BussinessTypeName, soh.ShortChar04
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
            if (result != null)
            {
                result.ClassDetail = _repoCls.GetByID("MfgSys", result.ClassID);
            }
            return result;
        }

        public IEnumerable<OrderDetailModel> GetOrderDtlByFilter(string soNo, PlanningHeadModel model)
        {
            IEnumerable<OrderDetailModel> query = this.GetOrderDtlAll(soNo);

            //Verify alway.
            if (!string.IsNullOrEmpty(model.BussinessType)) query = query.Where(p => p.BussinessType.Equals(model.BussinessType.GetString()));
            if (!string.IsNullOrEmpty(model.Possession)) query = query.Where(p => p.Possession.Equals(Convert.ToInt32(model.Possession)));

            if (model.Materails.ToList().Count > 0)
            {
                var mat = model.Materails.FirstOrDefault();
                if (Convert.ToBoolean(model.CurrentClass.CustomerReq)) query = query.Where(p => p.CustID.GetString().ToUpper().Equals(mat.CustID.GetString().ToUpper()));
                if (Convert.ToBoolean(model.CurrentClass.ComudityReq)) query = query.Where(p => p.CommodityCode.GetString().ToUpper().Equals(mat.CommodityCode.GetString().ToUpper()));
                if (Convert.ToBoolean(model.CurrentClass.SpecCodeReq)) query = query.Where(p => p.SpecCode.GetString().ToUpper().Equals(mat.SpecCode.GetString().ToUpper()));
                if (Convert.ToBoolean(model.CurrentClass.PlateCodeReq)) query = query.Where(p => (string.IsNullOrEmpty(p.CoatingCode) ? "" : p.CoatingCode.ToUpper()).Equals(string.IsNullOrEmpty(mat.CoatingCode) ? "" : mat.CoatingCode.ToUpper()));

                if (Convert.ToBoolean(model.CurrentClass.MakerCodeReq.GetInt())) query = query.Where(p => p.MakerCode.GetString().ToUpper().Equals(mat.MakerCode.GetString().ToUpper()));
                if (Convert.ToBoolean(model.CurrentClass.MillCodeReq.GetInt())) query = query.Where(p => p.MillCode.GetString().ToUpper().Equals(mat.MillCode.GetString().ToUpper()));
                if (Convert.ToBoolean(model.CurrentClass.SupplierReq.GetInt())) query = query.Where(p => p.SupplierCode.GetString().ToUpper().Equals(mat.SupplierCode.GetString().ToUpper()));

                query = query.Where(p => p.Thick.Equals(mat.Thick));
                query = query.Where(p => p.Width <= mat.Width);
                if (Convert.ToBoolean(model.CurrentClass.LengthReq.GetInt())) query = query.Where(p => p.Length.Equals(mat.Length));
            }

            query = query.Where(p => p.Thick >= model.ProcessLineDetail.ThickMin);
            query = query.Where(p => p.Thick <= model.ProcessLineDetail.ThickMax);

            return query;
        }
    }
}