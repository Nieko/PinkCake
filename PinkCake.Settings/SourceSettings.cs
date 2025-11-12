using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkCake.Settings
{
    public class SourceSettings
    {
        public string PhotoFilter { get; set; }
        public double? RefreshBuffer { get; set; }
        public TimeSpan Frequency { get; set; } = new TimeSpan(0, 3, 0);
        public SourceFolder[] Sources { get; set; } = new SourceFolder[] { };
    }
}
