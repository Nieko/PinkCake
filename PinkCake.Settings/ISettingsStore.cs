using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkCake.Settings
{
    public interface ISettingsStore
    {
        SourceSettings Load();
        void Save(SourceSettings setings);
    }
}
