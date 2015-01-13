using System.Collections.Generic;
using System.Linq;

namespace Epicoil.Library.Models.Planning
{
    public class SimulateReshearHeadModel
    {
        #region Constructors

        public SimulateReshearHeadModel()
        {
            Materials = new List<MaterialModel>();
            Cuttings = new List<CutDesignModel>();
            SimReshears = new List<SimulateReshearModel>();
        }

        #endregion Constructors

        #region Properties

        public int WorkOrderID { get; set; }

        public string WorkOrderNum { get; set; }

        public decimal RemainWidthOpt1 { get; set; }

        public decimal RemainWidthOpt2 { get; set; }

        public decimal RemainLengthOpt1 { get; set; }

        public decimal RemainLengthOpt2 { get; set; }

        public IList<MaterialModel> Materials { get; set; }

        public IList<CutDesignModel> Cuttings { get; set; }

        public IList<SimulateReshearModel> SimReshears { get; set; }

        #endregion Properties

        #region Methods

        public void SetMaterialRemain(MaterialModel mat)
        {
            RemainWidthOpt1 = mat.Width - SimReshears.Where(i => i.OptionNum == 1 && i.MaterialTransLineID == mat.TransactionLineID).Sum(i => i.WidthActualRemain);
            RemainWidthOpt2 = mat.Width - SimReshears.Where(i => i.OptionNum == 2 && i.MaterialTransLineID == mat.TransactionLineID).Sum(i => i.WidthActualRemain);
            RemainLengthOpt1 = mat.Length - SimReshears.Where(i => i.OptionNum == 1 && i.MaterialTransLineID == mat.TransactionLineID).Sum(i => i.LengthActualRemain);
            RemainLengthOpt2 = mat.Length - SimReshears.Where(i => i.OptionNum == 2 && i.MaterialTransLineID == mat.TransactionLineID).Sum(i => i.LengthActualRemain);
        }

        #endregion Methods
    }
}