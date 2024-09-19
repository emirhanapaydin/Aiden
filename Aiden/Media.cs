using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Windows.Forms;

namespace Aiden
{
    internal class Media
    {
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3; // Oynat veya duraklat
        public const int VK_MEDIA_PREV_TRACK = 0xB1; // Önceki parça
        public const int VK_MEDIA_NEXT_TRACK = 0xB0; // Sonraki parça
        public const uint KEYEVENTF_EXTENDEDKEY = 0x0001; // Key down
        public const uint KEYEVENTF_KEYUP = 0x0002; // Key up
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);
        private static string lastClosedProcessName = string.Empty;
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        public static string LastClosedProcessName { get => lastClosedProcessName; set => lastClosedProcessName = value; }
        public static Bitmap Screenshot()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height);

            using (Graphics gfx = Graphics.FromImage(screenshot))
            {
                gfx.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
            }

            return screenshot;
        }
        public static void Scshot()
        {
            string screenshotlanguage;
            screenshotlanguage = "Screenshots";
            Bitmap screenshot = Screenshot();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string screenshotPath = Path.Combine(desktopPath + @"\" + screenshotlanguage, $"AidenShot_{timestamp}.png");
            screenshot.Save(screenshotPath, System.Drawing.Imaging.ImageFormat.Png);

            Console.WriteLine($"Screenshot Saved: {screenshotPath}");

            Screenshot();
            AidenUtilities.RandomAnswer();
        }
        public static void LockWindows()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "nircmd.exe",
                Arguments = $"lockws",
                UseShellExecute = false,
                CreateNoWindow = true
            });
            Console.WriteLine("Windows is locked.");
        }
        public static void ReopenLastClosedWindow()
        {
            if (!string.IsNullOrEmpty(lastClosedProcessName))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = $"{lastClosedProcessName}.exe",
                    UseShellExecute = true
                });
                Console.WriteLine("Last closed window reopened.");
            }
            else
            {
                Console.WriteLine("No window to reopen.");
            }
        }
        public static void CloseFocusedWindow()
        {
            IntPtr hWnd = GetForegroundWindow();
            GetWindowThreadProcessId(hWnd, out uint processId);
            Process process = Process.GetProcessById((int)processId);
            lastClosedProcessName = process.ProcessName;

            Process.Start(new ProcessStartInfo
            {
                FileName = "nircmd.exe",
                Arguments = $"closeprocess {lastClosedProcessName}.exe",
                UseShellExecute = false,
                CreateNoWindow = true
            });
            Console.WriteLine("Focused window closed.");
        }
        public static void Fileopener()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select an app";
                Form1.synthesizer.Speak("Select an app");
                openFileDialog.Filter = "Uygulamalar (*.exe)|*.exe|Tüm dosyalar (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    string applicationName = Path.GetFileNameWithoutExtension(selectedFilePath);
                    string filePath = "selectedApp.txt";
                    string commandsPath = "commands.txt";
                    string filePath1 = "selectedAppPath.txt";
                    using (StreamWriter sw = new StreamWriter(filePath, true))
                    {
                        sw.WriteLine(applicationName);
                    }
                    using (StreamWriter sw = new StreamWriter(commandsPath, true))
                    {
                        sw.WriteLine("open " + applicationName.ToLower());
                    }
                    using (StreamWriter sw = new StreamWriter(filePath1, true))
                    {
                        sw.WriteLine(selectedFilePath);
                    }

                    Form1.synthesizer.Speak("application named" + applicationName + "saved. Say, " + "open " + applicationName + ", whenever you want me to open it.");
                    Form1.Timer.Enabled = true;
                    Form1.a = 0;
                }
            }

        }
        public static void SendMediaKey(int keyCode)
        {
            // Simulate key press
            keybd_event((byte)keyCode, 0, KEYEVENTF_EXTENDEDKEY, IntPtr.Zero);
            // Simulate key release
            keybd_event((byte)keyCode, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, IntPtr.Zero);
        }
        public static void SaveApps()
        {
            Media.Fileopener();
            Choices commands1 = new Choices(File.ReadAllLines("commands.txt"));
            GrammarBuilder gb1 = new GrammarBuilder(commands1);
            Grammar g1 = new Grammar(gb1);
            Form1.recognizer.UnloadAllGrammars();
            Form1.recognizer.LoadGrammar(g1);
            Form1.Timer.Enabled = true;
        }
    }
}
