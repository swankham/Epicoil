using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface IResourceRepo
    {
        IEnumerable<ResourceModel> GetAll(string plant);

        IEnumerable<ResourceModel> GetByFilter(ResourceModel model);

        ResourceModel GetByID(string plant, string ResourceID);
    }
}
