using System;
using System.Collections.Generic;
using System.Data;
using Epicoil.Library.Frameworks;

namespace Epicoil.Library.Models
{
    public class SpecModel
    {
        public string SpecID { get; set; }
        public string SpecName { get; set; }
        public string Commodity { get; set; }
        public decimal Gravity { get; set; }
        public int RequireCoating { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.SpecID = (string)row["Key1"];
            this.SpecName = (string)row["Character01"];
            this.Commodity = (string)row["Key2"];
            this.Gravity = (decimal)row["Number01"];
            this.RequireCoating = Convert.ToInt32(row["CheckBox01"]);
        }
    }
}
