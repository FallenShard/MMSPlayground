using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using MMSPlayground.Presenters;
using MMSPlayground.Model;

namespace MMSPlayground
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

            ImageModel model = new ImageModel();
            MainPresenter presenter = new MainPresenter(model);
            
            Application.Run(new MainForm(presenter));
        }
    }
}
