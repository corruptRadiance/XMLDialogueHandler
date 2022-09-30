using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Diagnostics;
using DialogueGame.Dialogue;
using DialogueGame.StressTest;

namespace DialogueGame
{
    class Program
    {
        // Write dialogue to console in order, awaiting user input between statements

        static DialogueObject? currentDialogue;
        static string path = "Stress Testing/StressDialogue.xml";
        static int count = 1000; // Amount of dialogue objects to write

        static void Main()
        {
            Stopwatch sw = new Stopwatch();
            XMLGenerator.NewXMLFile(path, count);

            sw.Start();

            currentDialogue = DialogueHandler.FindDialogue(path, (count-1).ToString());
            Console.WriteLine(currentDialogue.completeDialogue + "\nTime elapsed: " + sw.Elapsed);

            sw.Stop();
        }
    }
}