using System;
using System.Windows.Forms;

namespace XLevelEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (FormMain frmMain = new FormMain())
            {
                Application.Run(frmMain);
            }
        }
    }
}

