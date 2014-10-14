using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Frameworks;
using Epicoil.Library.Models.Planning;

namespace Epicoil.Library.Repositories.Planning
{
    public class DieMasterRepo : IDieMasterRepo
    {
        public IEnumerable<DieModel> GetDieAll(string plant)
        {
            string sql = string.Format(@"SELECT * FROM UD107 WHERE Key5 = N'{0}' ORDER BY Key1 ASC", plant);
            if (true)
            {

            }
            return Repository.Instance.GetMany<DieModel>(sql);
        }


        public DiePatternModel GetPattern(string patternID)
        {
            throw new NotImplementedException();
        }
    }
}
