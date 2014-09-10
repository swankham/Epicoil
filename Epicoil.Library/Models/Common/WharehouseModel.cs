using System.Data;

namespace Epicoil.Library.Models
{
    public class WharehouseModel
    {
        public string Plant { get; set; }

        public string WarehouseCode { get; set; }

        public string Description { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.Plant = (string)row["Plant"].GetString();
            this.WarehouseCode = (string)row["WarehouseCode"].GetString();
            this.Description = (string)row["Description"].GetString();
        }
    }
}