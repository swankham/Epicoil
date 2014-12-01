using Epicoil.Library.Models.Planning;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Epicoil.Library.Models.Production
{
    public class ProductionHeadModel
    {
        public string Company { get; set; }

        public string Plant { get; set; }

        public int ProductionID { get; set; }

        public int WorkOrderID { get; set; }

        public string WorkOrderNum { get; set; }

        public decimal CutSeq { get; set; }

        public string CutSeqStr { get; set; }

        public decimal CuttingAutoWeight { get; set; }

        public decimal CuttingManualWeight { get; set; }

        public DateTime ProductionDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime FinishTime { get; set; }

        public int PuaseTime { get; set; }

        public string ProcessLineID { get; set; }

        public int CompleteFlag { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public ResourceModel ProcessLineDetail = new ResourceModel();
        public IEnumerable<ResourceModel> ResourceList = new List<ResourceModel>();
        public IEnumerable<SerialCuttingModel> SerialList = new List<SerialCuttingModel>();
        public IEnumerable<MaterialModel> MaterailList = new List<MaterialModel>();
        public IEnumerable<ReasonModel> ReasonList = new List<ReasonModel>();
        public IEnumerable<CuttedLineUpModel> CuttedList = new List<CuttedLineUpModel>();

        public List<ResourceModel> ProcessLines
        {
            get { return this.ResourceList.ToList(); }
            set { this.ResourceList = value; }
        }

        public List<SerialCuttingModel> SerialLines
        {
            get { return this.SerialList.ToList(); }
            set { this.SerialList = value; }
        }

        public List<MaterialModel> Materials 
        {
            get { return this.MaterailList.ToList(); }
            set { this.MaterailList = value; }
        }

        public List<ReasonModel> Reasons
        {
            get { return this.ReasonList.ToList(); }
            set { this.ReasonList = value; }
        }

        public List<CuttedLineUpModel> Cutteds
        {
            get { return this.CuttedList.ToList(); }
            set { this.CuttedList = value; }
        }

        public string Reason { get; set; }

        public string ActionState { get; set; }

        public int OperationState { get; set; }

        public void DataBind(DataRow row)
        {
            this.Plant = (string)row["Plant"].GetString();
            this.ProductionID = (int)row["ProductionID"].GetInt();
            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.WorkOrderNum = (string)row["WorkOrderNum"].GetString();
            this.ProductionDate = (DateTime)row["ProductionDate"].GetDate();
            this.StartTime = (DateTime)row["StartTime"].GetDate();
            this.FinishTime = (DateTime)row["FinishTime"].GetDate();
            this.PuaseTime = (int)row["PuaseTime"].GetInt();
            this.ProcessLineID = (string)row["ProcessLineID"].GetString();
            this.CompleteFlag = (int)row["CompleteFlag"].GetInt();
            this.CreationDate = (DateTime)row["CreationDate"].GetDate();
            this.LastUpdateDate = (DateTime)row["LastUpdateDate"].GetDate();
            this.CreatedBy = (string)row["CreatedBy"].GetString();
            this.UpdatedBy = (string)row["UpdatedBy"].GetString();
            //this.CutSeq = (decimal)row["CutSeq"].GetDecimal();
            this.CutSeqStr = this.CutSeq.ToString("#,###.#");
            this.OperationState = (int)row["OperationState"].GetInt();
        }
    }
}