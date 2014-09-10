using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public class WharehouseRepo : IWharehouseRepo
    {
        public IEnumerable<WharehouseModel> GetAll(string Plant)
        {
            string sql = string.Format(@"SELECT WarehouseCode, Description, Plant
                                            FROM Warehse Where Plant = N'{0}'", Plant);

            return Repository.Instance.GetMany<WharehouseModel>(sql);
        }

        public WharehouseModel GetByID(string code)
        {
            string sql = string.Format(@"SELECT WarehouseCode, Description, Plant
                                            FROM Warehse Where WarehouseCode = N'{0}'", code);
            return Repository.Instance.GetOne<WharehouseModel>(sql);
        }
    }
}