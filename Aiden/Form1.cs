using System;
using System.Diagnostics;
using System.Speech.Recognition;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Timers;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Aiden
{

    public partial class Form1 : Form
    {
        private readonly ContextMenuStrip contextMenuStrip1;
        private readonly ToolStripMenuItem exitToolStripMenuItem;
        private static Form3 form3Instance;
        [DllImport("user32.dll")]
        private static extern int SendMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        public static SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        public static System.Timers.Timer Timer { get; } = new System.Timers.Timer();
        public static SpeechSynthesizer Synthesizer { get => synthesizer; set => synthesizer = value; }
        public static int A { get => a; set => a = value; }
        public Form1()
        {
            InitializeComponent();
            this.notifyIcon1 = new NotifyIcon();
            this.contextMenuStrip1 = new ContextMenuStrip();
            this.exitToolStripMenuItem = new ToolStripMenuItem();
            this.notifyIcon1.Icon = new Icon("appicon.ico"); 
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.exitToolStripMenuItem.Text = "Çıkış";
            this.exitToolStripMenuItem.Click += new EventHandler(this.ExitToolStripMenuItem_Click);
            this.contextMenuStrip1.Items.Add(this.exitToolStripMenuItem);
            this.Shown += MainForm_Shown;
           
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Visible = false;
            base.OnFormClosing(e);
        }
        public static readonly SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        private static readonly Choices commands = new Choices(File.ReadAllLines("commands.txt"));
        private static readonly GrammarBuilder gb = new GrammarBuilder(commands);
        private static readonly Grammar g = new Grammar(gb);
        private void Form1_Load(object sender, EventArgs e)
        {   
            Timer.Interval = 1000;           
            Timer.Elapsed += Timer_Tick;
            
            try
            {
                recognizer.SetInputToDefaultAudioDevice();
                gb.Append(commands);
                recognizer.LoadGrammar(g);
                recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                synthesizer.Speak("Error," + ex.Message);
            }
            AidenUtilities.AidenHello();
        }
        private static void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
           
            switch (e.Result.Text)
            {
                case "hey aiden":
                    if (a <= 9)
                    {
                        synthesizer.Speak("Listening");
                        a = 0;
                        Timer.Enabled = true;
                        
                    }
                    break;
            }
            if (Timer.Enabled == true && a <= 9)
            {
                switch (e.Result.Text)
                {
                    case "increase volume":
                    case "turn up the volume":
                    case "raise the volume":
                    case "volume up":
                    case "increase the sound":
                    case "boost the volume":
                    case "amplify the sound":
                    case "make it louder":
                    case "turn the sound up":
                    case "raise the audio level":

                        SystemVolume.VolumeUp();
                        AidenUtilities.RandomAnswer();
                        break;
                    case "decrease volume":
                    case "lower the volume":
                    case "turn down the volume":
                    case "volume down":
                    case "decrease the sound":
                    case "reduce the volume":
                    case "quieter please":
                    case "diminish the sound":
                    case "make it quieter":
                    case "turn the sound down":
                    case "lower the audio level":
                        SystemVolume.VolumeDown();
                        AidenUtilities.RandomAnswer();
                        break;
                    case "mute the system":
                    case "mute system":
                    case "mute windows":
                    case "mute sound":
                        SystemVolume.MuteSys();
                        AidenUtilities.RandomAnswer();
                        break;
                    case "unmute":
                    case "unmute the system":
                    case "unmute system":
                    case "unmute windows":
                    case "unmute sound":
                        SystemVolume.UnmuteSys();
                        AidenUtilities.RandomAnswer();
                        break;
                    case "close window":
                    case "close the window":
                    case "shut window":
                    case "shut the window":
                    case "terminate window":
                    case "terminate the window":
                    case "exit window":
                    case "exit the window":
                    case "close this window":
                    case "end window":
                    case "end the window":
                        Media.CloseFocusedWindow();
                        AidenUtilities.RandomAnswer();
                        break;
                    case "reopen":

                        Media.ReopenLastClosedWindow();
                        AidenUtilities.RandomAnswer();
                        break;
                    case "lock the screen":
                    case "secure the screen":
                    case "lock the display":
                    case "lock the monitor":
                    case "lock the device":
                    case "lock the computer":
                    case "lock the workstation":
                    case "lock the desktop":
                    case "lock the system":
                    case "lock the interface":
                    case "lock the windows":
                    case "lock windows":
                        Media.LockWindows();
                        AidenUtilities.RandomAnswer();
                        break;
                    case "stop listening":

                        AidenUtilities.StopListening();
                        break;
                    case "exit":
                        AidenUtilities.RandomAnswer();
                        synthesizer.SpeakAsyncCancelAll();
                        synthesizer.Dispose();
                        Environment.Exit(0);
                        Application.Exit();
                        break;
                    case "screenshot":
                        Media.Scshot();
                        break;
                    case "add to startup":
                        Startup.AddStartup();
                        AidenUtilities.RandomAnswer();
                        break;
                    case "remove startup":
                        Startup.RemoveStartup();
                        AidenUtilities.RandomAnswer();
                        break;
                    case "save applications":
                        a = 0;
                        Timer.Enabled = false;
                        Media.SaveApps();
                        a = 0;
                        break;
                    case "whats up":
                    case "how you doing":
                    case "how are you":
                    case "howar you":
                    case "hawar you":
                    case "how do you feel":
                    case "how are you doing":
                        AidenUtilities.Tahammulbir();
                        a = 0;
                        break;
                    case "tell me a joke":
                    case "make me laugh":
                        Timer.Enabled=false;
                        AidenUtilities.Aidenjoke();
                        Timer.Enabled=true;
                        a = 0;
                        break;
                    case "schedule closing":
                    case "schedule computer to shutdown":
                    case "set shutdown time":
                    case "set shutdown schedule":
                    case "shutdown schedule":
                        Closingschedule();
                        a = 0;
                        break;
                    case "pause music":
                    case "pause the music":
                    case "stop the music":
                        Media.SendMediaKey(Media.VK_MEDIA_PLAY_PAUSE);
                        AidenUtilities.RandomAnswer();
                        break;
                    case "resume music":
                    case "start music":
                    case "start the music":
                    case "unpause music":
                    case "unpause the music":
                    case "resume the music":
                        Media.SendMediaKey(Media.VK_MEDIA_PLAY_PAUSE);
                        AidenUtilities.RandomAnswer();
                        break;
                    case "previous music":
                        Media.SendMediaKey(Media.VK_MEDIA_PREV_TRACK);
                        Media.SendMediaKey(Media.VK_MEDIA_PREV_TRACK);
                        AidenUtilities.RandomAnswer();
                        break;
                    case "restart music":
                    case "go to start":
                    case "go to the beginning":
                    case "go to the start":
                        Media.SendMediaKey(Media.VK_MEDIA_PREV_TRACK);
                        AidenUtilities.RandomAnswer();
                        break;
                    case "next music":
                    case "next one":
                    case "skip music":
                    case "skip this music":
                    case "skip the music":
                        Media.SendMediaKey(Media.VK_MEDIA_NEXT_TRACK);
                        AidenUtilities.RandomAnswer();
                        break;

                }
                try
                {
                    string filePath = "selectedApp.txt";
                    string filePath1 = "selectedAppPath.txt";
                    if (e.Result.Text.StartsWith("open"))
                    {
                        string command = e.Result.Text.Substring(5).Trim();
                        string fileContent = File.ReadAllText(filePath);
                        int index = fileContent.IndexOf(command, StringComparison.OrdinalIgnoreCase);
                        string fileContent1 = File.ReadAllText(filePath1);
                        if (index >= 0)
                        {
                            var lines = fileContent1.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            string result = lines.FirstOrDefault(line => fileContent.IndexOf(command, StringComparison.OrdinalIgnoreCase) >= 0 && line.IndexOf(command, StringComparison.OrdinalIgnoreCase) >= 0) ?? string.Empty;
                            if (!string.IsNullOrEmpty(result))
                            {
                                Console.WriteLine("result: " + result);

                                if (File.Exists(result))
                                {
                                    Process.Start(new ProcessStartInfo(result) { UseShellExecute = true });
                                }
                                else
                                {
                                    Console.WriteLine("The file deosnt exist: " + result);
                                }
                            }
                            else
                            {
                                Console.WriteLine("File path is not found");
                            }
                        }
                        else
                        {
                            Console.WriteLine("invalid Index");
                        }
                    }
                }
                catch (Exception ex)
                {
                    synthesizer.Speak(ex.Message);

                }
                
                Console.WriteLine(e.Result.Text);
            }
        }
        private static void Closingschedule()
        {
            if (form3Instance == null || form3Instance.IsDisposed)
            {
                form3Instance = new Form3();
                form3Instance.FormClosed += (s, args) => form3Instance = null;
                form3Instance.Show();
            }
            else
            {
                form3Instance.BringToFront();
            }
        }
        public static int a = 0;
        public static void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            a++;
            Console.WriteLine(a);
            if (a >= 10)
            {
                AidenUtilities.StopListening();
            }
        }    
    }
}
    

