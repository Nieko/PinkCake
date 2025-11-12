using PinkCake.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkCake
{
    public interface IAboutData
    {
        ISettingsStore SettingsStore { get; }
        bool GetServiceStatus();
        void StartService();
    }
}
