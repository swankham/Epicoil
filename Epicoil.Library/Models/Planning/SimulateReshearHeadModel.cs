using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Models.Planning
{
    public class SimulateReshearHeadModel
    {
        public int WorkOrderID { get; set; }

        public string WorkOrderNum { get; set; }

        public decimal RemainWidthOpt1 { get; set; }

        public decimal RemainWidthOpt2 { get; set; }

        public decimal RemainLengthOpt1 { get; set; }

        public decimal RemainLengthOpt2 { get; set; }

        private IEnumerable<MaterialModel> MaterialLines = new List<MaterialModel>();
        private IEnumerable<CutDesignModel> CuttingLines = new List<CutDesignModel>();
        private IEnumerable<SimulateReshearModel> SimReshearLines = new List<SimulateReshearModel>();

        public List<MaterialModel> Materials
        {
            get { return this.MaterialLines.ToList(); }
            set { this.MaterialLines = value; }
        }

        public List<CutDesignModel> Cuttings
        {
            get { return this.CuttingLines.ToList(); }
            set { this.CuttingLines = value; }
        }

        public List<SimulateReshearModel> SimReshears
        {
            get { return this.SimReshearLines.ToList(); }
            set { this.SimReshearLines = value; }
        }

        public void SetMaterialRemain(MaterialModel mat) 
        {
            RemainWidthOpt1 = mat.Width - SimReshears.Where(i => i.OptionNum == 1 && i.MaterialTransLineID == mat.TransactionLineID).Sum(i => i.WidthActualRemain);
            RemainWidthOpt2 = mat.Width - SimReshears.Where(i => i.OptionNum == 2 && i.MaterialTransLineID == mat.TransactionLineID).Sum(i => i.WidthActualRemain);
            RemainLengthOpt1 = mat.Length - SimReshears.Where(i => i.OptionNum == 1 && i.MaterialTransLineID == mat.TransactionLineID).Sum(i => i.LengthActualRemain);
            RemainLengthOpt2 = mat.Length - SimReshears.Where(i => i.OptionNum == 2 && i.MaterialTransLineID == mat.TransactionLineID).Sum(i => i.LengthActualRemain);
        }
    }
}