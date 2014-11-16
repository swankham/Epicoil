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

    public enum CoilStep
    {
        MA = 1,
        MB = 2,
        MC = 3,
        MD = 4,
        ME = 5,
        MF = 6
    }

    public enum SimulateOpt
    {
        Weight = 0,
        Length = 1,
        Division = 2
    }

    public enum SimulateStatus
    {
        Simulate = 0,
        Simulated = 1
    }

    public enum GenerateSNStatus
    {
        Create_SN = 0,
        SN_Created = 1
    }

    public enum OperationState
    {
        Sale = 0,
        Planning = 1,
        Production1 = 2,
        Production2 = 3,
        FinishJob = 4,
        Logistic = 5
    }

    public enum CompleteStatus
    {
        Confirm = 0,
        Confirmed = 1,
        Hold = 2
    }
}
