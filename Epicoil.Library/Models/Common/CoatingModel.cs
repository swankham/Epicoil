using System;
using System.Collections.Generic;
using System.Data;
using Epicoil.Library.Frameworks;

namespace Epicoil.Library.Models
{
    public class CoatingModel
    {
        public string CoatingPlate { get; set; }
        public string CoatingName { get; set; }
        public string Abbr { get; set; }
        public decimal FrontPlate { get; set; }
        public decimal BackPlate { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.CoatingPlate = (string)row["Key1"];
            this.CoatingName = (string)row["Character01"];
            this.Abbr = (string)row["Character02"];
            this.FrontPlate = (decimal)row["Number01"];
            this.BackPlate = (decimal)row["Number02"];
        }
    }
}
