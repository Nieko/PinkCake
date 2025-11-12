using PinkCake.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PinkCake
{
    public class AboutData : IAboutData
    {
        public ISettingsStore SettingsStore { get; private set; }

        private IServiceStatus _ServiceStatus;

        public AboutData(ISettingsStore store, IServiceStatus serviceStatus)
        {
            SettingsStore = store;
            _ServiceStatus = serviceStatus;
        }

        public bool GetServiceStatus()
        {
            return _ServiceStatus.GetIsRunning();
        }

        public void StartService()
        {
            _ServiceStatus.Start();
        }
    }
}
