using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Production;
using Epicoil.Library.Repositories.Planning;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Repositories.Production
{
    public class ProductionPlanRepo : IProductionPlanRepo
    {
        #region Fields

        private readonly IWorkEntryRepo _work;
        private readonly IResourceRepo _repoResrc;

        #endregion Fields

        #region Constucter

        public ProductionPlanRepo()
        {
            this._work = new WorkEntryRepo();
            this._repoResrc = new ResourceRepo();
        }

        #endregion Constucter

        #region Methods

        public ProductionPlanModel Get(SessionInfo _session)
        {
            ProductionPlanModel model = new ProductionPlanModel();
            model.WorkDateFrom = DateTime.Now;
            model.WorkDateTo = DateTime.Now;
            model.DueDateFrom = DateTime.Now;
            model.DueDateTo = DateTime.Now;
            model.Resources = _repoResrc.GetAll(_session.PlantID).ToList();
            model.WorkOrders = GetWorksPlan(_session).ToList();

            return model;
        }

        public IEnumerable<WorkOrderPlanModel> GetWorksPlan(SessionInfo _session)
        {
            string sql = @"SELECT 0 as Seq, uf.Name as PICName, busi.Character01 as BussinessTypeName, plh.*
                                            FROM ucc_pln_PlanHead plh (NOLOCK)
                                            LEFT JOIN UserFile uf ON(plh.PIC = uf.DcdUserID)
                                            LEFT JOIN UD25 busi ON(plh.BT = busi.Key1)
                                            WHERE OpenFlag = 1";

            var result = Repository.Instance.GetMany<WorkOrderPlanModel>(sql);
            return result;
        }

        #endregion Methods
    }
}