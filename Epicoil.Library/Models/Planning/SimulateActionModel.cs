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
    }
}