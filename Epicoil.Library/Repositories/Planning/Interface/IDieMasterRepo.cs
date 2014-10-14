using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models.Planning;

namespace Epicoil.Library.Repositories.Planning
{
    public interface IDieMasterRepo
    {
        IEnumerable<DieModel> GetDieAll(string plant);

        DiePatternModel GetPattern(string patternID);
    }
}
