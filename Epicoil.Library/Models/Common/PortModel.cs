using System.Data;

namespace Epicoil.Library.Models
{
    public class PortModel
    {
        public int PortID { get; set; }

        public string PortCode { get; set; }

        public string PortName { get; set; }

        public int ActiveFlag { get; set; }

        public void DataBind(DataRow row)
        {
            this.PortID = (int)row["PortID"];
            this.PortCode = (string)row["PortCode"];
            this.PortName = (string)row["PortName"];
            this.ActiveFlag = (int)row["ActiveFlag"].GetInt();
        }
    }
}