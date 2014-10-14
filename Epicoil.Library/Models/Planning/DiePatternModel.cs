using System.Data;

namespace Epicoil.Library.Models.Planning
{
    public class DiePatternModel
    {
        public string PatternID { get; set; }

        public string StrokePerPcs { get; set; }

        public string FROutPut { get; set; }

        public string DROutPut { get; set; }

        public string OPOutPut { get; set; }

        public string Block { get; set; }

        public string Remark { get; set; }

        public void DataBind(DataRow row)
        {
            this.PatternID = (string)row["Key1"].GetString();
            this.StrokePerPcs = (string)row["ShortChar01"].GetString();
            this.FROutPut = (string)row["ShortChar02"].GetString();
            this.DROutPut = (string)row["ShortChar03"].GetString();
            this.OPOutPut = (string)row["ShortChar04"].GetString();
            this.Block = (string)row["ShortChar05"].GetString();
            this.Remark = (string)row["Character01"].GetString();
        }
    }
}