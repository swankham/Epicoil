using System.Data;

namespace Epicoil.Library.Models
{
    public class CategoryGroupModel
    {
        public string CategoryGroupCode { get; set; }

        public string CategoryGroupName { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.CategoryGroupCode = (string)row["Key1"];
            this.CategoryGroupName = (string)row["Character01"];
        }
    }
}