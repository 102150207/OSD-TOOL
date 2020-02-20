using System;
using System.Windows.Forms;

namespace OSD_Tool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Splash splash = new Splash();
            splash.Show();
            Application.Run();
        }
    }
}
