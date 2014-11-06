using System;
using System.Data;

namespace Epicoil.Library.Models.Planning
{
    public class CoilBackModel
    {
        public int LineID { get; set; }

        public int WorkOrderID { get; set; }

        public int TransactionLineID { get; set; }

        public int SeqID { get; set; }

        public string Serial { get; set; }

        public string CommodityCode { get; set; }

        public string CommodityName { get; set; }

        public string SpecCode { get; set; }

        public string SpecName { get; set; }

        public string CoatingCode { get; set; }

        public string CoatingName { get; set; }

        public decimal Gravity { get; set; }

        public decimal FrontPlate { get; set; }

        public decimal BackPlate { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public decimal Weight { get; set; }

        public decimal Qty { get; set; }

        public decimal LengthM { get; set; }
        //{
        //    get
        //    {
        //        return (Length == 0) ? CBLengthMeter(Weight, Width, Thick, Gravity, FrontPlate, BackPlate) : Math.Round((Length / 1000), 2);
        //    }
        //}

        public string MCSSNo { get; set; }

        public string OldSerial { get; set; }

        public int BackStep { get; set; }

        public string Status { get; set; }

        public string BussinessType { get; set; }

        public string BussinessTypeName { get; set; }

        public int Possession { get; set; }

        public string PossessionName
        {
            get
            {
                return Enum.GetName(typeof(Possession), Convert.ToInt32(Possession));
            }
        }

        public int ProductStatus { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public int CoilBackState { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public void DataBind(DataRow row)
        {
            this.LineID = (int)row["LineID"].GetInt();
            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.TransactionLineID = (int)row["TransactionLineID"].GetInt();
            this.SeqID = (int)row["SeqID"].GetInt();
            this.Serial = string.IsNullOrEmpty((string)row["Serial"].GetString()) ? "" : (string)row["Serial"].GetString();
            this.CommodityCode = (string)row["CommodityCode"].GetString();
            this.CommodityName = (string)row["CommodityName"].GetString();
            this.SpecCode = (string)row["SpecCode"].GetString();
            this.SpecName = (string)row["SpecName"].GetString();
            this.CoatingCode = string.IsNullOrEmpty((string)row["CoatingCode"].GetString()) ? "" : (string)row["CoatingCode"].GetString();
            this.CoatingName = (string)row["CoatingName"].GetString();
            this.Gravity = (decimal)row["Gravity"].GetDecimal();
            this.FrontPlate = (decimal)row["FrontPlate"].GetDecimal();
            this.BackPlate = (decimal)row["BackPlate"].GetDecimal();
            this.Thick = (decimal)row["Thick"].GetDecimal();
            this.Width = (decimal)row["Width"].GetDecimal();
            this.Length = (decimal)row["Length"].GetDecimal();
            this.Weight = (decimal)row["Weight"].GetDecimal();
            this.Qty = (decimal)row["Qty"].GetDecimal();
            this.LengthM = (decimal)row["LengthM"].GetDecimal();
            this.MCSSNo = (string)row["MCSSNo"].GetString();
            this.OldSerial = (string)row["OldSerial"].GetString();
            this.BackStep = (int)row["BackStep"].GetInt();
            this.Status = (string)row["Status"].GetString();
            this.BussinessType = string.IsNullOrEmpty((string)row["BussinessType"].GetString()) ? "" : (string)row["BussinessType"].GetString();
            this.BussinessTypeName = (string)row["BussinessTypeName"].GetString(); 
            this.Possession = (int)row["Possession"].GetInt();
            this.ProductStatus = (int)row["ProductStatus"].GetInt();
            this.Description = string.IsNullOrEmpty((string)row["Description"].GetString()) ? "" : (string)row["Description"].GetString();
            this.Note = string.IsNullOrEmpty((string)row["Note"].GetString()) ? "" : (string)row["Note"].GetString(); 
            this.CoilBackState = (int)row["CoilBackState"].GetInt();
            this.CreationDate = (DateTime)row["CreationDate"].GetDate();
            this.LastUpdateDate = (DateTime)row["LastUpdateDate"].GetDate();
            this.CreatedBy = (string)row["CreatedBy"].GetString();
            this.UpdatedBy = (string)row["UpdatedBy"].GetString();
        }

        public decimal CBLengthMeter(decimal weight, decimal width, decimal thick, decimal gravity, decimal frontPlate, decimal backPlate)
        {
            decimal d1 = weight * 1000;
            decimal d2 = thick * gravity;
            decimal d3 = (frontPlate + backPlate) / 1000;
            decimal d4 = width / 1000;

            d2 = d2 + d3;
            d2 = d2 * d4;

            //Fix bug Infinity.
            if (d2 == 0) d2 = 1;
            decimal result = d1 / d2;

            //Convert mm to M.
            return Math.Round(result / 1000, 2);
        }
    }
}