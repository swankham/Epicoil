using System;
using System.Collections.Generic;
using Epicoil.Library.Models;
using Epicoil.Library.Models.TQA;

namespace Epicoil.Library.Repositories.TQA
{
    public interface IMcssRepo
    {
        IEnumerable<MCSS> GetAll(string plant);

        IEnumerable<MCSS> GetByFilter(DateTime DateFrom, DateTime DateTo, MCSS model, bool TISIFlag);

        MCSS Get(string plant, string McssNum);

        int GenerateMcssID();

        InitialModel GetInitial();

        void ClearInitial(string Key);

        MCSS Save(MCSS model, SessionInfo epiSession, out bool IsSucces, out string msgError);

        void SaveUD15(MCSS model, SessionInfo epiSession, out bool IsSucces, out string msgError);

        void SaveUD34(MCSS model, SessionInfo epiSession, out bool IsSucces, out string msgError);

        bool GetNewPart(MCSS model, SessionInfo epiSession, out bool IsSucces, out string msgError);

        IEnumerable<SpecialRef> SaveSpecailRef(SpecialRef model, SessionInfo epiSession, Dictionary<int, string> Refs);

        IEnumerable<SpecialRef> GetSpecialRefByMCSS(string mcssno);

        int GenerateRefNo(string mcssno);

        IEnumerable<SpecialRef> DeleteSpecailRef(SpecialRef model);

    }
}