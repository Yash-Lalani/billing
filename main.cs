using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace billing
{
    public partial class main : Form
    {
        private ToolStripMenuItem currentMenuItem = null;

        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
            // This form is the MDI parent
            IsMdiContainer = true;
            Dashboard Dashboard = new Dashboard();
            Dashboard.MdiParent = this;
            Dashboard.Dock = DockStyle.Fill;
            Dashboard.Show();
            SetActiveMenu(homeToolStripMenuItem);
            UpdateFormTitle("Billing System - Home");
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseExistingChildren();
            Dashboard Dashboard = new Dashboard();
            Dashboard.MdiParent = this;
            Dashboard.Dock = DockStyle.Fill;
            Dashboard.Show();
            SetActiveMenu(homeToolStripMenuItem);
            UpdateFormTitle("Billing System - Home");
        }

        private void addBillDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseExistingChildren();
            billdetails billDetailsForm = new billdetails();
            billDetailsForm.MdiParent = this;
            billDetailsForm.Dock = DockStyle.Fill;
            billDetailsForm.Show();
            SetActiveMenu(addBillDetailsToolStripMenuItem);
            UpdateFormTitle("Billing System - Add Bill Details");
            SetActiveMenu(settingsToolStripMenuItem);
        }

        private void billToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CloseExistingChildren();
            bill billForm = new bill();
            billForm.MdiParent = this;
            billForm.Dock = DockStyle.Fill;
            billForm.Show();
            SetActiveMenu(billToolStripMenuItem);
            UpdateFormTitle("Billing System - Bill");
        }

        private void stocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseExistingChildren();
            stocks st = new stocks();
            st.MdiParent = this;
            st.Dock = DockStyle.Fill;
            st.Show();
            SetActiveMenu(stocksToolStripMenuItem);
            UpdateFormTitle("Billing System - Stocks");
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseExistingChildren();
            companybankdetails cbForm = new companybankdetails();
            cbForm.MdiParent = this;
            cbForm.Dock = DockStyle.Fill;
            cbForm.Show();
            SetActiveMenu(addToolStripMenuItem);
            UpdateFormTitle("Billing System - Add Company Bank Details");
            SetActiveMenu(settingsToolStripMenuItem);
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseExistingChildren();
            clientlist cl = new clientlist();
            cl.MdiParent = this;
            cl.Dock = DockStyle.Fill;
            cl.Show();
            SetActiveMenu(stockToolStripMenuItem);
            UpdateFormTitle("Billing System - Client List");
        }
        private void accountSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseExistingChildren();
            accountSettings accounts = new accountSettings();
            accounts.MdiParent = this;
            accounts.Dock = DockStyle.Fill;
            accounts.Show();
            SetActiveMenu(accountSettingsToolStripMenuItem);
            UpdateFormTitle("Billing System - Account Settings");
            SetActiveMenu(settingsToolStripMenuItem);
        }
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login lg = new login();
            this.Hide();
            lg.Show();
        }
        private void SetActiveMenu(ToolStripMenuItem menuItem)
        {
            if (currentMenuItem != null)
            {
                currentMenuItem.BackColor = SystemColors.Control;
            }
            menuItem.BackColor = SystemColors.ActiveCaption;
            currentMenuItem = menuItem;
        }

        private void CloseExistingChildren()
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void UpdateFormTitle(string pageTitle)
        {
            this.Text = pageTitle;
        }

        
    }
}