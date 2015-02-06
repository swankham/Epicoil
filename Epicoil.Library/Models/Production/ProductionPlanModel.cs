using CustomAnnotations;
using System;
using System.Collections.Generic;
using System.Data;

namespace Epicoil.Library.Models.Production
{
    #region ProductionPlanModel

    public class ProductionPlanModel
    {
        #region Constructer

        public ProductionPlanModel()
        {
            Resources = new List<ResourceModel>();
            WorkOrders = new List<WorkOrderPlanModel>();
            WorkOrdersPlanned = new List<WorkOrderPlanModel>();
        }

        #endregion Constructer

        #region Properties

        public int WorkOrderID { get; set; }

        public string WorkOrderNumForm { get; set; }

        public string WorkOrderNumTo { get; set; }

        public DateTime? WorkDateFrom { get; set; }

        [DateTimeCompare("WorkDateFrom", ValueComparison.IsGreaterThan, ErrorMessage = "End work date must be later than start work date.")]
        public DateTime? WorkDateTo { get; set; }

        public DateTime? DueDateFrom { get; set; }

        [DateTimeCompare("DueDateFrom", ValueComparison.IsGreaterThan, ErrorMessage = "End due date must be later than start due date.")]
        public DateTime? DueDateTo { get; set; }

        public bool? MachineFlag { get; set; }

        public string MachineCode { get; set; }

        public IList<ResourceModel> Resources { get; set; }

        public IList<WorkOrderPlanModel> WorkOrders { get; set; }

        public IList<WorkOrderPlanModel> WorkOrdersPlanned { get; set; }

        #endregion Properties
    }

    #endregion ProductionPlanModel

    #region WorkOrdersPlan

    public class WorkOrderPlanModel : IEquatable<WorkOrderPlanModel>
    {
        public int Seq { get; set; }

        public int WorkOrderId { get; set; }

        public string WorkOrderNum { get; set; }

        public string BussinessType { get; set; }

        public string BussinessTypeName { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }

        public string OrderType { get; set; }

        public string PIC { get; set; }

        public string PICName { get; set; }

        public string Possession { get; set; }

        public string PossessionName
        {
            get
            {
                return Enum.GetName(typeof(Possession), Convert.ToInt32(Possession));
            }
        }

        public decimal ProcessStep { get; set; }

        public string ProcessLine { get; set; }

        public decimal Yield { get; set; }

        public Dictionary<int, int> WorksDependency { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            WorkOrderPlanModel objAsPart = obj as WorkOrderPlanModel;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public override int GetHashCode()
        {
            return WorkOrderId;
        }

        public bool Equals(WorkOrderPlanModel other)
        {
            if (other == null) return false;
            return (this.WorkOrderId.Equals(other.WorkOrderId));
        }

        public virtual void DataBind(DataRow row)
        {
            this.Seq = (int)row["Seq"].GetInt();
            this.WorkOrderId = (int)row["WorkOrderId"].GetInt();
            this.WorkOrderNum = (string)row["WorkOrderNum"].GetString();
            this.BussinessType = (string)row["BT"].GetString();
            this.BussinessTypeName = (string)row["BussinessTypeName"].GetString();
            this.IssueDate = (DateTime)row["IssueDate"].GetDate();
            this.DueDate = (DateTime)row["DueDate"].GetDate();
            this.OrderType = string.IsNullOrEmpty((string)row["OrderType"]) ? "" : this.OrderType = (string)row["OrderType"].GetString();
            this.PIC = (string)row["PIC"].GetString();
            this.PICName = (string)row["PICName"].GetString();
            this.Possession = (string)row["Possession"].GetString();
            this.ProcessLine = (string)row["ProcessLine"].GetString();
            this.ProcessStep = (decimal)row["ProcessStep"].GetDecimal();
            this.Yield = (decimal)row["Yield"].GetDecimal();
        }
    }

    #endregion WorkOrdersPlan
}