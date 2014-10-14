using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;
using Epicoil.Library.Frameworks;

namespace Epicoil.Library.Repositories
{
    public class UserCodeRepo : IUserCodeRepo
    {
        public IEnumerable<UserCodeModel> GetAll(string CodeTypeID)
        {
            string sql = string.Format(@"SELECT CodeTypeID, CodeID, CodeDesc, LongDesc FROM UDCodes 
                                            WHERE CodeTypeID = N'{0}' AND IsActive = 1", CodeTypeID);

            return Repository.Instance.GetMany<UserCodeModel>(sql);
        }

        public IEnumerable<UserCodeModel> GetByFilter(UserCodeModel model)
        {
            IEnumerable<UserCodeModel> query = GetAll(model.CodeTypeID);

            //if (model.CommodityCode != null) { query = query.Where(p => p.CommodityCode.Contains(model.CommodityCode.ToString())); }
            //if (model.CommodityName != null) { query = query.Where(p => p.CommodityName.Contains(model.CommodityName.ToString())); }

            return query;
        }

        public UserCodeModel GetByID(string CodeTypeID, string CodeID)
        {
            string sql = string.Format(@"SELECT CodeTypeID, CodeID, CodeDesc, LongDesc FROM UDCodes 
                                            WHERE CodeTypeID = N'{0}' AND CodeID = N'{1}' AND IsActive = 1", CodeTypeID, CodeID);

            return Repository.Instance.GetOne<UserCodeModel>(sql);
        }
    }
}
