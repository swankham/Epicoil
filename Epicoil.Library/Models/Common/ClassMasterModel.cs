using System;
using System.Data;

namespace Epicoil.Library.Models
{
    public class ClassMasterModel
    {
        public string Company { get; set; }

        public string Plant { get; set; }

        public int ClassNo { get; set; }

        public string Description { get; set; }

        public int SteelCover { get; set; }

        public int CustomerReq { get; set; }

        public int ComudityReq { get; set; }

        public int SpecCodeReq { get; set; }

        public int PlateCodeReq { get; set; }

        public int MakerCodeReq { get; set; }

        public int MillCodeReq { get; set; }

        public int SupplierReq { get; set; }

        public int ThicknessReq { get; set; }

        public int WidthReq { get; set; }

        public int LengthReq { get; set; }

        public int Remark1Req { get; set; }

        public int Remark2Req { get; set; }

        public int Remark3Req { get; set; }

        public int Remark4Req { get; set; }

        public int Remark5Req { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public void DataBind(DataRow row)
        {
            this.Company = (string)row["company"];
            this.Plant = (string)row["plant"];
            this.ClassNo = (int)row["ClassNo"];
            this.Description = (string)row["Description"].GetString();
            this.SteelCover = (int)row["SteelCover"].GetInt();
            this.CustomerReq = (int)row["CustomerReq"].GetInt();
            this.ComudityReq = (int)row["ComudityReq"].GetInt();
            this.SpecCodeReq = (int)row["SpecCodeReq"].GetInt();
            this.PlateCodeReq = (int)row["PlateCodeReq"].GetInt();
            this.MakerCodeReq = (int)row["MakerCodeReq"].GetInt();
            this.MillCodeReq = (int)row["MillCodeReq"].GetInt();
            this.SupplierReq = (int)row["SupplierReq"].GetInt();
            this.ThicknessReq = (int)row["ThicknessReq"].GetInt();
            this.WidthReq = (int)row["WidthReq"].GetInt();
            this.LengthReq = (int)row["LengthReq"].GetInt();
            this.Remark1Req = (int)row["Remark1Req"].GetInt();
            this.Remark2Req = (int)row["Remark2Req"].GetInt();
            this.Remark3Req = (int)row["Remark3Req"].GetInt();
            this.Remark4Req = (int)row["Remark4Req"].GetInt();
            this.Remark5Req = (int)row["Remark5Req"].GetInt();
            this.CreatedBy = (string)row["CreatedBy"];
            this.UpdatedBy = (string)row["UpdatedBy"];
        }
    }
}