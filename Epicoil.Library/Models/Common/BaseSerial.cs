using System;
using System.Data;

namespace Epicoil.Library.Models
{
    public class BaseSerial
    {
        public int Possession { get; set; }

        public string PossessionName
        {
            get
            {
                return Enum.GetName(typeof(Possession), Convert.ToInt32(Possession));
            }
        }

        public string BussinessType { get; set; }

        public string BussinessTypeName { get; set; }

        public string CommodityCode { get; set; }

        public string CommodityName { get; set; }

        public string SpecCode { get; set; }

        public string SpecName { get; set; }

        public decimal Gravity { get; set; }

        public string CoatingCode { get; set; }

        public string CoatingName { get; set; }

        public decimal FrontPlate { get; set; }

        public decimal BackPlate { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.Possession = (int)row["Possession"].GetInt();
            this.BussinessType = string.IsNullOrEmpty(row["BussinessType"].GetString()) ? "" : row["BussinessType"].GetString();
            this.BussinessTypeName = (string)row["BussinessTypeName"].GetString();
            this.CommodityCode = (string)row["CommodityCode"].GetString();
            this.CommodityName = (string)row["CommodityName"].GetString();
            this.SpecCode = (string)row["SpecCode"].GetString();
            this.SpecName = (string)row["SpecName"].GetString();
            this.Gravity = (decimal)row["Gravity"].GetDecimal();
            this.CoatingCode = string.IsNullOrEmpty(row["CoatingCode"].GetString()) ? "" : row["CoatingCode"].GetString();
            this.CoatingName = (string)row["CoatingName"].GetString();
            this.FrontPlate = (decimal)row["FrontPlate"].GetDecimal();
            this.BackPlate = (decimal)row["BackPlate"].GetDecimal();
        }
    }
}