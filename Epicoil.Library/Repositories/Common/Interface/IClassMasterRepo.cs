using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface IClassMasterRepo
    {
        IEnumerable<ClassMasterModel> GetAll(string plant);

        ClassMasterModel GetByID(string plant, int Id);
    }
}
