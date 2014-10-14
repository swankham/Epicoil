using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epicoil.Library.Models
{
    public class ResourceModel
    {
        public string Plant { get; set; }
        public string ResourceGrpID { get; set; }
        public string GropDescription { get; set; }
        public string ResourceID { get; set; }
        public string ResourceDescription { get; set; }

        public void DataBind(DataRow row)
        {
            this.Plant = (string)row["Plant"].GetString();
            this.ResourceGrpID = (string)row["ResourceGrpID"].GetString();
            this.GropDescription = (string)row["GropDescription"].GetString();
            this.ResourceID = (string)row["ResourceID"].GetString();
            this.ResourceDescription = (string)row["ResourceDescription"].GetString();
        }
    }

}
