using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.Research;
using EyeXFramework.Forms;
using Tobii.EyeX.Framework;

namespace CSCI298
{
    static class Program
    {
        
        /***************************************************/
        public static FormsEyeXHost _eyeHost = new FormsEyeXHost();

        /// <summary>
        /// Gets the singleton EyeX host instance.
        /// </summary>
        public static FormsEyeXHost EyeXHost
        {
            get { return _eyeHost; }
        }

    /**************************************************/

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
        static void Main()
        {
            //_eyeHost.Start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartPage());
            _eyeHost.Dispose();
        }
    }
}
