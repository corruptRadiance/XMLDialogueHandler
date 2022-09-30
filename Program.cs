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
        static int count = 1000;

        static void Main()
        {
            // Console.Write("Length of XML File: ");
            // count = Console.Read();

            Stopwatch sw = new Stopwatch();
            XMLGenerator.NewXMLFile(path, count);

            sw.Start();

            for (int i = 0; i < count; i++)
            {
                currentDialogue = DialogueHandler.FindDialogue(path, i.ToString());
                Console.WriteLine(i + ": " + currentDialogue.completeDialogue + " | Time elapsed: " + sw.Elapsed);
            }

            sw.Stop();
        }
    }
}