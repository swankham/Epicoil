using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;

namespace Epicoil.Library.Repositories.Planning
{
    public interface IWorkEntryRepo
    {
        IEnumerable<MaterialModel> GetAllMaterial(string plant);

        IEnumerable<MaterialModel> GetAllMatByFilter(string plant, MaterialModel model);
    }
}
