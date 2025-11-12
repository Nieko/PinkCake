using PinkCake.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinkCake
{
    static class Program
    {
        public static string ServiceName => "PinkCakeUpdater";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var compositionRoot = new CompositionRoot();

            compositionRoot.Register<About, About>()
                .Register<IAboutData, AboutData>()
                .Register<ISettingsStore, UserAppHomeStore>()
                .Register<IServiceStatus, ServiceStatus>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var aboutForm = compositionRoot.Resolve<About>();
            Application.Run(aboutForm);
        }
    }
}
