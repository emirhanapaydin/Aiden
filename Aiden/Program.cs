using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aiden
{
    internal static class Program
    {
        static readonly CultureInfo currentCulture = CultureInfo.CurrentCulture;
        private static readonly string languageCode = currentCulture.TwoLetterISOLanguageName;
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(languageCode == "en")
            {
                Application.Run(new Form1());
            }
            //if (languageCode == "tr")
            //{
            //    Application.Run(new Form2());
            //}
            else
            {
                Form1.Synthesizer.Speak("This application can run only in English version of Windows.");
                Form1.Synthesizer.Dispose();
                Application.Exit();
               
            }
        }
    }
}
