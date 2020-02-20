using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace OSD_Tool.Screens
{
    public partial class Home : Form
    {
        private static Home _instance;
        public static Home Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Home();

                return _instance;
            }
        }

        public Home()
        {
            InitializeComponent();
            this.panelActiveWH.Hide();
            this.btnMenuAnnualLeave.BackColor = Color.FromArgb(26, 28, 31);
            this.workingHourControl.Hide();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            this.MinimizeBox = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit this application?",
                        "Confirm",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Process.GetCurrentProcess().Kill();
                return;
            }
        }

        private void btnMenuWorkingHour_Click(object sender, EventArgs e)
        {
            this.panelActiveAnnualLeave.Hide();
            this.panelActiveWH.Show();
            this.btnMenuAnnualLeave.BackColor = Color.Transparent;
            this.btnMenuWorkingHour.BackColor = Color.FromArgb(26, 28, 31);
            this.annualLeaveCtr.Hide();
            this.workingHourControl.Show();
        }

        private void btnMenuAnnualLeave_Click(object sender, EventArgs e)
        {
            this.panelActiveAnnualLeave.Show();
            this.panelActiveWH.Hide();
            this.btnMenuWorkingHour.BackColor = Color.Transparent;
            this.btnMenuAnnualLeave.BackColor = Color.FromArgb(26, 28, 31);
            this.workingHourControl.Hide();
            this.annualLeaveCtr.Show();
            this.workingHourControl.ResetAll();
            this.workingHourControl.Refresh();
            this.workingHourControl.Update();
        }
    }
}
