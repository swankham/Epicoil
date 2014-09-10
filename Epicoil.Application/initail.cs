using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Epicoil.Appl
{
    static class initail
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMPI());
        }

        static void ApplyTheme(Form f)
        {
            f.Font = new Font("Microsoft Sans Serif", 8.25f); f.BackColor = Color.FromArgb(190, 208, 232);
        }

        public static void UseTheme(Form form)
        {
            ApplyTheme(form);
        }
    }

}
