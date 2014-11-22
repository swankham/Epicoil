using System;
using System.Collections.Generic;
using Epicoil.Library.Models;
using Epicoil.Library.Models.TQA;

namespace Epicoil.Library.Repositories.TQA
{
    public interface IProductMasterRepo
    {
        IEnumerable<ProductsMasterModel> GetAll(string plant, bool waitingflag, bool inactiveflag, bool activeflag, bool rejectflag);

        IEnumerable<ProductsMasterModel> GetByFilter(ProductsMasterModel data);

        ProductsMasterModel Get(ProductsMasterModel data);

        bool CheckPartExisting(string PartNum);

        bool CheckMaterialExisting(string norCode, string matCode);

        bool CheckUsedLine(string norNum);

        ProductsMasterModel Save(SessionInfo _session, ProductsMasterModel data);

        bool NewPart(SessionInfo _session, ProductsMasterModel model, out bool IsSucces, out string msgError);
    }
}