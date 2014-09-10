using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories
{
    public class ImportPortRepo : IImportPortRepo
    {
        public IEnumerable<PortModel> GetAll()
        {
            string sql = string.Format(@"SELECT * FROM ucc_ImportPort WHERE ActiveFlag = 1 Order by PortID asc");

            return Repository.Instance.GetMany<PortModel>(sql);
        }

        public IEnumerable<PortModel> GetByFilter(PortModel model)
        {
            IEnumerable<PortModel> query = GetAll();

            if (model.PortCode != null) { query = query.Where(p => p.PortCode.Contains(model.PortCode.ToString())); }
            if (model.PortName != null) { query = query.Where(p => p.PortName.Contains(model.PortName.ToString())); }

            return query;
        }

        public PortModel GetByID(string PortCode)
        {
            string sql = string.Format(@"SELECT * FROM ucc_ImportPort WHERE ActiveFlag = 1 AND PortCode = N'{0}'", PortCode);
            return Repository.Instance.GetOne<PortModel>(sql);
        }
    }
}