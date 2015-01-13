using System;
using System.Data;

namespace Epicoil.Library.Models.Planning
{
    public class GeneratedSerialModel : BaseSerial
    {
        #region Properties

        public string Plant { get; set; }

        public string SerialNo { get; set; }

        public int SimLineID { get; set; }

        public int WorkOrderID { get; set; }

        public string WorkOrderNum { get; set; }

        public int CuttingLineID { get; set; }

        public int MaterialTransLineID { get; set; }

        public string MCSSNo { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public decimal LengthM { get; set; }

        public string Status { get; set; }

        public decimal UnitWeight { get; set; }

        public decimal Quantity { get; set; }

        public decimal TotalWeight { get; set; }

        public int GeneratedFlag { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string NORNum { get; set; }

        #endregion Properties

        #region Methods

        public override void DataBind(DataRow row)
        {
            base.DataBind(row);

            this.Plant = (string)row["Plant"].GetString();
            this.SerialNo = (string)row["SerialNo"].GetString();
            this.SimLineID = (int)row["SimLineID"].GetInt();
            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.WorkOrderNum = (string)row["WorkOrderNum"].GetString();
            this.CuttingLineID = (int)row["CuttingLineID"].GetInt();
            this.MaterialTransLineID = (int)row["MaterialTransLineID"].GetInt();
            this.Thick = (decimal)row["Thick"].GetDecimal();
            this.Width = (decimal)row["Width"].GetDecimal();
            this.Length = (decimal)row["Length"].GetDecimal();
            this.LengthM = (decimal)row["LengthM"].GetDecimal();
            this.Status = (string)row["Status"].GetString();
            this.UnitWeight = (decimal)row["UnitWeight"].GetDecimal();
            this.Quantity = (decimal)row["Quantity"].GetDecimal();
            this.TotalWeight = (decimal)row["TotalWeight"].GetDecimal();
            this.GeneratedFlag = (int)row["GeneratedFlag"].GetInt();
            this.CreationDate = (DateTime)row["CreationDate"].GetDate();
            this.LastUpdateDate = (DateTime)row["LastUpdateDate"].GetDate();
            this.CreatedBy = (string)row["CreatedBy"].GetString();
            this.UpdatedBy = (string)row["UpdatedBy"].GetString();
            this.MCSSNo = (string)row["MCSSNo"].GetString();
            this.NORNum = string.IsNullOrEmpty((string)row["NORNum"].GetString()) ? "" : (string)row["NORNum"].GetString();
        }

        #endregion Methods
    }
}