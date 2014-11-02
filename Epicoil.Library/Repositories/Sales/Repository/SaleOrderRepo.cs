using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Sales;

namespace Epicoil.Library.Repositories.Sales
{
    public class SaleOrderRepo : ISaleOrderRepo
    {
        public IEnumerable<OrderHeadModel> GetOrderHeadAll()
        {
            string sql = @"SELECT soh.OrderNum, soh.OrderDate, soh.RequestDate, cust.CustID, cust.Name as CustomerName
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



    }
}
