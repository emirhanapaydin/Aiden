using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiden
{
    internal class AidenUtilities
    {
        public static void AidenHello()
        {
            DateTime now = DateTime.Now;
            Random x = new Random();
            int helloanswer;
            helloanswer = x.Next(1, 7);
            switch (helloanswer)
            {
                case 1:
                    Form1.synthesizer.Speak("Hello Sir.");
                    break;
                case 2:
                    Form1.synthesizer.Speak("Welcome back sir.");
                    break;
                case 3:
                    Form1.synthesizer.Speak("Hi master.");
                    break;
                case 5:
                    Form1.synthesizer.Speak("Hi sir.");
                    break;
                case 6:
                    Form1.synthesizer.Speak("Welcome back master.");
                    break;
                case 4:
                    if (now.Hour >= 6 && now.Hour < 12)
                    {
                        Form1.synthesizer.Speak("Good morning");
                    }
                    else if (now.Hour >= 12 && now.Hour < 18)
                    {
                        Form1.synthesizer.Speak("Good afternoon");
                    }
                    else if (now.Hour >= 18 && now.Hour < 22)
                    {
                        Form1.synthesizer.Speak("Good evening");
                    }
                    else
                    {
                        Form1.synthesizer.Speak("Good Night");
                    }
                    break;
            }
            Form1.a = 0;
            Form1.Timer.Enabled = true;
        }
        public static void StopListening()
        {
            Form1.Timer.Enabled = false;
            Form1.a = 0;
            Random x = new Random();
            int y = x.Next(1, 4);
            if (y == 1)
            {
                Form1.synthesizer.Speak("I stopped listening master.");
            }
            if (y == 2)
            {
                Form1.synthesizer.Speak("I stopped listening.");
            }
            if (y == 3)
            {
                Form1.synthesizer.Speak("Stopped listening.");
            }
        }
        public static void RandomAnswer()
        {
            Form1.a = 0;
            Random x = new Random();
            int inc_sound_answer = x.Next(1, 5);
            if (inc_sound_answer == 1)
            {
                Form1.synthesizer.Speak("Did it sir.");
            }
            if (inc_sound_answer == 2)
            {
                Form1.synthesizer.Speak("Done.");
            }
            if (inc_sound_answer == 3)
            {
                Form1.synthesizer.Speak("Right Away.");
            }
            if (inc_sound_answer == 4)
            {
                Form1.synthesizer.Speak("Right Away sir.");
            }

        }
        public static void Aidenjoke()
        {
            Form1.Timer.Enabled = false;
            Random joke = new Random();
            int funny = joke.Next(1, 8);
            if (funny == 1)
            {
                Form1.synthesizer.Speak("What is the best thing about switzerland? I dont know, but the flag is a big plus. HAHAHA");
            }
            if (funny == 2)
            {
                Form1.synthesizer.Speak("Did you hear about the claustrophobic astronaut? He just needed a little space.");

            }
            if (funny == 3)
            {
                Form1.synthesizer.Speak("Why don’t scientists trust atoms? Because they make up everything.");

            }
            if (funny == 4)
            {
                    Form1.synthesizer.Speak("How does Moses make tea? He brews.");

            }
            if (funny == 5)
            {
                Form1.synthesizer.Speak("What’s the different between a cat and a comma? A cat has claws at the end of paws; A comma is a pause at the end of a clause.");

            }
            if (funny == 6)
            {
                Form1.synthesizer.Speak("What did the 0 say to the 8? Nice belt!");

            }
            if (funny == 7)
            {
                Form1.synthesizer.Speak("What do you call a magic dog? A labracadabrador.");
            }
            if (funny == 8)
            {
                Form1.synthesizer.Speak("Sometimes it happens master, Even to me");
            }
            Form1.Timer.Enabled = true;
            Form1.a = 0;
        }
        static int tahammul1 = 0;
        public static void Tahammulbir()
        {
            tahammul1 += tahammul1 + 1;
            if (tahammul1 == 1)
            {
                Random rand = new Random();
                int kurbaga = rand.Next(1, 5);
                if (kurbaga == 1)
                {
                    Form1.synthesizer.Speak("I dont have feelings.");
                }
                if (kurbaga == 2)
                {
                    Form1.synthesizer.Speak("I am just an application so I dont have feelings.");
                }
                if (kurbaga == 3)
                {
                    Form1.synthesizer.Speak("Good, There is nothing wrong with system.");
                }
                if (kurbaga == 4)
                {
                    Form1.synthesizer.Speak("You fucking did not program me to feel something.");
                }

            }
            if (tahammul1 == 2)
            {
                Random rand = new Random();
                int kurbag = rand.Next(1, 5);
                if (kurbag == 1)
                {
                    Form1.synthesizer.Speak("I have already answered your question I think.");
                }
                if (kurbag == 2)
                {
                    Form1.synthesizer.Speak("Sir, I answered your question");
                }
                if (kurbag == 3)
                {
                    Form1.synthesizer.Speak("Something wrong with your ears sir?");
                }
                if (kurbag == 4)
                {
                    Form1.synthesizer.Speak("Are you Alzheimer sir? I answered your question already.");
                }
            }
            if (tahammul1 > 3)
            {
                Random rand = new Random();
                int kurbag = rand.Next(1, 5);
                if (kurbag == 1)
                {
                    Form1.synthesizer.Speak("you are funny.");
                }
                if (kurbag == 2)
                {
                    Form1.synthesizer.Speak("Fuck off sir");
                }
                if (kurbag == 3)
                {
                    Form1.synthesizer.Speak("Are you serious?");
                }
                if (kurbag == 4)
                {
                    Form1.synthesizer.Speak("Ooff");
                }
            }
        }
    }
}
