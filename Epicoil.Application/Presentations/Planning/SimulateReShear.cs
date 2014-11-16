using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories.Planning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Epicoil.Appl.Presentations.Planning
{
    public partial class SimulateReShear : BaseSession
    {
        private readonly IWorkEntryRepo _repo;

        public PlanningHeadModel HeadModel;

        public SimulateReShear(SessionInfo _session = null, PlanningHeadModel model = null)
        {
            InitializeComponent();
            this._repo = new WorkEntryRepo();
            HeadModel = model;
            epiSession = _session;
        }
    }
}
