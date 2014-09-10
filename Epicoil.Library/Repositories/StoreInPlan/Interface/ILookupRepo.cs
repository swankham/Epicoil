using System.Collections.Generic;

using Epicoil.Library.Models.StoreInPlan;

namespace Epicoil.Library.Repositories.StoreInPlan
{
    public interface ILookupRepo
    {
        IEnumerable<MappingLookupModel> GetAll(string typeCode, string supplierCode);

        IEnumerable<MappingLookupModel> GetByFilter(MappingLookupModel model);

        MappingLookupModel GetByID(MappingLookupModel model);

        IEnumerable<MappingLookupModel> Save(MappingLookupModel model);

        IEnumerable<MappingLookupModel> Delete(MappingLookupModel model);
    }
}