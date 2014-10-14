using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;

namespace Epicoil.Repositories
{
    public interface IMainRepo
    {
        IEnumerable<MenuModel> GetAll(int ParentID, int MenuLevel);

        IEnumerable<ItemMenuModel> GetAllItem(int ParentID);
    }
}
