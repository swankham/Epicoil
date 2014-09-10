using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public interface IBussinessTypeRepo
    {
        IEnumerable<BussinessTypeModel> GetAll();

        IEnumerable<BussinessTypeModel> GetByFilter(BussinessTypeModel model);

        BussinessTypeModel GetByID(string code);
    }
}