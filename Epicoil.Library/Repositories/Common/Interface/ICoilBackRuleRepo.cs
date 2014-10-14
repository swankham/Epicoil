using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;

namespace Epicoil.Library.Repositories
{
    public interface ICoilBackRuleRepo
    {
        IEnumerable<CoilBackRuleModel> GetAll();

        IEnumerable<CoilBackRuleModel> GetByFilter(CoilBackRuleModel filter);
    }
}
