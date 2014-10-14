using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using Epicor.Mfg.BO;
using Epicor.Mfg.Core;

using Epicoil.Library.Models;
using Epicoil.Library.Models.StoreInPlan;
using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.Common;
using Epicoil.Library.Repositories.StoreIn;
using Epicoil.Library.Repositories.StoreInPlan;

namespace Epicoil.Appl.Presentations
{
    public partial class Progression : BaseSession
    {
        private readonly IStoreInPlanRepo _repoPlan;
        private readonly IStoreInRepo _repo;
        private SessionInfo _session;
        private int Possession;

        private decimal TransactionID;

        int MinVal;
        int MaxVal;
        bool Completed;

        public Progression(string moduleName, decimal tranactionID, SessionInfo session, int possession)
        {
            InitializeComponent();
            this._repoPlan = new StoreInPlanRepo();
            this._repo = new StoreInRepo();
            this._session = session;
            this.Possession = possession;

            this.TransactionID = tranactionID;

            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            prgBar.Minimum = 0;
        }

        private void Progression_Load(object sender, EventArgs e)
        {
            bool success;
            string msg;
            var result = _repo.GetNewPartCollection(TransactionID);

            prgBar.Minimum = 0;
            prgBar.Maximum = result.Count();

            int i = 0;
            if (result != null)
            {
                //Initial Session to Epicor
                Session currSession = new Session(_session.UserID, _session.UserPassword, _session.AppServer, Session.LicenseType.Default);

                //Loop to get new part in Epicor

                foreach (var item in result)
                {
                    item.iRunning = _repo.RunningPart();
                    item.SerialNo = _repo.GetSerialByFormat(item.iRunning);// +"-DEMO";
                    lblDescription.Text = "Create Part Rows completed : " + (i + 1).ToString();

                    _repo.NewPartCollection(item, currSession, out success, out msg);
                    _repo.UpdateArticleToStoreIn(item.TransactionLineID, item.SerialNo);
                    _repo.UpdateStock(item.SerialNo, 1M);
                    i++;
                    prgBar.Value = i;
                }

                if (Possession != 2)
                {
                    lblDescription.Text = "Receipt PO...";
                    //Get data to new Receipt PO in Epicor
                    var rcvResult = _repo.GetDataToNewReceiptPO(TransactionID);
                    _repo.UpdatePOReleaseQty(currSession, rcvResult.PONum.ToString(), out msg);
                    //List<ReceiptDetailModel> line = new List<ReceiptDetailModel>();
                    var line = _repo.GetPODetailToReceipt(TransactionID).ToList();
                    _repo.GetNewRcv(rcvResult, line, currSession, out success, out msg);
                }

                //Dispose Epicor Session
                currSession.Dispose();

            }
            MessageBox.Show("Save completed.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
