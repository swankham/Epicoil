using System.Data;

namespace Epicoil.Library.Models.Planning
{
    public class DieModel
    {
        #region Constructors

        public DieModel()
        {
            Pattern = new DiePatternModel();
        }

        #endregion Constructors

        #region Properties

        public string CompanyID { get; set; }

        public string PlantID { get; set; }

        public string DieCode { get; set; }

        public string DieName { get; set; }

        public string DieRemark { get; set; }

        public string PatternID { get; set; }

        public DiePatternModel Pattern { get; set; }

        #endregion Properties

        #region Methods

        public void DataBind(DataRow row)
        {
            this.CompanyID = (string)row["Company"].GetString();
            this.PlantID = (string)row["Key5"].GetString();
            this.DieCode = (string)row["Key1"].GetString();
            this.DieName = (string)row["Character01"].GetString();
            this.DieRemark = (string)row["Character02"].GetString();
            this.PatternID = (string)row["ShortChar01"].GetString();
        }

        #endregion Methods
    }

    public class PartList
    {
        #region Properties

        public string DieCode { get; set; }

        public string PatternID { get; set; }

        public string OutPutSide { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public string PartCode { get; set; }

        public string PartName { get; set; }

        public int Active { get; set; }

        #endregion
    }
}