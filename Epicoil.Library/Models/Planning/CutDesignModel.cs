﻿using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.Planning;
using System;
using System.Data;
using System.Linq;

namespace Epicoil.Library.Models.Planning
{
    public class CutDesignModel
    {
        #region Fields

        private readonly IWorkEntryRepo _repo;
        private readonly IResourceRepo _repoRes;
        private readonly IUserCodeRepo _repoUcd;

        #endregion Fields

        #region Constructors

        public CutDesignModel()
        {
            this._repo = new WorkEntryRepo();
            this._repoUcd = new UserCodeRepo();
            this._repoRes = new ResourceRepo();
        }

        #endregion Constructors

        #region Properties

        public decimal BackPlate { get; set; }

        public string BussinessType { get; set; }

        public string BussinessTypeName { get; set; }

        public decimal CalQuantity { get; set; }

        public string CoatingCode { get; set; }

        public string CommodityCode { get; set; }

        public bool CompleteRow { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public string CustID { get; set; }

        public string CustomerName { get; set; }

        public int CutDivision { get; set; }

        public int CutSeq { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string Description { get; set; }

        public string DestinationCode { get; set; }

        public string EndUserCode { get; set; }

        public decimal FrontPlate { get; set; }

        public decimal Gravity { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public decimal Length { get; set; }

        public int LineID { get; set; }

        public string MakerCode { get; set; }

        public string MakerName { get; set; }

        public bool MaterialLineID { get; set; }

        public string MillCode { get; set; }

        public string MillName { get; set; }

        public string NORNum { get; set; }

        public string Note { get; set; }

        public decimal Pack { get; set; }

        public string Plant { get; set; }

        public int Possession { get; set; }

        public int ProductStatus { get; set; }

        public decimal QtyPack { get; set; }

        public int SOLine { get; set; }

        public string SONo { get; set; }

        public decimal SOQuantity { get; set; }

        public decimal SOWeight { get; set; }

        public string SpecCode { get; set; }

        public int Stand { get; set; }

        public string Status { get; set; }

        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public decimal Thick { get; set; }

        public decimal TotalLength { get; set; }

        public decimal TotalWeight { get; set; }

        public int TransactionLineID { get; set; }

        public decimal UnitWeight { get; set; }

        public string UpdatedBy { get; set; }

        public decimal Width { get; set; }

        public int WorkOrderID { get; set; }

        #region Custom for Re-shear

        public string Direction { get; set; }

        public decimal QtyPerMaterial { get; set; }

        #endregion Custom for Re-shear

        #endregion Properties

        #region Methods

        public void CalculateRows(PlanningHeadModel head)
        {
            //Fix bug in case Materials is null.
            if (head.ProcessLine.ResourceGrpID == "S")
            {
                decimal widthMat = 0;
                if (head.Materials.ToList().Count > 0) widthMat = head.Materials.Max(p => p.Width);
                UnitWeight = Math.Round(CalUnitWgtByUsingWgt(head.UsingWeight, widthMat, Width), 2) / ((CutDivision == 0) ? 1 : CutDivision);
                TotalWeight = Math.Round(UnitWeight * (CutDivision * Stand), 2);
                decimal matLengthM = head.Materials.Sum(i => i.LengthM);
                decimal matWeight = head.Materials.Sum(i => i.Weight);
                decimal matUsingWeight = head.Materials.Sum(i => i.UsingWeight);
                TotalLength = CalUsingLength(matLengthM, matWeight, matUsingWeight, CutDivision);
            }
            else if (head.ProcessLine.ResourceGrpID == "L")
            {
                UnitWeight = CalUnitWgt(Thick, Width, Length / 1000, Gravity, FrontPlate, BackPlate);
                TotalWeight = UnitWeight * SOQuantity;
                Pack = CalQuantity / ((QtyPack == 0 ? 1 : QtyPack));
                TotalLength = Length * CalQuantity;
            }
        }

        public decimal CalUnitWgt(decimal T, decimal W, decimal L, decimal Gravity, decimal FrontCoat, decimal BackCoat)
        {
            decimal Ma = 0.0M;
            decimal Mb = 0.0M;
            decimal Mc = 0.0M;
            decimal WgtPerUnit = 0;

            Ma = (T * Gravity) + ((FrontCoat + BackCoat) / 1000);
            Mb = (W / 1000) * (L);
            Mc = (Ma * Mb);
            WgtPerUnit = Math.Round(Mc, 2);
            return WgtPerUnit;
        }

        public void DataBind(DataRow row)
        {
            this.Plant = (string)row["Plant"].GetString();
            this.LineID = ((int)row["LineID"].GetInt() == 0) ? 1 : (int)row["LineID"].GetInt();
            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.TransactionLineID = (int)row["TransactionLineID"].GetInt();
            this.CutSeq = (int)row["CutSeq"].GetInt();
            this.SONo = string.IsNullOrEmpty((string)row["SONo"].GetString()) ? "" : (string)row["SONo"].GetString();
            this.SOLine = (int)row["SOLine"].GetInt();
            this.NORNum = string.IsNullOrEmpty((string)row["NORNum"].GetString()) ? "" : (string)row["NORNum"].GetString();
            this.CommodityCode = string.IsNullOrEmpty((string)row["CommodityCode"].GetString()) ? "" : (string)row["CommodityCode"].GetString();
            this.SpecCode = string.IsNullOrEmpty((string)row["SpecCode"].GetString()) ? "" : (string)row["SpecCode"].GetString();
            this.CoatingCode = string.IsNullOrEmpty((string)row["CoatingCode"].GetString()) ? "" : (string)row["CoatingCode"].GetString();
            this.Thick = (decimal)row["Thick"].GetDecimal();
            this.Width = (decimal)row["Width"].GetDecimal();
            this.Length = (decimal)row["Length"].GetDecimal();
            this.Status = (string)row["Status"].GetString();
            this.Stand = (int)row["Stand"].GetInt();
            this.CutDivision = (int)row["CutDivision"].GetInt();
            this.UnitWeight = (decimal)row["UnitWeight"].GetDecimal();
            this.TotalWeight = (decimal)row["TotalWeight"].GetDecimal();
            this.TotalLength = (decimal)row["TotalLength"].GetDecimal();
            this.CustID = string.IsNullOrEmpty((string)row["CustID"].GetString()) ? "" : (string)row["CustID"].GetString();
            this.EndUserCode = string.IsNullOrEmpty((string)row["EndUserCode"].GetString()) ? "" : (string)row["EndUserCode"].GetString();
            this.DestinationCode = string.IsNullOrEmpty((string)row["DestinationCode"].GetString()) ? "" : (string)row["DestinationCode"].GetString();
            this.QtyPack = (decimal)row["QtyPack"].GetDecimal();
            this.Pack = (decimal)row["Pack"].GetDecimal();
            this.SOWeight = (decimal)row["SOWeight"].GetDecimal();
            this.SOQuantity = (decimal)row["SOQuantity"].GetDecimal();
            this.CalQuantity = (decimal)row["CalQuantity"].GetDecimal();
            this.DeliveryDate = (DateTime)row["DeliveryDate"].GetDate();
            this.BussinessType = string.IsNullOrEmpty((string)row["BussinessType"].GetString()) ? "" : (string)row["BussinessType"].GetString();
            this.BussinessTypeName = (string)row["BussinessTypeName"].GetString();
            this.Possession = (int)row["Possession"].GetInt();
            this.ProductStatus = (int)row["ProductStatus"].GetInt();
            this.Description = string.IsNullOrEmpty((string)row["Description"].GetString()) ? "" : (string)row["Description"].GetString();
            this.Note = string.IsNullOrEmpty((string)row["Note"].GetString()) ? "" : (string)row["Note"].GetString();
            this.CreationDate = (DateTime)row["CreationDate"].GetDate();
            this.LastUpdateDate = (DateTime)row["LastUpdateDate"].GetDate();
            this.CreatedBy = (string)row["CreatedBy"].GetString();
            this.UpdatedBy = (string)row["UpdatedBy"].GetString();
            this.CompleteRow = Convert.ToBoolean((int)row["CompletedRow"].GetInt());
            this.Gravity = (decimal)row["Gravity"].GetDecimal();
            this.FrontPlate = (decimal)row["FrontPlate"].GetDecimal();
            this.BackPlate = (decimal)row["BackPlate"].GetDecimal();
            this.QtyPerMaterial = (int)row["QtyPerMaterial"].GetInt();
            this.Direction = (string)row["Direction"].GetString();
        }

        public bool ValidateByRow(PlanningHeadModel head, out string risk, out string msg)
        {
            bool valid = true;
            risk = string.Empty;
            msg = string.Empty;
            var r = this.Thick.ToString();

            if (string.IsNullOrEmpty(Status))
            {
                risk = "WARNNING";
                msg = "";
                return false;
            }

            if (head.CuttingDesign.Where(p => p.Status.ToString().Equals("S") && p.LineID != LineID).ToList().Count != 0 && Status == "S")
            {
                risk = "WARNNING";
                msg = "Status 'S' is already exist in cutting lines.";
                return false;
            }

            if (head.Materials.ToList().Count > 0)
            {
                if (head.ProcessLine.ResourceGrpID == "S")
                {
                    decimal totalWidth = (from item in head.CuttingDesign
                                          select item).Sum(i => i.Width * i.Stand);
                    var totalMatWidth = (from mat in head.Materials
                                         select mat).First();

                    if (totalMatWidth.Width < totalWidth)
                    {
                        risk = "ERROR";
                        msg = "Total width in cutting line is greater than material width.";
                        return false;
                    }
                }
            }

            if (head.ProcessLine.ResourceGrpID == "L")
            {
                if (CalQuantity == 0)
                {
                    risk = "WARNNING";
                    msg = "Calculate quantity invalid.";
                    return false;
                }

                if (Pack != (CalQuantity / ((QtyPack == 0) ? 1 : QtyPack)))
                {
                    risk = "WARNNING";
                    msg = "Pack invalid.";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(SONo) && SOLine == 0)
            {
                if (string.IsNullOrEmpty(SONo) || SOLine == 0)
                {
                    risk = "WARNNING";
                    msg = "Please select SO Line.";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(NORNum))
            {
                if (Status == "S")
                {
                    risk = "ERROR";
                    msg = "Please change status = 'F' for S/O line.";
                    return false;
                }
            }

            if (Stand == 0)
            {
                risk = "ERROR";
                msg = "Stand value must be more than zero.";
                return false;
            }

            if (CutDivision == 0)
            {
                risk = "ERROR";
                msg = "Cut Division value must be more than zero.";
                return false;
            }
            return valid;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="MaterialLengthM">Unit is Meter</param>
        /// <param name="MaterialWeight">Unit is Kg.</param>
        /// <param name="MaterialUsingWeight">Unit is Kg.</param>
        /// <param name="CutDiv">Value of cut</param>
        /// <returns></returns>
        private static decimal CalUsingLength(decimal MaterialLengthM, decimal MaterialWeight, decimal MaterialUsingWeight, decimal CutDiv)
        {
            decimal ActualLength = 0.0M;
            if (MaterialWeight > 0 && MaterialUsingWeight > 0 && MaterialLengthM > 0 && CutDiv > 0)
            {
                ActualLength = MaterialUsingWeight * MaterialLengthM / MaterialWeight / CutDiv;
            }
            return ActualLength;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="UsingWgt">Unit is Kg.</param>
        /// <param name="WidthMaterial">Unit is MM.</param>
        /// <param name="WidthFG">Unit is MM.</param>
        /// <returns></returns>
        private decimal CalUnitWgtByUsingWgt(decimal UsingWgt, decimal WidthMaterial, decimal WidthFG)
        {
            decimal CalWeightFG = 0.0M;
            if (UsingWgt > 0 && WidthMaterial > 0 && WidthFG > 0)
            {
                CalWeightFG = Math.Round((UsingWgt / WidthMaterial * WidthFG), 2);
            }
            return CalWeightFG;
        }

        #endregion Methods
    }
}