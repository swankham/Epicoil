using System;
using System.Data;
using System.Collections.Generic;

namespace Epicoil.Library.Models.TQA
{
    public class MCSS : ICloneable
    {
        public bool InsertState { get; set; }

        public string Plant { get; set; }

        public string McssNum { get; set; }

        public string MCSSID { get; set; }

        public int Revision { get; set; }

        public DateTime RequestDate { get; set; }

        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public string CustID { get; set; }

        public string CustomerName { get; set; }

        public string MakerCode { get; set; }

        public string MakerName { get; set; }

        public string MillCode { get; set; }

        public string MillName { get; set; }

        public string MatSpec1 { get; set; }

        public string MatSpec2 { get; set; }

        public string CommodityCode { get; set; }

        public string CommodityName { get; set; }

        public bool CmdtRequireCoating { get; set; }

        public string Coating1 { get; set; }

        public string Coating2 { get; set; }

        public decimal CoatingWeight1 { get; set; }

        public decimal CoatingWeight2 { get; set; }

        public string CategoryGroupHead1 { get; set; }

        public string CategoryGroupHead2 { get; set; }

        public int Pocession { get; set; }

        public decimal POAllowance { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public string Size
        {
            get
            {
                return Thick.ToString("#,##0.###") + Width.ToString("#,##0.###") + Length.ToString("#,##0.###");
            }
        }

        //Condition Tab
        public int CustomerType { get; set; }

        public string CustomerTypeRemark { get; set; }

        public string BussinessType { get; set; }

        public string BussinessTypeName { get; set; }

        public decimal QuantityPerMonth { get; set; }

        public decimal QuantityPerPlant { get; set; }

        public string BusinessRoute { get; set; }

        public string BusinessRemark { get; set; }

        //ToleranceSpec Tab
        public int StandardRef { get; set; }

        public string StandardRefRemark { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public int ThicknessTolerance { get; set; }

        public decimal ThicknessTolerValPos { get; set; }

        public decimal ThicknessTolerValNeg { get; set; }

        public int WidthStandard { get; set; }

        public decimal WidthStdPos { get; set; }

        public decimal WidthStdNeg { get; set; }

        public int Oilling { get; set; }

        public decimal OillingVal { get; set; }

        public string BaseMaterial { get; set; }

        public string Country { get; set; }

        public string Remark { get; set; }

        //Chemical Tab
        public int DistCR { get; set; }

        public string DistCRRemark { get; set; }

        public int DistHR { get; set; }

        public string DistHRRemark { get; set; }

        public int DistGI { get; set; }

        public string DistGIRemark { get; set; }

        public int Stainless { get; set; }   //DistSS

        public string StainlessRemark { get; set; }  //DistSSRemark

        public int ChemPersent { get; set; }

        public string ChemPersentRemark { get; set; }

        public int Yield { get; set; }

        public string YieldRemark { get; set; }

        public int Tensile { get; set; }

        public string TensileRemark { get; set; }

        public int Elongation { get; set; }

        public string ElongationRemark { get; set; }

        public int Hardness { get; set; }

        public string HardnessRemark { get; set; }

        public int CoreLoss { get; set; }

        public string CoreLossRemark { get; set; }

        public int Magnatic { get; set; }

        public string MagnaticRemark { get; set; }

        public int Oriented { get; set; }

        public string OrientedRemark { get; set; }

        //Cust Process Tab
        public bool Welding { get; set; }

        public bool Painting { get; set; }

        public bool ProcessOther { get; set; }

        public string ProcessOtherRemark { get; set; }

        public bool Degreasing { get; set; }

        public bool Blanking { get; set; }

        public bool Commercial { get; set; }

        public bool Drawing { get; set; }

        public bool DeepDrawing { get; set; }

        public bool ExtraDeep { get; set; }

        public bool Folding { get; set; }

        public bool FormingOther { get; set; }

        public string FormingOtherRemark { get; set; }

        public string PartName { get; set; }

        public string ProductName { get; set; }

        public string CusProcessRemark { get; set; }

        public string EndUser1 { get; set; }

        public string EndUser2 { get; set; }

        public string EndUser3 { get; set; }

        public string EndUser4 { get; set; }

        public string CategoryGroup1 { get; set; }

        public string CategoryGroup2 { get; set; }

        public string CategoryGroup3 { get; set; }

        public string CategoryGroup4 { get; set; }

        //Hardous substance Tab
        public int EdgeWave { get; set; }

        public string EdgeWaveRemark { get; set; }

        public int CenterWave { get; set; }

        public string CenterWaveRemark { get; set; }

        public int PackingStyle { get; set; }

        public decimal WeightPerCoilMin { get; set; }

        public decimal WeightPerCoilMax { get; set; }

        public decimal IDCoil { get; set; }

        public decimal ODCoil { get; set; }

        public bool RoHS { get; set; }

        public bool PFOS { get; set; }

        public bool SOC { get; set; }

        public bool ELV { get; set; }

        public bool REACH { get; set; }

        public bool Other { get; set; }

        public string OtherRemark { get; set; }

        public bool TISIFlag { get; set; }

        public string TISINo { get; set; }

        public string LicenseNo { get; set; }

        public IEnumerable<SpecialRef> SpecialRefs { get; set; }

        public void DataBind(DataRow row)
        {
            this.InsertState = false;
            this.Plant = (string)row["Plant"].GetString();
            this.McssNum = (string)row["McssNum"].GetString();
            this.MCSSID = (string)row["ShortChar08"].GetString();
            this.Revision = Convert.ToInt32(row["Revision"].GetDecimal());
            this.RequestDate = (DateTime)row["Date01"].GetDate();
            this.SupplierCode = (string)row["SupplierCode"];
            this.SupplierName = (string)row["SupplierName"].GetString();
            this.CustID = (string)row["CustID"];
            this.CustomerName = (string)row["CustomerName"].GetString();
            this.MakerCode = (string)row["MakerCode"];
            this.MakerName = (string)row["MakerName"].GetString();
            this.MillCode = (string)row["MillCode"];
            this.MillName = (string)row["MillName"].GetString();
            this.MatSpec1 = (string)row["MatSpec1"].GetString();
            this.MatSpec2 = (string)row["MatSpec2"].GetString();
            this.CommodityCode = (string)row["CommodityCode"].GetString();
            this.CommodityName = (string)row["CommodityName"].GetString();
            this.CmdtRequireCoating = Convert.ToBoolean(row["RequireCoating"].GetBoolean());
            this.Coating1 = (string)row["Coating1"].GetString();
            this.Coating2 = (string)row["Coating2"].GetString();
            this.CoatingWeight1 = (decimal)row["CoatingWeight1"].GetDecimal();
            this.CoatingWeight2 = (decimal)row["CoatingWeight2"].GetDecimal();
            this.CategoryGroupHead1 = (string)row["CategoryHedGroup1"].GetString();
            this.CategoryGroupHead2 = (string)row["CategoryHedGroup2"].GetString();
            this.Pocession = (int)row["Procession"].GetInt();
            this.POAllowance = (decimal)row["POAllowance"].GetDecimal();
            this.Thick = (decimal)row["Thick"].GetDecimal();
            this.Width = (decimal)row["Width"].GetDecimal();
            this.Length = (decimal)row["Length"].GetDecimal();
            this.TISIFlag = (bool)row["TISIFlag"].GetBoolean();
            this.TISINo = (string)row["TISINo"].GetString();
            this.LicenseNo = (string)row["LicenseNo"].GetString();

            this.CustomerType = (int)row["CustomerType"].GetInt();
            this.CustomerTypeRemark = (string)row["CustomerTypeRemark"].GetString();
            this.BussinessType = (string)row["BussinessType"].GetString();
            this.BussinessTypeName = (string)row["BussinessTypeName"].GetString();
            this.QuantityPerMonth = (decimal)row["QuantityPerMonth"].GetDecimal();
            this.QuantityPerPlant = (decimal)row["QuantityPerPlant"].GetDecimal();
            this.BusinessRoute = (string)row["BusinessRoute"].GetString();

            this.StandardRef = (int)row["StandardRef"].GetInt();
            this.StandardRefRemark = (string)row["StandardRefRemark"].GetString();
            this.Number = (string)row["Number"].GetString();
            this.Name = (string)row["Name"].GetString();
            this.ThicknessTolerance = (int)row["ThicknessTolerance"].GetInt();
            this.ThicknessTolerValPos = (decimal)row["ThicknessTolerValPos"].GetDecimal();
            this.ThicknessTolerValNeg = (decimal)row["ThicknessTolerValNeg"].GetDecimal();
            this.WidthStandard = (int)row["WidthStandard"].GetInt();
            this.WidthStdPos = (decimal)row["WidthStdPos"].GetDecimal();
            this.WidthStdNeg = (decimal)row["WidthStdNeg"].GetDecimal();
            this.Oilling = (int)row["Oilling"].GetInt();
            this.OillingVal = (decimal)row["OillingVal"].GetDecimal();
            this.BaseMaterial = (string)row["BaseMaterial"].GetString();
            this.Country = (string)row["Country"].GetString();
            this.Remark = (string)row["Remark"].GetString();

            this.DistCR = (int)row["DistCR"].GetInt();
            this.DistCRRemark = (string)row["DistCRRemark"].GetString();
            this.DistHR = (int)row["DistHR"].GetInt();
            this.DistHRRemark = (string)row["DistHRRemark"].GetString();
            this.DistGI = (int)row["DistGI"].GetInt();
            this.DistGIRemark = (string)row["DistGIRemark"].GetString();
            this.Stainless = (int)row["Stainless"].GetInt();
            this.StainlessRemark = (string)row["StainlessRemark"].GetString();
            this.ChemPersent = (int)row["ChemPersent"].GetInt();
            this.ChemPersentRemark = (string)row["ChemPersentRemark"].GetString();
            this.Yield = (int)row["Yield"].GetInt();
            this.YieldRemark = (string)row["YieldRemark"].GetString();
            this.Tensile = (int)row["Tensile"].GetInt();
            this.TensileRemark = (string)row["TensileRemark"].GetString();
            this.Elongation = (int)row["Elongation"].GetInt();
            this.ElongationRemark = (string)row["ElongationRemark"].GetString();
            this.Hardness = (int)row["Hardness"].GetInt();
            this.HardnessRemark = (string)row["HardnessRemark"].GetString();
            this.CoreLoss = (int)row["CoreLoss"].GetInt();
            this.CoreLossRemark = (string)row["CoreLossRemark"].GetString();
            this.Magnatic = (int)row["Magnatic"].GetInt();
            this.MagnaticRemark = (string)row["MagnaticRemark"].GetString();
            this.Oriented = (int)row["Oriented"].GetInt();
            this.OrientedRemark = (string)row["OrientedRemark"].GetString();

            this.Welding = Convert.ToBoolean(row["Welding"].GetInt());
            this.Painting = Convert.ToBoolean(row["Painting"].GetInt());
            this.ProcessOther = Convert.ToBoolean(row["ProcessOther"].GetInt());
            this.ProcessOtherRemark = (string)row["ProcessOtherRemark"].GetString();
            this.Degreasing = Convert.ToBoolean(row["Degreasing"].GetInt());
            this.Blanking = Convert.ToBoolean(row["Blanking"].GetInt());
            this.Commercial = Convert.ToBoolean(row["Commercial"].GetInt());
            this.Drawing = Convert.ToBoolean(row["Drawing"].GetInt());
            this.DeepDrawing = Convert.ToBoolean(row["DeepDrawing"].GetInt());
            this.ExtraDeep = Convert.ToBoolean(row["ExtraDeep"].GetInt());
            this.Folding = Convert.ToBoolean(row["Folding"].GetInt());
            this.FormingOther = Convert.ToBoolean(row["FormingOther"].GetInt());
            this.FormingOtherRemark = (string)row["FormingOtherRemark"].GetString();
            this.PartName = (string)row["PartName"].GetString();
            this.ProductName = (string)row["ProductName"].GetString();
            this.CusProcessRemark = (string)row["CusProcessRemark"].GetString();
            this.EndUser1 = (string)row["EndUser1"].GetString();
            this.EndUser2 = (string)row["EndUser2"].GetString();
            this.EndUser3 = (string)row["EndUser3"].GetString();
            this.EndUser4 = (string)row["EndUser4"].GetString();
            this.CategoryGroup1 = (string)row["CategoryGroup1"].GetString();
            this.CategoryGroup2 = (string)row["CategoryGroup2"].GetString();
            this.CategoryGroup3 = (string)row["CategoryGroup3"].GetString();
            this.CategoryGroup4 = (string)row["CategoryGroup4"].GetString();

            this.EdgeWave = (int)row["EdgeWave"].GetInt();
            this.EdgeWaveRemark = (string)row["EdgeWaveRemark"].GetString();
            this.CenterWave = (int)row["CenterWave"].GetInt();
            this.CenterWaveRemark = (string)row["CenterWaveRemark"].GetString();
            this.PackingStyle = (int)row["PackingStyle"].GetInt();
            this.WeightPerCoilMin = (decimal)row["WeightPerCoilMin"].GetDecimal();
            this.WeightPerCoilMax = (decimal)row["WeightPerCoilMax"].GetDecimal();
            this.IDCoil = (decimal)row["IDCoil"].GetDecimal();
            this.ODCoil = (decimal)row["ODCoil"].GetDecimal();
            this.RoHS = Convert.ToBoolean(row["RoHS"].GetInt());
            this.PFOS = Convert.ToBoolean(row["PFOS"].GetInt());
            this.SOC = Convert.ToBoolean(row["SOC"].GetInt());
            this.ELV = Convert.ToBoolean(row["ELV"].GetInt());
            this.REACH = Convert.ToBoolean(row["REACH"].GetInt());
            this.Other = Convert.ToBoolean(row["Other"].GetInt());
            this.OtherRemark = (string)row["OtherRemark"].GetString();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class SpecialRef
    {
        public string Plant { get; set; }

        public int RefId { get; set; }

        public string McssNum { get; set; }

        public int RefNo { get; set; }

        public string Description { get; set; }

        public string CreateBy { get; set; }

        public void DataBind(DataRow row)
        {
            this.Plant = (string)row["Plant"].GetString();
            this.RefId = (int)row["LineID"].GetInt();
            this.RefNo = (int)row["LineNum"];
            this.McssNum = (string)row["MCSSNum"].GetString();
            this.Description = (string)row["Description"];
        }
    }

    public class InitialModel
    {
        public string McssNum { get; set; }

        public string Key1 { get; set; }

        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public string CustID { get; set; }

        public string CustomerName { get; set; }

        public string MakerCode { get; set; }

        public string MakerName { get; set; }

        public string MillCode { get; set; }

        public string MillName { get; set; }

        public string MatSpec1 { get; set; }

        public string MatSpec2 { get; set; }

        public string CommodityCode { get; set; }

        public string CommodityName { get; set; }

        public decimal Thick { get; set; }

        public decimal Width { get; set; }

        public decimal Length { get; set; }

        public void DataBind(DataRow row)
        {
            this.Key1 = (string)row["Key1"];
            this.SupplierCode = (string)row["SupplierCode"].GetString();
            this.SupplierName = (string)row["SupplierName"].GetString();
            this.CustID = (string)row["CustID"].GetString();
            this.CustomerName = (string)row["CustomerName"].GetString();
            this.MakerCode = (string)row["MakerCode"].GetString();
            this.MakerName = (string)row["MakerName"].GetString();
            this.MillCode = (string)row["MillCode"].GetString();
            this.MillName = (string)row["MillName"].GetString();
            this.MatSpec1 = (string)row["MatSpec1"].GetString();
            this.MatSpec2 = (string)row["MatSpec2"].GetString();
            this.CommodityCode = (string)row["CommodityCode"].GetString();
            this.CommodityName = (string)row["CommodityName"].GetString();
            this.Thick = (decimal)row["Thick"].GetDecimal();
            this.Width = (decimal)row["Width"].GetDecimal();
            this.Length = (decimal)row["Length"].GetDecimal();
        }
    }
}