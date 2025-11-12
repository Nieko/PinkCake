using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkCake
{
    public interface IServiceStatus
    {
        bool GetIsRunning();
        void Start();
    }
}
