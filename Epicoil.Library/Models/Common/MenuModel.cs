using System.Data;

namespace Epicoil.Library.Models
{
    public class MenuModel
    {
        public int MenuID { get; set; }

        public string MenuDescription { get; set; }

        public int ParentMenuID { get; set; }

        public int Sequence { get; set; }

        public string Module { get; set; }

        public string MenuType { get; set; }
        public int MenuLevel { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.MenuID = (int)row["MenuID"].GetInt();
            this.MenuDescription = (string)row["MenuDesc"].GetString();
            this.ParentMenuID = (int)row["ParentMenuID"].GetInt();
            this.Sequence = (int)row["Sequence"].GetInt();
            this.Module = (string)row["Module"].GetString();
            this.MenuType = (string)row["MenuType"].GetString();
            this.MenuLevel = (int)row["MenuLevel"].GetInt();
        }
    }
}