using System.Data;
using Epicoil.Library.Models;
using System.Collections.Generic;
using Epicoil.Library.Frameworks;

namespace Epicoil.Library.Repositories
{
    public class ResourceRepo : IResourceRepo
    {
        public IEnumerable<ResourceModel> GetAll(string plant)
        {
            string sql = string.Format(@"SELECT rgp.Plant, rgp.ResourceGrpID, rgp.Description as GropDescription
	                                             , res.ResourceID, res.Description as ResourceDescription
                                            FROM Resource_ res
                                            INNER JOIN ResourceGroup rgp ON(res.ResourceGrpID = rgp.ResourceGrpID)
                                            WHERE res.Inactive = 0 AND rgp.Plant = N'{0}'
                                            ORDER BY Plant, GropDescription", plant);

            return Repository.Instance.GetMany<ResourceModel>(sql);
        }

        public IEnumerable<ResourceModel> GetByFilter(ResourceModel model)
        {
            IEnumerable<ResourceModel> query = GetAll(model.Plant);

            //if (model.CommodityCode != null) { query = query.Where(p => p.CommodityCode.Contains(model.CommodityCode.ToString())); }
            //if (model.CommodityName != null) { query = query.Where(p => p.CommodityName.Contains(model.CommodityName.ToString())); }

            return query;
        }

        public ResourceModel GetByID(string plant, string ResourceID)
        {
            string sql = string.Format(@"SELECT rgp.Plant, rgp.ResourceGrpID, rgp.Description as GropDescription
	                                             , res.ResourceID, res.Description as ResourceDescription
                                            FROM Resource_ res
                                            INNER JOIN ResourceGroup rgp ON(res.ResourceGrpID = rgp.ResourceGrpID)
                                            WHERE res.Inactive = 0 AND rgp.Plant = N'{0}' AND res.ResourceID = N'{1}'
                                            ORDER BY Plant, GropDescription", plant, ResourceID);

            return Repository.Instance.GetOne<ResourceModel>(sql);
        }
    }
}
