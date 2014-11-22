using Epicoil.Library.Models.Planning;
using System;
using System.Data;

namespace Epicoil.Library.Models.Production
{
    public class MaterialFindingModel : MaterialModel
    {
        public bool FoundFlag { get; set; }

        public bool UnPackFlag { get; set; }

        public bool ProcessFlag { get; set; }

        public string ProcessLineCode { get; set; }

        public string ProcessLineName { get; set; }

        public override void DataBind(DataRow row)
        {
            base.DataBind(row);

            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.WorkOrderNum = (string)row["WorkOrderNum"].GetString();
            this.FoundFlag = Convert.ToBoolean((int)row["FoundFlag"].GetInt());
            this.UnPackFlag = Convert.ToBoolean((int)row["UnPackFlag"].GetInt());
            this.ProcessFlag = Convert.ToBoolean((int)row["ProcessFlag"].GetInt());
            this.ProcessLineCode = (string)row["ProcessLineCode"].GetString();
            this.ProcessLineName = (string)row["ProcessLineName"].GetString();
        }
    }
}