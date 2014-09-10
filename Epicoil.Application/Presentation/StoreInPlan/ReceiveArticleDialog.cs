using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.StoreInPlan;
using Epicoil.Library.Repositories.StoreInPlan;
using Epicoil.Appl;

namespace Epicoil.Appl.Presentations.StoreInPlan
{
    public partial class ReceiveArticleDialog : BaseSession
    {
        private readonly IStoreInPlanRepo _repo;
        private IEnumerable<StoreInPlanDetail> ArticleList;
        private StoreInPlanDetail lineNum;
        private int numofline;
        private decimal WeightRcv;
        private bool DataFlag;

        public ReceiveArticleDialog(StoreInPlanDetail model, SessionInfo _session, int LineReceive, decimal WeightReceive)
        {
            InitializeComponent();
            this._repo = new StoreInPlanRepo();
            this.ArticleList = new List<StoreInPlanDetail>();
            this.lineNum = model;
            this.numofline = LineReceive;
            this.WeightRcv = WeightReceive;
            this.DataFlag = false;
            epiSession = _session;
        }

        private void ReceiveArticleDialog_Load(object sender, EventArgs e)
        {
            txtPONumber.Text = lineNum.PONumber;
            txtPOLine.Text = lineNum.POLine.ToString();
            txtSaleContract.Text = lineNum.SaleContract;
            txtWeightBalnce.Text = this.WeightRcv.ToString("#,##0.00#");
            if (lineNum.Length != 0)
            {
                dataGridView1.Columns[7].HeaderText = "Qty/Pack";
            }
            ListToGrid();
            CalculateSum();
        }

        private void ListToGrid()
        {
            decimal splitWeight = 0; //WeightRcv / numofline;
            int seq = _repo.GetLastSeqId(lineNum.StoreInPlanId, lineNum.PONum, lineNum.POLine);
            for (int i = 0; i < this.numofline; i++)
            {
                dataGridView1.Rows.Add(seq, lineNum.CommodityCode, lineNum.SpecCode, lineNum.Thick, lineNum.Width
                                       , lineNum.Length, "", 1, splitWeight, "", lineNum.PackingNumber, "", lineNum.EndUserName, lineNum.ActlEndUserName);
                seq++;
            }
        }

        private decimal SumWeightToGrid(out decimal quantity)
        {
            decimal num = 0M;
            quantity = 0M;
            for (int i = 0; (this.dataGridView1.Rows.Count) > i; i++)
            {
                decimal d;
                if (!decimal.TryParse(Convert.ToString(dataGridView1.Rows[i].Cells["quantity1"].Value.GetString()), out d))
                {
                    quantity += 0;
                }
                else
                {
                    quantity += decimal.Parse(dataGridView1.Rows[i].Cells["quantity1"].Value.GetString()).GetDecimal();
                }

                if (!decimal.TryParse(Convert.ToString(dataGridView1.Rows[i].Cells["weight1"].Value.GetString()), out d))
                {
                    num += 0;
                }
                else
                {
                    num += decimal.Parse(dataGridView1.Rows[i].Cells["weight1"].Value.GetString()).GetDecimal();
                }
            }
            return num;
        }

