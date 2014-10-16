using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;

namespace Epicoil.Library.Repositories.Planning
{
    public interface IDieMasterRepo
    {
        IEnumerable<DieModel> GetDieAll(string plant);

        IEnumerable<DiePatternModel> GetPatternAll();

        IEnumerable<DiePatternModel> GetByFilter(DiePatternModel Filter);

        DieModel GetByID(string plant, string dieID);

        DiePatternModel GetDiePattern(string patternID);

        IEnumerable<DieModel> Save(DieModel data, SessionInfo epiSession);

        string MaxID();

        void DeleteLine(string dieID);

        IEnumerable<DieModel> GetByFilterDie(DieModel Filter);
    }

}
