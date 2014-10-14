using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Repositories
{
    public class MainRepo : IMainRepo
    {
        public IEnumerable<MenuModel> GetAll(int ParentID, int MenuLevel)
        {
            string sql = string.Format(@"SELECT * FROM epc_menus WHERE ParentMenuID = {0} AND MenuLevel = {1} ORDER BY Sequence ASC", ParentID, MenuLevel);
            return Repository.Instance.GetMany<MenuModel>(sql);
        }

        public IEnumerable<ItemMenuModel> GetAllItem(int ParentID)
        {
            string sql = string.Format(@"SELECT * FROM epc_menus WHERE ParentMenuID = {0} AND Enabled = 1 AND MenuLevel = 4 ORDER BY Sequence ASC", ParentID);
            return Repository.Instance.GetMany<ItemMenuModel>(sql);
        }
    }
}