using System;
using System.Drawing;
using Epicoil.Appl;
using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories.Planning;
using Epicoil.Library.Repositories.Common;
using System.Windows.Forms;

namespace Epicoil.Appl.Presentations.Planning
{
    public partial class DieMaster : BaseSession
    {
        private readonly IDieMasterRepo _repo;
        //private static SessionInfo epiSession;
        private DieModel DieHeader;

        public DieMaster(SessionInfo _session, DieModel model = null)
        {
            InitializeComponent();
            this._repo = new DieMasterRepo();
            epiSession = _session;
            //epiSession = _session;
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

        private void dataGridView1_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DieHeader = _repo.GetByID(epiSession.PlantID, this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim());
                if (DieHeader != null)
                {
                    SetHeaderContent(DieHeader);
                }
            }
        }
        private void ClearHeaderContent()
        {
            txtDieCode.DataBindings.Clear();
            txtDieName.DataBindings.Clear();
            txtPattern.DataBindings.Clear();
            txtPatternRemark.DataBindings.Clear();
            txtStrokePcs.DataBindings.Clear();
            txtDieRemark.DataBindings.Clear();
        }

        private void SetHeaderContent(DieModel  model)
        {
            ClearHeaderContent();
            txtDieCode.DataBindings.Add("Text", model, "DieCode", false, DataSourceUpdateMode.OnPropertyChanged);
            txtDieName.DataBindings.Add("Text", model, "DieName", false, DataSourceUpdateMode.OnPropertyChanged);

            txtPattern.DataBindings.Add("Text", model, "Pattern.PatternID", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPatternRemark.DataBindings.Add("Text", model, "Pattern.Remark", false, DataSourceUpdateMode.OnPropertyChanged);
            txtStrokePcs.DataBindings.Add("Text", model, "Pattern.StrokePerPcs", false, DataSourceUpdateMode.OnPropertyChanged);

            txtDieRemark.DataBindings.Add("Text", model, "DieRemark", false, DataSourceUpdateMode.OnPropertyChanged);          
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            bool err = false;
            DieModel model = new DieModel();
            if (txtDieCode.Text.Trim() == "")
            {
                err = true;
            }
            else
            {
                model.DieCode = txtDieCode.Text.Trim();
            }

            if (txtDieName.Text.Trim() == "")
            {
                err = true;
            }
            else
            {
                model.DieName = txtDieName.Text.Trim();
            }

            if (txtPattern.Text.Trim() == "")
            {
                err = true;
            }
            else
            {
                model.PatternID = txtPattern.Text.Trim();
            }
                  
            model.DieRemark = txtDieRemark.Text.Trim();
            
            if (err == false)
            {
                var result = _repo.Save(DieHeader, epiSession);
                SetGrid();
            }
            else
            {
                MessageBox.Show("sss");
            }
        }

        private void tblNew_Click(object sender, EventArgs e)
        {
            string dieID = "";
            int dieCode = 1;
            dieID = _repo.MaxID();
            dieID = dieID.Substring(3, 4);
            int ignoreMe;
            bool successfullyParsed = int.TryParse(dieID, out ignoreMe);
            if (successfullyParsed)
            {
                dieCode = Convert.ToInt32(dieID) + 1;
            }

            DieHeader = new DieModel();
            DieHeader.DieCode = "DIE" + dieCode.ToString("0000");
            SetHeaderContent(DieHeader);
            
        }

        private void txtPattern_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPattern_Leave(object sender, EventArgs e)
        {
            if (txtPattern.Text.ToString().Trim() != "")
            {
                var result = _repo.GetDiePattern(txtPattern.Text.ToString().Trim());
                if (result != null)
                {
                    DieHeader.PatternID = result.PatternID;
                    DieHeader.Pattern.Remark = result.Remark;
                    DieHeader.Pattern.StrokePerPcs = result.StrokePerPcs;
                    SetHeaderContent(DieHeader);
                }
            }
        }

        private void btnPattern_Click(object sender, EventArgs e)
        {
            using (DiePatternMaster frm = new DiePatternMaster(epiSession))
            {
                frm.ShowDialog();
                DieHeader.PatternID = frm.PatternPara.ToString();
                DieHeader.Pattern.PatternID = DieHeader.PatternID;
                DieHeader.Pattern.Remark = frm.RemarkPara.ToString();
                DieHeader.Pattern.StrokePerPcs = frm.StorePerPcsPara.ToString();
                SetHeaderContent(DieHeader);
            }
        }
    }
}