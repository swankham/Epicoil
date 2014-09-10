using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface ICommodity
    {
        IEnumerable<CommodityModel> GetAll();

        IEnumerable<CommodityModel> GetByFilter(CommodityModel model);

        CommodityModel GetByID(string CmdtCode);
    }
}