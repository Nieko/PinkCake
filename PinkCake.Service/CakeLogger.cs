using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkCake.Service
{
    internal class CakeLogger
    {
        private static object _lock = new object();
        private static Queue<string> _Messages = new Queue<string>();

        public CakeLogger Log(string message)
        {
            var logCake = new FileInfo("cake.log");

            if ((logCake.Exists && (logCake.Length / Math.Pow(1024, 2)) > 10 * Math.Pow(1024, 2)))
            {
                // Too much cake
                var lastEntries = File.ReadLines(logCake.FullName)
                    .Reverse()
                    .Take(10)
                    .Reverse();

                lock (_lock)
                { 
                    try
                    {


                        File.WriteAllLines(logCake.FullName, lastEntries);

                    }
                    catch
                    {
                        _Messages.Enqueue("WARNING: UNABLE TO TRUNCATE LOG");
                        _Messages.Enqueue(message);

                        return this;
                    };
                }
            }

            lock(_lock)
            {
                _Messages.Enqueue(message);
            }

            try
            {
                File.AppendAllLines(logCake.FullName, _Messages);

                while (_Messages.Count > 0)
                {
                    _Messages.Dequeue();
                }
            }
            catch { }

            return this;
        }

        public CakeLogger Log(Exception ex, string message)
        {
            Log(message + Environment.NewLine + ex.ToString());

            return this;
        }
    }
}
