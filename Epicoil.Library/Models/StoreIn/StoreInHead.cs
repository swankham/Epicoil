using System;
using System.Data;
using Epicoil.Library.Models.StoreInPlan;

namespace Epicoil.Library.Models.StoreIn
{
    public class StoreInHead : StoreInPlanHead
    {
        public decimal TransactionID { get; set; }

        public int StoreInID { get; set; }

        public string StoreInNum { get; set; }

        public DateTime StoreInDate { get; set; }

        public int Possession { get; set; }

        public int PONum { get; set; }

        public string PONumber { get; set; }

        public string PossessionPromt
        {
            get
            {
                if (Possession == 2) return "ITAKU"; else if (Possession == 0) return "JISHAZAI"; else return "";
            }
        }

        public virtual void DataBind(DataRow row)
        {
            //binding base model
            base.DataBind(row);
            this.TransactionID = (decimal)row["TransactionID"].GetDecimal();
            this.StoreInID = (int)row["StoreInID"].GetInt();
            this.StoreInNum = (string)row["StoreInNum"].GetString();
            this.StoreInDate = (DateTime)row["StoreInDate"].GetDate();
            this.Possession = string.IsNullOrEmpty(row["Poscession"].ToString()) ? 2 : Convert.ToInt32(row["Poscession"].ToString());
        }
    }
}