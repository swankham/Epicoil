using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace Epicoil.Library.Models.TQA
{
    public class ProductsMasterModel
    {
        //[dbo].[UD12]
        public string Company { get; set; }

        public string Plant { get; set; }

        public string PlantName { get; set; }

        public string Section { get; set; }

        public string SectionName { get; set; }

        public string RequestDate { get; set; }

        //Customer Size Tab
        public string NorNum { get; set; }

        public int Revision { get; set; }

        public string CustId { get; set; }

        public string CustomerName { get; set; }

        public string DestId { get; set; }

        public string DestinationName { get; set; }

        public string EndUser { get; set; }

        public string EndUserName { get; set; }

        public string CommodityCode { get; set; }

        public string CommodityName { get; set; }

        public string SpecCode { get; set; }

        public string SpecName { get; set; }

        public string CoatingCode { get; set; }

        public string CoatingName { get; set; }

        /// <summary>
        /// 0 = Waiting
        /// 1 = On Process
        /// 2 = Acitive
        /// 3 = Rejected
        /// </summary>
        public int NorStatus { get; set; }

        public bool ActiveStatus { get; set; }

        public int SetStatus { get; set; }

        public string Status
        {
            get
            {
                if (NorStatus == 0)
                {
                    return "Waiting Sale GM Approve";
                }
                else if (NorStatus == 1)
                {
                    return "On Process";
                }
                else if (NorStatus == 2)
                {
                    return "Active";
                }
                else if (NorStatus == 3)
                {
                    return "Reject";
                }
                else if (NorStatus == 4)
                {
                    return "Sale GM Reject";
                }
                else if (NorStatus == 6)
                {
                    return "Waiting TQA Approve";
                }
                else // NorStatus == 5
                {
                    return "Inactive";
                }
            }
        }

        public int Possession { get; set; }

        //[DataType(DataType.MultilineText)]
        public string RejectRemark { get; set; }

        public decimal SizeThick { get; set; }

        public decimal SizeWidth { get; set; }

        //[DisplayFormat(DataFormatString = "{0:d2}"; ApplyFormatInEditMode = true)]
        public decimal SizeLength { get; set; }

        public string Size
        {
            get
            {
                string tick = "";
                tick = SizeThick.ToString("#,##0.00");
                string width = "";
                width = SizeWidth.ToString("#,##0.00");
                string length = "";
                length = SizeLength.GetDecimal().ToString("#,##0.00");
                string size = "";
                size = tick + " x " + width + " x " + length;
                return size;
            }
            set { }
        }

        public decimal TolrThick { get; set; }

        public decimal TolrWidth { get; set; }

        public decimal TolrLength { get; set; }

        public bool UccStdJis { get; set; }

        public bool UccStd1 { get; set; }

        public bool UccStd2 { get; set; }

        public string RefNo { get; set; }

        public string AttachFile { get; set; }

        public string Note { get; set; }

        //Product Status Tab
        public int ProductStatus { get; set; }

        public int FrequencyOfUse { get; set; }

        public string OriginalProcess { get; set; }

        public string FinalCoil { get; set; }

        public string MassProPlan { get; set; }

        public string OrderPeriodFirst { get; set; }

        public string OrderPeriodSecond { get; set; }

        //Product Information Tab
        public string BusinessRoute { get; set; }

        public string Consumption { get; set; }

        public bool PartInner { get; set; }

        public bool PartOuter { get; set; }

        public string PartNum { get; set; }

        public string PartName { get; set; }

        public string PartModel { get; set; }

        public string BrandName { get; set; }

        public string Process_ { get; set; }

        //Packing Tab
        public string CoilStyleCode { get; set; }

        public string StyleImg { get; set; }

        public string SheetStyleCode { get; set; }

        public decimal CoilWeigthMin { get; set; }

        public decimal CoilWeigthMax { get; set; }

        public decimal CoilWeigthPackMin { get; set; }

        public decimal CoilWeigthPackMax { get; set; }

        public decimal CoilPerPackMin { get; set; }

        public decimal CoilPerPackMax { get; set; }

        public decimal CoilID { get; set; }

        public decimal CoilOD { get; set; }

        public decimal SheetPackMin { get; set; }

        public decimal SheetPackMax { get; set; }

        public decimal SheetWeigthMin { get; set; }

        public decimal SheetWeigthMax { get; set; }

        //Special Tab
        public string ClassNum { get; set; }

        public string SpecialNote { get; set; }

        public List<MCSS> MasterCoilList { get; set; }

        //[dbo].[ucc_tqa_NorExtension]
        public string PlateName { get; set; }

        public decimal TolrThickPos { get; set; }

        public decimal TolrThickNeg { get; set; }

        public decimal TolrWidthPos { get; set; }

        public decimal TolrWidthNeg { get; set; }

        public decimal TolrLengthPos { get; set; }

        public decimal TolrLengthNeg { get; set; }

        public bool FixDirection { get; set; }

        public int? FixSide { get; set; }

        public string FixDirecSize
        {
            get
            {
                if (FixSide == 0) { return SizeLength.ToString("#,##0.000"); }
                else { return SizeWidth.ToString("#,##0.000"); }
            }
            set { }
        }

        public decimal BurrCoil { get; set; }

        public decimal BurrSheet { get; set; }

        public decimal EdgeWaveVal { get; set; }

        public decimal EdgeWavePercent { get; set; }

        public decimal Camber { get; set; }

        public decimal BurrTrimmber { get; set; }

        public decimal CenterWaveVal { get; set; }

        public decimal CenterWavePercent { get; set; }

        public decimal Bow { get; set; }

        public decimal Bending { get; set; }

        public decimal Telescope { get; set; }

        public decimal DiffThick { get; set; }

        public decimal Diagonal { get; set; }

        public decimal Overlap { get; set; }

        public string Instruction1 { get; set; }

        public string Instruction2 { get; set; }

        public string Instruction3 { get; set; }

        public string Instruction4 { get; set; }

        public string Instruction5 { get; set; }

        //Special Instruction
        //[DataType(DataType.ImageUrl)]
        //public HttpPostedFileBase InstructionImgFile { get; set; }

        public string InstructionImgPath { get; set; }

        //public HttpPostedFileBase InstructionFile { get; set; }

        public string InstructionFilePath { get; set; }

        public string InstructionFormat { get; set; }

        public string History { get; set; }

        public decimal PackingRow { get; set; }

        public int PackingRowSelected { get; set; }

        public decimal PackingColumn { get; set; }

        public decimal PackingID { get; set; }

        public decimal PackingOD { get; set; }

        public string NoteCoil { get; set; }

        public string NoteSheet { get; set; }

        public int KnifeSpecialStatus { get; set; }

        public string Clearance { get; set; }

        public string FixedKnifeSet { get; set; }

        public virtual void DataBind(DataRow row)
        {
            Company = (string)row["Company"];
            RequestDate = (string)row["Date01"].ToString();
            Plant = (string)row["Key5"].ToString();
            //PlantName = (string)row["Key5"].ToString();
            Section = (string)row["SaleSection"].ToString();
            SectionName = (string)row["SaleSectionName"].ToString();
            /*Epicor data*/
            //Possession = Convert.ToInt32(row["Number18"]);
            Possession = Convert.ToInt32(string.IsNullOrEmpty(row["ShortChar13"].GetString()) ? "0" : row["ShortChar13"]);
            RejectRemark = (string)row["Character10"];
            SizeThick = decimal.Round((decimal)row["Number01"], 3);
            SizeWidth = decimal.Round((decimal)row["Number02"], 3);
            SizeLength = decimal.Round((decimal)row["Number03"], 3);
            UccStdJis = Convert.ToBoolean(row["CheckBox10"]);
            UccStd1 = Convert.ToBoolean(row["CheckBox11"]);
            UccStd2 = Convert.ToBoolean(row["CheckBox12"]);
            ActiveStatus = Convert.ToBoolean(row["CheckBox20"]);
            /*-----------*/

            //Header
            NorNum = (string)row["Key1"];
            Revision = Convert.ToInt32(decimal.Round((decimal)row["Number17"], 0));
            CustId = (string)row["ShortChar04"];
            CustomerName = (string)row["CustomerName"].GetString();
            DestId = (string)row["ShortChar06"];
            DestinationName = (string)row["DestinationName"].GetString();
            EndUser = (string)row["ShortChar05"];
            EndUserName = (string)row["EndUserName"].GetString();
            CommodityCode = (string)row["ShortChar07"];
            CommodityName = (string)row["CommodityName"].GetString();
            SpecCode = (string)row["ShortChar08"];
            SpecName = (string)row["SpecName"].GetString();
            CoatingCode = (string)row["ShortChar10"];
            CoatingName = (string)row["CoatingName"].GetString();
            NorStatus = Convert.ToInt32(row["Number16"]);

            //Product Information
            BusinessRoute = (string)row["Character08"];
            Consumption = (string)row["Character09"];
            PartInner = Convert.ToBoolean(row["CheckBox01"]);
            PartOuter = Convert.ToBoolean(row["CheckBox02"]);
            PartNum = (string)row["Character03"];
            PartName = (string)row["Character04"];
            PartModel = (string)row["Character05"];
            BrandName = (string)row["Character06"];
            Process_ = (string)row["ShortChar14"];

            ProductStatus = Convert.ToInt32(row["Number20"]);
            FinalCoil = (string)row["ShortChar16"];
            MassProPlan = (string)row["ShortChar15"];
            OriginalProcess = (string)row["ShortChar12"];
            OrderPeriodFirst = (string)row["ShortChar17"];
            OrderPeriodSecond = (string)row["ShortChar19"];
            FrequencyOfUse = Convert.ToInt32(row["Number19"]);

            //Packing Tab
            CoilStyleCode = (string)row["ShortChar11"];
            StyleImg = (string)row["StyleImg"].GetString();
            SheetStyleCode = (string)row["ShortChar02"];
            //CoilWeigthMin = (decimal)row["Number18"];
            CoilWeigthMin = (decimal)row["Number18"];
            CoilWeigthMax = (decimal)row["Number05"];
            CoilWeigthPackMin = (decimal)row["Number06"];
            CoilWeigthPackMax = (decimal)row["Number07"];
            CoilPerPackMin = (decimal)row["Number08"];
            CoilPerPackMax = (decimal)row["Number09"];
            CoilID = (decimal)row["Number10"];
            CoilOD = (decimal)row["Number11"];
            SheetPackMin = (decimal)row["Number12"];
            SheetPackMax = (decimal)row["Number13"];
            SheetWeigthMin = (decimal)row["Number14"];
            SheetWeigthMax = (decimal)row["Number15"].GetValueWithDefault<decimal>(0M);

            //Special Tab
            //ClassNum = (string)row["Number17"];//[ShortChar08] [nvarchar](50) NULL;
            SpecialNote = (string)row["Character02"].GetValueWithDefault<string>(string.Empty);//[Character02] [nvarchar](max) NULL;

            PlateName = row["PlateName"].GetString(); //[decimal](18; 3) NULL;
            TolrThickPos = row["TolrThickPos"].GetDecimal(); //[decimal](18; 3) NULL;
            TolrThickNeg = row["TolrThickNeg"].GetDecimal(); //[decimal](18; 3) NULL;
            TolrWidthPos = row["TolrWidthPos"].GetDecimal(); //[decimal](18; 3) NULL;
            TolrWidthNeg = row["TolrWidthNeg"].GetDecimal(); //[decimal](18; 3) NULL;
            TolrLengthPos = row["TolrLengthPos"].GetDecimal(); //[decimal](18; 3) NULL;
            TolrLengthNeg = row["TolrLengthNeg"].GetDecimal(); //[decimal](18; 3) NULL;
            FixDirection = row["FixDirection"].GetBoolean(); //[tinyint] NULL;
            FixSide = (int)row["FixSide"].GetInt(); //[int] NULL;
            BurrCoil = (decimal)row["BurrCoil"].GetDecimal(); //[decimal](18; 3) NULL;
            BurrSheet = (decimal)row["BurrSheet"].GetDecimal(); //[decimal](18; 3) NULL;
            EdgeWaveVal = (decimal)row["EdgeWaveVal"].GetDecimal(); //[decimal](18; 3) NULL;
            EdgeWavePercent = (decimal)row["EdgeWavePercent"].GetDecimal(); //[decimal](18; 2) NULL;
            Camber = (decimal)row["Camber"].GetDecimal(); //[decimal](18; 3) NULL;
            BurrTrimmber = (decimal)row["BurrTrimmber"].GetDecimal(); //[decimal](18; 3) NULL;
            CenterWaveVal = (decimal)row["CenterWaveVal"].GetDecimal(); //[decimal](18; 3) NULL;
            CenterWavePercent = (decimal)row["CenterWavePercent"].GetDecimal(); //[decimal](18; 3) NULL;
            Bow = (decimal)row["Bow"].GetDecimal(); //[decimal](18; 3) NULL;
            Bending = (decimal)row["Bending"].GetDecimal(); //[decimal](18; 3) NULL;
            Telescope = (decimal)row["Telescope"].GetDecimal(); //[decimal](18; 3) NULL;
            DiffThick = (decimal)row["DiffThick"].GetDecimal(); //[decimal](18; 3) NULL;
            Diagonal = (decimal)row["Diagonal"].GetDecimal(); //[decimal](18; 3) NULL;
            Overlap = (decimal)row["Overlap"].GetDecimal(); //[decimal](18; 3) NULL;
            Instruction1 = (string)row["Instruction1"].GetString(); //[nvarchar](max) NULL;
            Instruction2 = (string)row["Instruction2"].GetString(); //[nvarchar](max) NULL;
            Instruction3 = (string)row["Instruction3"].GetString(); //[nvarchar](max) NULL;
            Instruction4 = (string)row["Instruction4"].GetString(); //[nvarchar](max) NULL;
            Instruction5 = (string)row["Instruction5"].GetString(); //[nvarchar](max) NULL;
            InstructionImgPath = (string)row["InstructionImg"].GetString(); //[nvarchar](max) NULL;
            InstructionFilePath = (string)row["InstructionFile"].GetString(); //[nvarchar](max) NULL;
            InstructionFormat = (string)row["InstructionFormat"].GetString(); //[nvarchar](50) NULL;
            History = (string)row["History"].GetString(); //[nvarchar](max) NULL;
            PackingRow = (decimal)row["PackingRow"].GetDecimal(); //[decimal](18; 3) NULL;
            PackingRowSelected = (int)row["PackingRowSelected"].GetInt(); //[tinyint] NULL;
            PackingColumn = (decimal)row["PackingColumn"].GetDecimal(); //[decimal](18; 3) NULL;
            PackingID = (decimal)row["PackingID"].GetDecimal(); //[decimal](18; 3) NULL;
            PackingOD = (decimal)row["PackingOD"].GetDecimal(); //[decimal](18; 3) NULL;
            NoteCoil = (string)row["NoteCoil"].GetString(); //[nvarchar](max) NULL;
            NoteSheet = (string)row["NoteSheet"].GetString(); //[nvarchar](max) NULL;
            KnifeSpecialStatus = row["KnifeSpecialStatus"].GetInt(); //[tinyint] NULL;
            Clearance = (string)row["Clearance"].GetString(); //[nvarchar](50) NULL;
            FixedKnifeSet = (string)row["FixedKnifeSet"].GetString(); //[nvarchar](50) NULL
        }
    }
}