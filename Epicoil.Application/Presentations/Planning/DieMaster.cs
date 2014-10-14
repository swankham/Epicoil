using System;
using System.Drawing;

using Epicoil.Appl;
using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories.Planning;
using Epicoil.Library.Repositories.Common;

namespace Epicoil.Appl.Presentations.Planning
{
    public partial class DieMaster : BaseSession
    {
        private readonly IDieMasterRepo _repo;

        private DieModel DieHeader;

        public DieMaster(SessionInfo _session = null, DieModel model = null)
        {
            InitializeComponent();
            this._repo = new DieMasterRepo();
            epiSession = _session;
            DieHeader = model;
        }

        private void DieMaster_Load(object sender, EventArgs e)
        {
            InitailContent();
        }

        private void InitailContent()
        {
            SetGrid();
        }

        private void SetGrid()
        {
            var result = _repo.GetDieAll(epiSession.PlantID);
            dataGridView1.Rows.Clear();
            int i = 0;
            foreach (var p in result)
            {
                dataGridView1.Rows.Add(p.DieCode, p.DieName);
                if (i % 2 == 1)
                {
                    this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}