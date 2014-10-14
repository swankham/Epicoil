using Epicoil.Library.Models;
using System.Collections.Generic;
using System.Data;

namespace Epicoil.Library.Models.Planning
{
    public class DieModel
    {
        public string CompanyID { get; set; }

        public string PlantID { get; set; }

        public string DieCode { get; set; }

        public string DieName { get; set; }

        public string DieRemark { get; set; }

        public string PatternID { get; set; }

        public DiePatternModel Pattern { get; set; }

        public void DataBind(DataRow row)
        {
            this.CompanyID = (string)row["Company"].GetString();
            this.PlantID = (string)row["Key5"].GetString();
            this.DieCode = (string)row["Key1"].GetString();
            this.DieName = (string)row["Character01"].GetString();
            this.DieRemark = (string)row["Character02"].GetString();
            this.PatternID = (string)row["ShortChar01"].GetString();
        }
    }
}