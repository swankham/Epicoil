using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace Epicoil.Library.Frameworks
{
    public interface IRepository
    {
        void DataBind(DataRow row);
    }
}
