using System;
using System.Data;

namespace Epicoil.Library.Models.StoreInPlan
{
    public class ImexCheckModel : StoreInPlanHead
    {
        public string UpdatedBy { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public override void DataBind(DataRow row)
        {
            base.DataBind(row);

            this.LastUpdateDate = (DateTime)row["LastUpdateDate"].GetDate();
            this.UpdatedBy = (string)row["UpdatedBy"].GetString();
            this.UserGroup = (string)row["UserGroup"].GetString();
        }
    }
}