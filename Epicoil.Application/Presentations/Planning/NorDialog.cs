using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Epicoil.Library.Models;
using Epicoil.Library.Models.TQA;
using Epicoil.Library.Repositories.TQA;

namespace Epicoil.Appl.Presentations.Planning
{
    public partial class NorDialog : BaseSession
    {

        private IEnumerable<ProductsMasterModel> _model;
        public ProductsMasterModel _selected;
        private readonly IProductMasterRepo _repo;

        public NorDialog(SessionInfo _epiSession, IEnumerable<ProductsMasterModel> data)
        {
            InitializeComponent();
            epiSession = _epiSession;
            _model = data;
        }


        private void ListNorGrid(IEnumerable<ProductsMasterModel> item)
        {
            int i = 0;
            dgvNor.Rows.Clear();
            foreach (var p in item)
            {
                dgvNor.Rows.Add(p.NorNum, p.CommodityCode + " - " + p.CommodityName, p.SpecCode + " - " + p.SpecName, p.CoatingCode + " - " + p.CoatingName.GetString()
                                , p.SizeThick, p.SizeWidth, p.SizeLength , p.CustId + " - " + p.CustomerName);

                if (i % 2 == 1)
                {
                    this.dgvNor.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }
        private void butSearch_Click(object sender, EventArgs e)
        {
            var result = this._model;

            if (!string.IsNullOrEmpty(txtCustID.Text)) result = result.Where(p => p.CustId.ToString().ToUpper().Contains(txtCustID.Text.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(txtCommodityCode.Text)) result = result.Where(p => p.CommodityCode.ToString().ToUpper().Contains(txtCommodityCode.Text.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(txtSpecCode.Text)) result = result.Where(p => p.SpecCode.ToString().ToUpper().Contains(txtSpecCode.Text.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(txtCoatingCode.Text)) result = result.Where(p => p.CoatingCode.ToString().ToUpper().Contains(txtCoatingCode.Text.ToString().ToUpper()));

            if (numThickMin.Value != 0) result = result.Where(p => p.SizeThick >= numThickMin.Value);
            if (numThickMax.Value != 0) result = result.Where(p => p.SizeThick <= numThickMax.Value);
            if (numWidthMin.Value != 0) result = result.Where(p => p.SizeWidth >= numWidthMin.Value);
            if (numWidthMax.Value != 0) result = result.Where(p => p.SizeWidth <= numWidthMax.Value);
            if (numLengthMin.Value != 0) result = result.Where(p => p.SizeLength >= numLengthMin.Value);
            if (numLengthMax.Value != 0) result = result.Where(p => p.SizeLength <= numLengthMax.Value);

            ListNorGrid(result);
        }

        private void NorDialog_Load(object sender, EventArgs e)
        {
            ListNorGrid(this._model);
          
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            txtCustID.Clear();
            txtCommodityCode.Clear();
            txtSpecCode.Clear();
            txtCoatingCode.Clear();
            txtMakerCode.Clear();
            txtMillCode.Clear();
            numThickMin.Value = 0;
            numThickMax.Value = 0;
            numWidthMin.Value = 0;
            numWidthMax.Value = 0;
            numLengthMin.Value = 0;
            numLengthMax.Value = 0;
            ListNorGrid(this._model);
        }

        private void butSelect_Click(object sender, EventArgs e)
        {
            if (dgvNor.Rows.Count >= 1)
            {
                int iRow = dgvNor.CurrentRow.Index;
                string NorNo = dgvNor.Rows[iRow].Cells["NorNum"].Value.ToString();

                if (!string.IsNullOrEmpty(NorNo))
                {
                    _selected.NorNum = NorNo;
                    var result = _repo.Get(_selected);
                }

                this.Close();
            }
        }

    }
}
