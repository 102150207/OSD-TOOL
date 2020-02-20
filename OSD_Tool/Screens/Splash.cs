using OSD_Tool.Screens;
using System;
using System.Windows.Forms;

namespace OSD_Tool
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Interval = 300;
            timer.Enabled = true;
            timer.Tick += (s, ea) =>
            {
                progressBar.Increment(10);
                Console.Write(progressBar.Value);
                if (progressBar.Value == 100)
                {
                    this.Close();
                    timer.Stop();
                }
            };
        }

        private void Splash_FormClosing(object sender, FormClosingEventArgs e)
        {
            Home home = Home.Instance;
            home.Show();
        }
    }
}
