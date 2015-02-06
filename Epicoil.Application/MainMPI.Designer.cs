namespace Epicoil.Appl
{
    partial class MainMPI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMPI));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("");
            this.ribbonTab1 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.ributLogOff = new System.Windows.Forms.RibbonButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ribbonTab2 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel3 = new System.Windows.Forms.RibbonPanel();
            this.rbnbutLargeIcon = new System.Windows.Forms.RibbonButton();
            this.rbnbutSmallIcon = new System.Windows.Forms.RibbonButton();
            this.rbnbutList = new System.Windows.Forms.RibbonButton();
            this.rbnbutTile = new System.Windows.Forms.RibbonButton();
            this.ribbonTab3 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.about = new System.Windows.Forms.RibbonButton();
            this.ribbonButton1 = new System.Windows.Forms.RibbonButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.trvMenu = new System.Windows.Forms.TreeView();
            this.listView = new System.Windows.Forms.ListView();
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Panels.Add(this.ribbonPanel1);
            this.ribbonTab1.Text = "File";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ButtonMoreVisible = false;
            this.ribbonPanel1.Items.Add(this.ributLogOff);
            this.ribbonPanel1.Text = "";
            // 
            // ributLogOff
            // 
            this.ributLogOff.Image = global::Epicoil.Appl.Properties.Resources._1410298041_Logout;
            this.ributLogOff.SmallImage = global::Epicoil.Appl.Properties.Resources._1410298041_Logout;
            this.ributLogOff.Text = "LogOff";
            this.ributLogOff.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center;
            this.ributLogOff.ToolTip = "Store In Plan";
            this.ributLogOff.Click += new System.EventHandler(this.ributLogOff_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "open16.png");
            this.imageList1.Images.SetKeyName(1, "folder_closed.png");
            this.imageList1.Images.SetKeyName(2, "application.png");
            this.imageList1.Images.SetKeyName(3, "1400687324_folder-open.png");
            this.imageList1.Images.SetKeyName(4, "1410297591_text_list_bullets.png");
            this.imageList1.Images.SetKeyName(5, "folder-checked-32.png");
            this.imageList1.Images.SetKeyName(6, "none-folder-32.png");
            // 
            // ribbonTab2
            // 
            this.ribbonTab2.Panels.Add(this.ribbonPanel3);
            this.ribbonTab2.Text = "View";
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.Items.Add(this.rbnbutLargeIcon);
            this.ribbonPanel3.Items.Add(this.rbnbutSmallIcon);
            this.ribbonPanel3.Items.Add(this.rbnbutList);
            this.ribbonPanel3.Items.Add(this.rbnbutTile);
            this.ribbonPanel3.Text = null;
            // 
            // rbnbutLargeIcon
            // 
            this.rbnbutLargeIcon.DropDownResizable = true;
            this.rbnbutLargeIcon.Image = global::Epicoil.Appl.Properties.Resources.view_large;
            this.rbnbutLargeIcon.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbnbutLargeIcon.SmallImage")));
            this.rbnbutLargeIcon.Text = "LargeIcon";
            this.rbnbutLargeIcon.Click += new System.EventHandler(this.rbnbutLargeIcon_Click);
            // 
            // rbnbutSmallIcon
            // 
            this.rbnbutSmallIcon.DropDownResizable = true;
            this.rbnbutSmallIcon.Image = global::Epicoil.Appl.Properties.Resources.view_small;
            this.rbnbutSmallIcon.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbnbutSmallIcon.SmallImage")));
            this.rbnbutSmallIcon.Text = "SmallIcon";
            this.rbnbutSmallIcon.Click += new System.EventHandler(this.rbnbutSmallIcon_Click);
            // 
            // rbnbutList
            // 
            this.rbnbutList.DropDownResizable = true;
            this.rbnbutList.Image = global::Epicoil.Appl.Properties.Resources._1410297591_text_list_bullets;
            this.rbnbutList.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbnbutList.SmallImage")));
            this.rbnbutList.Text = "List";
            this.rbnbutList.Click += new System.EventHandler(this.rbnbutList_Click);
            // 
            // rbnbutTile
            // 
            this.rbnbutTile.DropDownResizable = true;
            this.rbnbutTile.Image = global::Epicoil.Appl.Properties.Resources.view_detailed;
            this.rbnbutTile.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbnbutTile.SmallImage")));
            this.rbnbutTile.Text = "Tile";
            this.rbnbutTile.Click += new System.EventHandler(this.rbnbutTile_Click);
            // 
            // ribbonTab3
            // 
            this.ribbonTab3.Panels.Add(this.ribbonPanel2);
            this.ribbonTab3.Text = "HELP";
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.Items.Add(this.about);
            this.ribbonPanel2.Text = "";
            // 
            // about
            // 
            this.about.Image = global::Epicoil.Appl.Properties.Resources.addons32;
            this.about.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.about.SmallImage = global::Epicoil.Appl.Properties.Resources.addons32;
            this.about.Text = "About";
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.Image")));
            this.ribbonButton1.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.ribbonButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.SmallImage")));
            this.ribbonButton1.Text = "ribbonButton1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 99);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trvMenu);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView);
            this.splitContainer1.Size = new System.Drawing.Size(1122, 488);
            this.splitContainer1.SplitterDistance = 234;
            this.splitContainer1.TabIndex = 1;
            // 
            // trvMenu
            // 
            this.trvMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trvMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.trvMenu.ImageIndex = 6;
            this.trvMenu.ImageList = this.imageList1;
            this.trvMenu.Location = new System.Drawing.Point(0, 0);
            this.trvMenu.Name = "trvMenu";
            this.trvMenu.SelectedImageIndex = 5;
            this.trvMenu.Size = new System.Drawing.Size(234, 488);
            this.trvMenu.TabIndex = 0;
            this.trvMenu.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvMenu_NodeMouseClick);
            // 
            // listView
            // 
            this.listView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.listView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listView.LargeImageList = this.imageList1;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(884, 488);
            this.listView.SmallImageList = this.imageList1;
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            // 
            // ribbon1
            // 
            this.ribbon1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ribbon1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 72);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.OrbImage = null;
            this.ribbon1.OrbText = "";
            this.ribbon1.OrbVisible = false;
            this.ribbon1.PanelCaptionHeight = 5;
            // 
            // 
            // 
            this.ribbon1.QuickAcessToolbar.DropDownButtonVisible = false;
            this.ribbon1.QuickAcessToolbar.Enabled = false;
            this.ribbon1.QuickAcessToolbar.Image = global::Epicoil.Appl.Properties.Resources.session_logout;
            this.ribbon1.QuickAcessToolbar.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center;
            this.ribbon1.QuickAcessToolbar.Visible = false;
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 10F);
            this.ribbon1.Size = new System.Drawing.Size(1122, 99);
            this.ribbon1.TabIndex = 0;
            this.ribbon1.Tabs.Add(this.ribbonTab1);
            this.ribbon1.Tabs.Add(this.ribbonTab2);
            this.ribbon1.Tabs.Add(this.ribbonTab3);
            this.ribbon1.TabsMargin = new System.Windows.Forms.Padding(12, 1, 20, 0);
            this.ribbon1.Text = "ribbon1";
            this.ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Black;
            // 
            // MainMPI
            // 
            this.BackgroundImage = global::Epicoil.Appl.Properties.Resources._1400687324_folder_open;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1122, 587);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ribbon1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMPI";
            this.Text = "EPICOIL";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainMPI_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RibbonTab ribbonTab1;
        private System.Windows.Forms.RibbonTab ribbonTab2;
        private System.Windows.Forms.TreeView trvMenu;
        public System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonTab ribbonTab3;
        private System.Windows.Forms.RibbonButton ributLogOff;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        private System.Windows.Forms.RibbonButton about;
        private System.Windows.Forms.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RibbonButton ribbonButton1;
        private System.Windows.Forms.RibbonButton rbnbutLargeIcon;
        private System.Windows.Forms.RibbonButton rbnbutSmallIcon;
        private System.Windows.Forms.RibbonButton rbnbutList;
        private System.Windows.Forms.RibbonButton rbnbutTile;
    }
}