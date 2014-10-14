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

        //public List<PartList> OutPutList { get; set; }
        public DiePatternModel DiePatternInst = new DiePatternModel();

        public DiePatternModel Pattern 
        {
            get { return this.DiePatternInst; }
            set { this.DiePatternInst = value; }
        }

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

    public class PartList
    {
        public string DieCode { get; set; }

        public string PatternID { get; set; }

        public string OutPutSide { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public string PartCode { get; set; }

        public string PartName { get; set; }

        public int Active { get; set; }
    }
}