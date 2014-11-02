using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Sales;

namespace Epicoil.Library.Repositories.Sales
{
    public interface ISaleOrderRepo
    {
        IEnumerable<OrderHeadModel> GetOrderHeadAll();

        OrderHeadModel GetOrderByID(string orderId);

        IEnumerable<OrderHeadModel> GetOrderHeadByFilter(OrderHeadModel data);
    }
}
