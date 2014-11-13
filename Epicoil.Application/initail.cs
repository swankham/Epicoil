using System;
using System.Drawing;
using System.Windows.Forms;

namespace Epicoil.Appl
{
    internal static class initail
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();            
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMPI());
        }

        private static void ApplyTheme(Form f)
        {
            f.Font = new Font("Microsoft Sans Serif", 9f);
            //f.ForeColor = Color.White;
            f.BackColor = Color.FromArgb(222, 239, 254);            
        }

        public static void UseTheme(Form form)
        {
            ApplyTheme(form);
        }
    }
}