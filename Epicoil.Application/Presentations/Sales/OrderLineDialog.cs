﻿using Epicoil.Library.Models;
using Epicoil.Library.Models.Sales;
using Epicoil.Library.Repositories.Sales;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Epicoil.Appl.Presentations.Sales
{
    public partial class OrderLineDialog : BaseSession
    {
        private readonly ISaleOrderRepo _repo;
        public OrderDetailModel _selected;
        private IEnumerable<OrderDetailModel> listAll;

        public OrderLineDialog(SessionInfo _session, IEnumerable<OrderDetailModel> model)
        {
            InitializeComponent();
            _repo = new SaleOrderRepo();
            _selected = new OrderDetailModel();
            this.listAll = new List<OrderDetailModel>();
            listAll = model;
            epiSession = _session;
        }

        private void SetGrid(IEnumerable<OrderDetailModel> data)
        {
            if (data.ToList().Count == 0) return;
            dgvList.Rows.Clear();
            int i = 0;
            foreach (var p in data)
            {
                dgvList.Rows.Add(p.OrderNum, p.OrderLine, p.NORNo, p.PossessionName, p.CommodityCode + " - " + p.CommodityName
                                , p.SpecCode + " - " + p.SpecName, p.CoatingCode + " - " + p.CoatingName
                                , p.BussinessType + " - " + p.BussinessTypeName, p.Thick, p.Width, p.Length
                                , p.SOWeight, p.SOAmount);
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void OrderLineDialog_Load(object sender, EventArgs e)
        {
            //var ord = listAll.FirstOrDefault();
            //textBox2.Text = ord.OrderNum.ToString();
            SetGrid(listAll);
        }

        private void butSelect_Click(object sender, EventArgs e)
        {
            if (dgvList.Rows.Count >= 1)
            {
                int iRow = dgvList.CurrentRow.Index;
                string orderID = dgvList.Rows[iRow].Cells["ordernum"].Value.ToString();
                string lineID = dgvList.Rows[iRow].Cells["orderline"].Value.ToString();

                if (!string.IsNullOrEmpty(orderID))
                {
                    _selected = _repo.GetOrderDtlByID(orderID, Convert.ToInt32(lineID));
                    this.Close();
                }
            }
        }

        private void dgvList_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            butSelect_Click(sender, e);
        }
    }
}