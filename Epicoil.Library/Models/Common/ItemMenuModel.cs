using System.Data;

namespace Epicoil.Library.Models
{
    public class ItemMenuModel : MenuModel
    {
        public string SecCode { get; set; }


        public override void DataBind(DataRow row)
        {
            base.DataBind(row);
            this.SecCode = (string)row["SecCode"].GetString();
        }
    }
}