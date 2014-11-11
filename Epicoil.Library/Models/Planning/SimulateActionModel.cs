using System;
using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Models.Planning
{
    public class SimulateActionModel
    {
        public int WorkOrderID { get; set; }

        public string WorkOrderNum { get; set; }

        public decimal MaterialWeight { get; set; }

        public decimal ProductWeight { get; set; }

        public decimal TrimWeight { get; set; }

        public int SimulateOption { get; set; }

        public string OptionName
        {
            get
            {
                return Enum.GetName(typeof(SimulateOpt), SimulateOption);
            }
        }

        public decimal Yield { get; set; }

        public int CutSeleted { get; set; }

        public decimal Expected { get; set; }

        private IEnumerable<MaterialModel> MaterialLines = new List<MaterialModel>();
        private IEnumerable<SimulateModel> CuttingLines = new List<SimulateModel>();

        public List<MaterialModel> Materials
        {
            get { return this.MaterialLines.ToList(); }
            set { this.MaterialLines = value; }
        }

        public List<SimulateModel> Cuttings
        {
            get { return this.CuttingLines.ToList(); }
            set { this.CuttingLines = value; }
        }

        public void CalculateRowForWeightOption(MaterialModel mat)
        {
            foreach (var m in Materials.Where(i => i.TransactionLineID.Equals(mat.TransactionLineID)))
            {
                var line = Cuttings.Where(i => i.CutDiv.Equals(CutSeleted) && i.MaterialSerialNo.GetString() == m.SerialNo && i.CalculatedFlag.GetBoolean() == true);
                if (m.UsedFlag && line.ToList().Count == 0)
                {
                    m.UsingWeight = m.UsingWeight + Expected;
                }
                else
                {
                    m.UsingWeight = Expected;
                }
                m.UsedFlag = true;
            }

            foreach (var item in Cuttings.Where(i => i.CutDiv.Equals(CutSeleted)))
            {
                item.CalculateRow(this, mat);
            }

            SumProductWeight();
            SumMaterialWeight();
            SumYeild();
        }

        public void CalculateRowForLegnthOption(MaterialModel mat)
        {
            foreach (var m in Materials.Where(i => i.TransactionLineID.Equals(mat.TransactionLineID)))
            {
                var line = Cuttings.Where(i => i.CutDiv.Equals(CutSeleted) && i.MaterialSerialNo.GetString() == m.SerialNo && i.CalculatedFlag.GetBoolean() == true);
                if (m.UsedFlag && line.ToList().Count == 0)
                {
                    m.UsingLengthM = m.UsingLengthM + Expected;
                }
                else
                {
                    m.UsingLengthM = Expected;
                }
                //m.UsedFlag = true;
            }

            //foreach (var item in Cuttings.Where(i => i.CutDiv.Equals(CutSeleted)))
            //{
            //    item.CalculateRow(this, mat);
            //}

            //SumProductWeight();
            //SumMaterialWeight();
            //SumYeild();
        }

        public void SumProductWeight()
        {
            if (Cuttings.ToList().Count != 0)
            {
                ProductWeight = Math.Round(Cuttings.Sum(i => i.TotalWeight), 0);
            }
            else
            {
                ProductWeight = 0;
            }
        }

        public void SumMaterialWeight()
        {
            if (Materials.ToList().Count != 0)
            {
                MaterialWeight = Math.Round(Materials.Sum(p => p.Weight), 0);
            }
            else
            {
                MaterialWeight = 0;
            }
        }

        public void SumTrimmingWeight(PlanningHeadModel plnHead)
        {
            decimal d1 = Materials.Sum(i => i.UsingWeight);
            decimal w1 = plnHead.CuttingDesign.Where(i => i.Status.Equals("S")).Sum(i => i.Width);
            decimal mw = Materials.Max(i => i.Width);

            decimal result = (d1 / mw) * w1 ;
            TrimWeight = result;
        }

        public decimal CalYeildPercent(decimal WgtFG, decimal WgtMaterial)
        {
            decimal YieldPer = 0;
            WgtMaterial = (WgtMaterial == 0) ? 1 : WgtMaterial;
            YieldPer = Math.Round(Math.Round(WgtFG, 0) / (Math.Round(WgtMaterial, 0)) * 100, 2);
            return YieldPer;
        }

        /// <summary>
        /// Sum Yield percent on header
        /// </summary>
        /// <param name="model"></param>
        public void SumYeild()
        {
            Yield = CalYeildPercent(Math.Round(ProductWeight, 0), Math.Round(MaterialWeight, 0));
        }

        public bool CheckYeild(PlanningHeadModel head, decimal YeildValue)
        {
            decimal YieldMin = head.ProcessLineDetail.YieldPercentMin;
            decimal YieldMax = head.ProcessLineDetail.YieldPercentMax;

            bool FlagYield = true;
            if (YeildValue < YieldMin)
            {
                FlagYield = false;
            }
            if (YeildValue > YieldMax)
            {
                FlagYield = false;
            }
            return FlagYield;
        }

        public bool ValidateToCal(out string msg)
        {
            bool valid = true;
            msg = "";

            var result1 = Cuttings.Where(i => i.CutDiv.Equals(CutSeleted) && i.CalculatedFlag == true).ToList();
            if (result1.Count > 0)
            {
                msg = @"This Cut Division has already calculated.";
                return false;
            }

            return valid;
        }

        public bool ValidateToConfirm(out string msg)
        {
            bool valid = true;
            msg = "";

            var result2 = Cuttings.Where(i => i.CalculatedFlag == false).ToList();
            if (result2.Count > 0)
            {
                msg = @"Please Calculate all line to complete.";
                return false;
            }

            return valid;
        }
    }
}