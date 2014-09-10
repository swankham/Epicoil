using System.Data;

namespace Epicoil.Library.Models.StoreIn
{
    public class StoreInHeadBalance : StoreInHead
    {
        public int StatusFlag { get; set; }

        public string StoreInStatus
        {
            get
            {
                string flag = "Not Done";
                if (StatusFlag == 1)
                {
                    flag = "Done";
                }
                return flag;
            }
        }

        public override void DataBind(DataRow row)
        {
            //binding base model
            base.DataBind(row);
            this.StatusFlag = (int)row["StatusFlag"].GetInt();
        }
    }
}