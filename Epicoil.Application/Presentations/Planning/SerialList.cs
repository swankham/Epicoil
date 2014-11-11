using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories.Planning;
using System.Collections.Generic;
using System.Drawing;

namespace Epicoil.Appl.Presentations.Planning
{
    public partial class SerialList : BaseSession
    {
        private readonly IWorkEntryRepo _repo;

        private IEnumerable<GeneratedSerialModel> snList;

        public SerialList(SessionInfo _session = null, IEnumerable<GeneratedSerialModel> model = null)
        {
            InitializeComponent();
            this._repo = new WorkEntryRepo();
            epiSession = _session;
            snList = model;
        }

        private void ListGrid(IEnumerable<GeneratedSerialModel> item)
        {
            int i = 0;
            dgvCutting.Rows.Clear();
            foreach (var p in item)
            {
                dgvCutting.Rows.Add(p.SerialNo, p.Thick, p.Width, p.Length, p.LengthM, p.UnitWeight, p.Status
                                    , p.CommodityCode + " - " + p.CommodityName, p.SpecCode + " - " + p.SpecName, p.CoatingCode + " - " + p.CoatingName
                                    , p.BussinessType + " - " + p.BussinessTypeName, p.PossessionName);
                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvCutting.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void SerialList_Load(object sender, System.EventArgs e)
        {
            ListGrid(snList);
        }
    }
}