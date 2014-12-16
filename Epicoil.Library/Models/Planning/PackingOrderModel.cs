using System;
using System.Collections.Generic;
using System.Data;

namespace Epicoil.Library.Models.Planning
{
    public class PackingOrderModel
    {
        #region Constructors

        public PackingOrderModel()
        {
            PackStyles = new List<PackStyleOrderModel>();
            SerialLines = new List<SerialsPackingModel>();
            SkidPacks = new List<SkidPackingModel>();
        }

        #endregion Constructors

        #region Properties

        public int Id { get; set; }

        public int WorkOrderId { get; set; }

        public string WorkOrderNum { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime IssueDate { get; set; }

        public string PackOrderNum { get; set; }

        public int CompleteFlag { get; set; }

        public string Remark { get; set; }

        public IList<PackStyleOrderModel> PackStyles { get; set; }

        public IList<SerialsPackingModel> SerialLines { get; set; }

        public IList<SkidPackingModel> SkidPacks { get; set; }

        public string CreationBy { get; set; }
        public DateTime CreationDate { get; set; }

        #endregion Properties

        #region Methods

        public void DataBind(DataRow row)
        {
            this.Id = (int)row["LineID"].GetInt();
            this.WorkOrderId = (int)row["WorkOrderID"].GetInt();
            this.WorkOrderNum = (string)row["WorkOrderNum"].GetString();
            this.PackOrderNum = (string)row["PackOrderNum"].GetString();
            this.Remark = (string)row["Remarks"].GetString();
            this.CompleteFlag = (int)row["CompleteFlag"].GetInt();
            this.DueDate = (DateTime)row["DueDate"].GetDate();
            this.IssueDate = (DateTime)row["IssueDate"].GetDate();
        }

        #endregion Methods
    }

    public class PackStyleOrderModel
    {
        #region Properties

        public int Id { get; set; }

        public int HeadLineID { get; set; }

        public string CustId { get; set; }

        public string CustomerName { get; set; }

        public decimal ThickMin { get; set; }

        public decimal ThickMax { get; set; }

        public decimal WidthMin { get; set; }

        public decimal WidthMax { get; set; }

        public decimal CoilWeigthPackMin { get; set; }

        public decimal CoilWeigthPackMax { get; set; }

        public decimal CoilPerPackMin { get; set; }

        public decimal CoilPerPackMax { get; set; }

        public string Size
        {
            get
            {
                string t = (ThickMin == ThickMax) ? ThickMin.ToString("0.00") : ThickMin.ToString("0.00") + "-" + ThickMax.ToString("0.00");
                string w = (WidthMin == WidthMax) ? WidthMin.ToString("0.00") : WidthMin.ToString("0.00") + "-" + WidthMax.ToString("0.00");
                return t + "x" + w + "xC";
            }
        }

        public decimal TotalQuantity { get; set; }

        public string StyleCode { get; set; }

        public string StyleName { get; set; }

        public string Remarks { get; set; }

        #endregion Properties

        #region Methods

        public void DataBind(DataRow row)
        {
            //base.DataBind(row);
            this.Id = (int)row["LineID"].GetInt();
            this.HeadLineID = (int)row["HeadLineID"].GetInt();
            this.CustId = (string)row["CustId"].GetString();
            this.CustomerName = (string)row["CustomerName"].GetString();
            this.ThickMin = (decimal)row["ThickMin"].GetDecimal();
            this.ThickMax = (decimal)row["ThickMax"].GetDecimal();
            this.WidthMin = (decimal)row["WidthMin"].GetDecimal();
            this.WidthMax = (decimal)row["WidthMax"].GetDecimal();
            this.StyleCode = (string)row["StyleCode"].GetString();
            this.StyleName = (string)row["StyleName"].GetString();
            this.TotalQuantity = (decimal)row["TotalQuantity"].GetDecimal();
            this.Remarks = (string)row["Remarks"].GetString();
            this.CoilWeigthPackMin = (decimal)row["CoilWeigthPackMin"];
            this.CoilWeigthPackMax = (decimal)row["CoilWeigthPackMax"];
            this.CoilPerPackMin = (decimal)row["CoilPerPackMin"];
            this.CoilPerPackMax = (decimal)row["CoilPerPackMax"];
        }

        #endregion Methods
    }

    public class SerialsPackingModel : BaseSerial
    {
        #region Properties

        public int Id { get; set; }

        public int PackStyleId { get; set; }

        public int HeadLineId { get; set; }

        public int SerialLineId { get; set; }

        public string SerialNo { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public decimal UnitWeight { get; set; }

        public string PackingStyleCode { get; set; }

        public int PackingDesignId { get; set; }

        public string SkidNumber { get; set; }

        public int CompleteFlag { get; set; }

        public bool DesignedPack
        {
            get
            {
                return (PackingDesignId == 0) ? false : true;
            }
        }

        #endregion Properties

        #region Method

        public void DataBind(DataRow row)
        {
            base.DataBind(row);

            this.Id = (int)row["LineID"].GetInt();
            this.PackStyleId = (int)row["PackStyleLineID"].GetInt();
            this.HeadLineId = (int)row["HeadLineID"].GetInt();
            this.SerialLineId = (int)row["SerialLineID"].GetInt();
            this.SerialNo = (string)row["SerialNo"].GetString();
            this.Thick = (decimal)row["Thick"].GetDecimal();
            this.Width = (decimal)row["Width"].GetDecimal();
            this.Length = (decimal)row["Length"].GetDecimal();
            this.UnitWeight = (decimal)row["UnitWeight"].GetDecimal();
            this.PackingStyleCode = (string)row["PackingStyleCode"].GetString();
            this.PackingDesignId = (int)row["PackingDesignId"].GetInt();
            this.SkidNumber = (string)row["SkidNumber"].GetString();
            this.CompleteFlag = (int)row["CompleteFlag"].GetInt();
        }

        #endregion Method
    }

    public class SkidPackingModel
    {
        #region Constructors

        public SkidPackingModel()
        {
            SerailsPack = new List<SerialsPackingModel>();
        }

        #endregion Constructors

        #region Properties

        public int Id { get; set; }

        public int HeadLineID { get; set; }

        public int PackStyleId { get; set; }

        public int Seq { get; set; }

        public string SkidNumber { get; set; }

        public string Description { get; set; }

        public IList<SerialsPackingModel> SerailsPack { get; set; }

        #endregion Properties

        #region Methods

        public void DataBind(DataRow row)
        {
            this.Id = (int)row["LineID"].GetInt();
            this.HeadLineID = (int)row["HeadLineID"].GetInt();
            this.PackStyleId = (int)row["PackStyleLineID"].GetInt();
            this.Seq = (int)row["Seq"].GetInt();
            this.SkidNumber = (string)row["SkidNumber"].GetString();
            this.Description = (string)row["Description"].GetString();
        }

        #endregion Methods
    }
}