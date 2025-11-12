using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;
using PinkCake.Settings;

namespace PinkCake.Service
{
    internal class Program
    {
        public static string ServiceName { get; private set; } = "PinkCake";

        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                if(args.Length > 1 || !new[] { "/i", "/install" }.Any(sw => args[0].ToLower() == sw))
                {
                    throw new ArgumentException("Invalid combination of parameters: " + string.Join(" ", args));
                }

                var exePath = Assembly.GetEntryAssembly().Location;
                var destinationPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                var destinationFile = System.IO.Path.Combine(destinationPath, Path.GetFileNameWithoutExtension(new FileInfo(exePath).Name) + ".lnk");

                if (!System.IO.File.Exists(destinationFile))
                {
                    var linkBuilder = new ShortcutBuilder();

                    linkBuilder.CreateLink("PinkCakeUpdater", exePath, destinationFile);
                }

                Console.WriteLine("Installation complete");
            }

            double refreshBuffer = 1.1;

            if(!double.TryParse(ConfigurationManager.AppSettings["RefreshBuffer"], out refreshBuffer))
            {
                refreshBuffer = 1.1;
            }

            var defaults = new SourceSettings
            {
                PhotoFilter = ConfigurationManager.AppSettings["PhotoFilter"],
                RefreshBuffer = (refreshBuffer == 0.0 ? null : new double?(refreshBuffer))
            };

            if (string.IsNullOrEmpty(defaults.PhotoFilter))
            {
                defaults.PhotoFilter = ".png,.jpg,.jpeg,.bmp";
            }

            var worker = new Worker(new UserAppHomeStore(), defaults);
            var workerTask = worker.StartAsync(new System.Threading.CancellationToken());

            workerTask.Wait();
        }
    }
}
