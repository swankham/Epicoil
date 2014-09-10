using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicoil.Library.Models.StoreInPlan
{
    public class StoreInPlanDialogModel : StoreInPlanHead
    {
        public int PONum { get; set; }

        public string PONumber { get; set; }

        public override void DataBind(DataRow row)
        {
            base.DataBind(row);

            this.PONum = (int)row["PONum"].GetInt();
            this.PONumber = (string)row["PONumber"].GetString();
        }
    }
}
