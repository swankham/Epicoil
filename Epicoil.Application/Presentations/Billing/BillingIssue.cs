using Epicoil.Library.Models;
using Epicoil.Library.Models.Billing;
using Epicoil.Library.Repositories.Billing;

namespace Epicoil.Appl.Presentations.Billing
{
    public partial class BillingIssue : BaseSession
    {
        private readonly IBillingRepo _repo;
        public BillingIssue(SessionInfo _session = null, InvcHeadModel model = null)
        {
            InitializeComponent();
            this._repo = new BillingRepo();
        }
    }
}