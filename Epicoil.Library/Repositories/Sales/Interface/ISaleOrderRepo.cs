using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Sales;
using Epicoil.Library.Models.Planning;

namespace Epicoil.Library.Repositories.Sales
{
    public interface ISaleOrderRepo
    {
        IEnumerable<OrderHeadModel> GetOrderHeader(PlanningHeadModel model, string orderNum = null);

        OrderHeadModel GetOrderByID(string orderId);

        IEnumerable<OrderHeadModel> GetOrderHeadByFilter(OrderHeadModel data, PlanningHeadModel model);

        IEnumerable<OrderDetailModel> GetOrderDtlAll(string OrderId);

        OrderDetailModel GetOrderDtlByID(string orderId, int lineId);

        IEnumerable<OrderDetailModel> GetOrderDtlByFilter(string soNo, PlanningHeadModel model);
    }
}
