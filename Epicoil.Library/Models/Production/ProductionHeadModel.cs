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

        public int CutSeq { get; set; }

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

        public void DataBind(DataRow row)
        {
            this.ProductionID = (int)row["ProductionID"].GetInt();
            this.WorkOrderID = (int)row["Length"].GetInt();
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
            this.CutSeqStr = this.CutSeq.ToString("#,###.#");
        }
    }
}