using System.Data;

namespace Epicoil.Library.Models
{
    public class UserCodeModel
    {
        public string CodeTypeID { get; set; }

        public string CodeID { get; set; }

        public string CodeDesc { get; set; }

        public string LongDesc { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.CodeTypeID = (string)row["CodeTypeID"].GetString();
            this.CodeID = (string)row["CodeID"].GetString();
            this.CodeDesc = (string)row["CodeDesc"].GetString();
            this.LongDesc = (string)row["LongDesc"].GetString();
        }
    }
}