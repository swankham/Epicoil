using System;
using System.Data;

namespace Epicoil.Library.Models.StoreInPlan
{
    public class StoreInPlanDialogModel : StoreInPlanHeadModel
    {
        public int PONum { get; set; }

        public string PONumber { get; set; }

        //Used by Store In Balance.
        public DateTime InvoiceDateFrom { get; set; }

        public DateTime InvoiceDateTo { get; set; }

        public override void DataBind(DataRow row)
        {
            base.DataBind(row);

            this.PONum = (int)row["PONum"].GetInt();
            this.PONumber = (string)row["PONumber"].GetString();
        }
    }
}