        private bool ValidateContent()
        {
            label5.Text = "";
            bool error = false;
            for (int i = 0; (this.dataGridView1.Rows.Count) > i; i++)
            {
                for (int j = 0; (this.dataGridView1.Columns.Count) > j; j++)
                {
                    if (j == 6 || j == 7 || j == 8 || j == 9)
                    {
                        try
                        {
                            DataGridViewRow row = dataGridView1.Rows
                                .Cast<DataGridViewRow>()
                                .Where(r => r.Cells["articleno"].Value.ToString().Equals(dataGridView1.Rows[i].Cells["articleno"].Value.ToString())).First();

                            int rowIndex = row.Index;
                            if (rowIndex != i)
                            {
                                dataGridView1.CurrentCell = dataGridView1[j, i];
                                label5.Text = "Article No is duplicate.";
                                return true;
                            }

                            if (string.IsNullOrEmpty(dataGridView1[j, i].Value.ToString()))
                            {
                                dataGridView1.CurrentCell = dataGridView1[j, i];
                                label5.Text = "Please fill data on current focus.";
                                return true;
                            }
                            else if (_repo.CheckArticleExisting(dataGridView1.Rows[i].Cells["articleno"].Value.ToString()) && dataGridView1.Columns[6].Name == "articleno")
                            {
                                dataGridView1.CurrentCell = dataGridView1[j, i];
                                label5.Text = "This Article number is duplicate.";
                                return true;
                            }
                            else
                            {
                                label5.Text = "";
                            }
                        }
                        catch (Exception)
                        {
                            return true;
                        }
                    }
                }
            }

            if (Convert.ToDecimal(txtWeightBalnce.Text) != Convert.ToDecimal(txtRemainingWeight.Text))
            {
                error = true;
                label5.Text = "Receipt weight must be equal Actual receipt weight!.";
            }
            else
            {
                label5.Text = "";
            }
            return error;
        }

        private void dataGridView1_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            CalculateSum();
        }

        private void CalculateSum()
        {
            decimal quantiry = 0M;
            decimal sumWeight = SumWeightToGrid(out quantiry);
            txtRemainingWeight.Text = sumWeight.ToString("#,##0.00#");
            this.DataFlag = false;
        }

        private void tlbInactive_Click(object sender, EventArgs e)
        {
            //dataGridView1.Refresh();
            if (ValidateContent())
            {
                this.DataFlag = false;
            }
            else
            {
                this.DataFlag = true;
            }
        }

        private void dataGridView1_RowsRemoved(object sender, System.Windows.Forms.DataGridViewRowsRemovedEventArgs e)
        {
            CalculateSum();
        }

        private void dataGridView1_CellValidating(object sender, System.Windows.Forms.DataGridViewCellValidatingEventArgs e)
        {
            //dataGridView1.CellEndEdit;
            this.DataFlag = false;
            if (e.ColumnIndex == 8 || e.ColumnIndex == 7) // 1 should be your column index
            {
                decimal i;
                if (!decimal.TryParse(Convert.ToString(e.FormattedValue), out i))
                {
                    e.Cancel = true;
                    label5.Text = "Please enter numeric.";
                }
                else if (Convert.ToString(e.FormattedValue) == "0")
                {
                    e.Cancel = true;
                    label5.Text = "Can't fill be zero.";
                }
                else
                {
                    label5.Text = "";
                }
            }
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            tlbInactive_Click(sender, e);
            if (!DataFlag)
            {
                MessageBox.Show("Plaes validate data before save.", "Invalid data.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (DataFlag)
            {
                for (int i = 0; (this.dataGridView1.Rows.Count) > i; i++)
                {
                    StoreInPlanDetail param = new StoreInPlanDetail();
                    param.SeqId = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString().Trim());
                    param.StoreInPlanId = lineNum.StoreInPlanId;
                    param.LineID = 0;
                    param.PONumber = lineNum.PONumber;
                    param.PONum = lineNum.PONum;
                    param.POLine = lineNum.POLine;
                    param.SpecCode = lineNum.SpecCode;
                    param.Thick = lineNum.Thick;
                    param.Width = lineNum.Width;
                    param.Length = lineNum.Length;
                    param.ArticleNo = dataGridView1.Rows[i].Cells[6].Value.ToString().Trim();
                    param.Quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells[7].Value.ToString().Trim());
                    param.Weight = Convert.ToDecimal(dataGridView1.Rows[i].Cells[8].Value.ToString().Trim());
                    param.Place = dataGridView1.Rows[i].Cells[9].Value.ToString().Trim();
                    param.PackingNumber = dataGridView1.Rows[i].Cells[10].Value.ToString().Trim();
                    param.Note = dataGridView1.Rows[i].Cells[11].Value.ToString().Trim();
                    param.EndUserID = dataGridView1.Rows[i].Cells[12].Value.ToString().Trim();

                    _repo.SaveArticle(param, epiSession);
                }
                this.Close();
            }
        }
    }
}