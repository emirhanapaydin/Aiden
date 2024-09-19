using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiden
{
    internal class SystemVolume
    {
        public static void VolumeUp()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "nircmd.exe",
                Arguments = "changesysvolume 19660",
                UseShellExecute = false,
                CreateNoWindow = true
            });
            Console.WriteLine("Volume increased.");
        }

        public static void VolumeDown()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "nircmd.exe",
                Arguments = "changesysvolume -19660",
                UseShellExecute = false,
                CreateNoWindow = true
            });
            Console.WriteLine("Volume decreased.");
        }
        public static void MuteSys()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "nircmd.exe",
                Arguments = "mutesysvolume 1",
                UseShellExecute = false,
                CreateNoWindow = true
            });
            Console.WriteLine("System Muted.");
        }
        public static void UnmuteSys()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "nircmd.exe",
                Arguments = "mutesysvolume 0",
                UseShellExecute = false,
                CreateNoWindow = true
            });
            Console.WriteLine("System Unmuted.");
        }
    }
}
