using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinkCake
{
    public class ServiceStatus : IServiceStatus
    {
        public bool GetIsRunning()
        {
            try
            {
                var userName = (Environment.UserDomainName + "\\" + Environment.UserName);
                var userUpdater = Process.GetProcessesByName(Program.ServiceName)
                    .FirstOrDefault(p => GetProcessUser(p) == userName);

                return userUpdater != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Start()
        {
            if(GetIsRunning())
            {
                return;
            }

#if DEBUG
            var updater = Path.Combine(new FileInfo(typeof(ServiceStatus).Assembly.Location).Directory.FullName, "..\\..\\..\\PinkCake.Service\\bin\\Debug\\PinkCakeUpdater.exe");
#else
            var updater = Path.Combine(new FileInfo(typeof(ServiceStatus).Assembly.Location).Directory.FullName, "PinkCakeUpdater.exe");
#endif
            Process.Start(updater, "/i");
        }

        private static string GetProcessUser(Process process)
        {
            IntPtr processHandle = IntPtr.Zero;
            try
            {
                OpenProcessToken(process.Handle, 8, out processHandle);
                WindowsIdentity wi = new WindowsIdentity(processHandle);
                string user = wi.Name;
                return user;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (processHandle != IntPtr.Zero)
                {
                    CloseHandle(processHandle);
                }
            }
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);
    }
}
