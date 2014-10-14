using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Epicoil.Library.Repositories;
using Epicoil.Library.Models;

namespace Epicoil.Appl.Presentations
{
    public partial class SaleSectionDialog : BaseSession
    {
        private readonly ISaleSectionRepo _repo;
        public string Code;
        public string Description;

        public SaleSectionDialog()
        {
            InitializeComponent();
            this._repo = new SaleSectionRepo();
            this.Code = "";
            this.Description = "";
        }

        private void SaleSectionDialog_Load(object sender, EventArgs e)
        {
            var result = _repo.GetAll("MfgSys");               // var any type 
            //dgvList.DataSource = result;
            SetGrid(result);

        }
        private void SetGrid(IEnumerable<SaleSectionModel> list)
        {
            dgvList.Rows.Clear();
            foreach(var item in list)
            {
                dgvList.Rows.Add(item.SaleSectCode, item.SaleSectDesc, item.Capacity, item.Plant);
            }      
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            SaleSectionModel model = new SaleSectionModel();
            model.SaleSectCode = txtFilter.Text;
            model.Plant = "MfgSys";

            var result = _repo.GetByFilet(model);
            // var any type 
            //dgvList.DataSource = result;
            SetGrid(result);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Code = dgvList.Rows[e.RowIndex].Cells["SaleSectionCode"].Value.ToString();
            Description = dgvList.Rows[e.RowIndex].Cells["SaleSectionDesc"].Value.ToString();
            MessageBox.Show("Code" + Code + " Desc " + Description);
            this.Close();
        }
    }
}
