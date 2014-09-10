using System;
using System.Data;

namespace Epicoil.Library.Models
{
    public class CommodityModel
    {
        public string CommodityCode { get; set; }

        public string CommodityName { get; set; }

        public bool CoatingRequire { get; set; }

        public bool LicenceRequire { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.CommodityCode = (string)row["Key1"];
            this.CommodityName = (string)row["Character01"];
            this.CoatingRequire = Convert.ToBoolean(row["CheckBox01"].GetBoolean());
            this.LicenceRequire = Convert.ToBoolean(row["CheckBox02"].GetBoolean());
        }
    }
}