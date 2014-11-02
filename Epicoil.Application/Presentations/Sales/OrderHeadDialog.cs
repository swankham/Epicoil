using Epicoil.Library.Models;
using Epicoil.Library.Models.Sales;
using Epicoil.Library.Repositories.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Epicoil.Appl.Presentations.Sales
{
    public partial class OrderHeadDialog : BaseSession
    {
        private readonly ISaleOrderRepo _repo;
        public OrderHeadModel _selected;
        private IEnumerable<OrderHeadModel> list;
        public OrderHeadDialog(SessionInfo _session, IEnumerable<OrderHeadModel> model)
        {
            InitializeComponent();
            _repo = new SaleOrderRepo();
            _selected = new OrderHeadModel();
            list = model;
        }

        private void SetGrid(IEnumerable<OrderHeadModel> data)
        {
            dgvList.Rows.Clear();
            int i = 0;
            foreach (var p in data)
            {
                dgvList.Rows.Add(p.OrderNum, p.OrderNum, p.OrderDate, p.CustID + " - " + p.CustomerName
                                ,p.EndUserCode +" - " + p.EndUserName, p.ShipTo +" - " + p.ShipToName
                                , p.CustPO, p.SOCode, p.Term, p.BussinessType, p.OrderType, p.TotalWeight, p.TotalAmount);
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void OrderHeadDialog_Load(object sender, EventArgs e)
        {
            SetGrid(list);
        }
    }
}
