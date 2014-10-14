using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;

namespace Epicoil.Library.Repositories
{
    public interface IUserCodeRepo
    {
        IEnumerable<UserCodeModel> GetAll(string CodeTypeID);

        IEnumerable<UserCodeModel> GetByFilter(UserCodeModel model);

        UserCodeModel GetByID(string CodeTypeID, string CodeID);
    }
}
