using Epicoil.Library.Models.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicoil.Library.Repositories.Production
{
    public interface IProductionRepo
    {
        IEnumerable<SerialCuttingModel> GetAllSerialByWorkOrder(int workOrderID);
    }
}
