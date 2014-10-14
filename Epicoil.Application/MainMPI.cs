using Epicoil.Repositories;
using System;
using System.Windows.Forms;

namespace Epicoil.Appl
{
    public partial class MainMPI : BaseSession
    {
        private readonly IMainRepo _repo;

        public MainMPI()
        {
            InitializeComponent();
            this._repo = new MainRepo();
        }

        private void MainMPI_Load(object sender, EventArgs e)
        {
            if (epiSession.SessionID == null)
            {
                Login frm = new Login();
                frm.ShowDialog();
                if (epiSession != null)
                {
                    this.Text = epiSession.PlantName;
                }
                else
                {
                    return;
                }
            }
            else
            {
                this.Text = epiSession.PlantName;
                return;
            }
            AddMenu();
            trvMenu.ExpandAll();
        }

        private void AddMenu()
        {
            TreeNode nodeRoot = new TreeNode(epiSession.CompanyName);
            nodeRoot.Tag = 1;
            trvMenu.Nodes.Add(nodeRoot);

            var menu = _repo.GetAll(1, 1);
            foreach (var i in menu)
            {
                TreeNode nodeChild = new TreeNode(i.MenuDescription);
                nodeChild.Tag = i.MenuID;
                nodeRoot.Nodes.Add(nodeChild);
                var submenu = _repo.GetAll(i.MenuID, 2);
                foreach (var s in submenu)
                {
                    TreeNode nodeChild2 = new TreeNode(s.MenuDescription);
                    nodeChild2.Tag = s.MenuID;
                    nodeChild.Nodes.Add(nodeChild2);
                }
            }
        }

        private void ributLogOff_Click(object sender, EventArgs e)
        {
            Login frm = new Login();
            frm.ShowDialog();

            if (epiSession != null)
            {
                this.Text = epiSession.PlantName;
                trvMenu.Nodes.Clear();
                AddMenu();
            }
        }

        private void GetItemMenu(int parentID)
        {
            listView.Clear();
            var item = _repo.GetAllItem(parentID);
            listView.View = View.List;
            int n = 0;
            foreach (var i in item)
            {
                listView.Items.Add(i.MenuDescription);
                listView.Items[n].Name = i.Module + "." + i.SecCode;
                listView.Items[n].ImageIndex = 2;
                n++;
            }
        }

        private void trvMenu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            GetItemMenu(int.Parse(e.Node.Tag.GetString()));
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = listView.SelectedItems;
                ListViewItem lvItem = items[0];
                string from = lvItem.Name;
                ShowForm(from);
            }
        }

        private void rbnbutLargeIcon_Click(object sender, EventArgs e)
        {
            listView.View = View.LargeIcon;
        }

        private void rbnbutDetails_Click(object sender, EventArgs e)
        {
            listView.View = View.Details;
        }

        private void rbnbutSmallIcon_Click(object sender, EventArgs e)
        {
            listView.View = View.SmallIcon;
        }

        private void rbnbutList_Click(object sender, EventArgs e)
        {
            listView.View = View.List;
        }

        private void rbnbutTile_Click(object sender, EventArgs e)
        {
            listView.View = View.Tile;
        }
    }
}