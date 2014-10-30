using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicoil.Library
{
    public enum MaterialStatus
    {
        Planning = 0,
        Possibility = 1,
        Processing_Plan = 2,
        Processing_Complete = 3,
        Stock_Out = 4,
        MCSS = 5
    }

    public enum ProductStatus
    {
        B = 0,
        N = 1,
        X = 2,
        F = 3,
        C = 4,
        S = 5   
    }
}
