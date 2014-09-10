using System.Collections.Generic;
using Epicoil.Library.Models;

namespace Epicoil.Library.Repositories
{
    public interface IPORepo
    {
        IEnumerable<POLineModel> GetAll();

        IEnumerable<POLineModel> GetByPO(int PONum);

        POLineModel GetByID(int PONum, int POLine); 
    }
}
