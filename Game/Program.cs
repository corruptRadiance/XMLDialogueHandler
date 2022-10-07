using System;
using System.IO;
using System.Text;
using DialogueGame.Dialogue;

namespace DialogueGame
{
    class Program
    {
        static string dialoguePath = "Game/Dialogue.xml";
        static DialogueObject? currentDialogue;

        static bool debug;
        static int startPos = 0;

        static void Main()
        {
            Console.Clear();

            Console.WriteLine("debug [0/1]");
            if (Convert.ToInt32(Console.ReadLine()) == 1)
            {
                debug = true;

                Console.WriteLine("Which dialogue id would you like to jump to?\nNOTE: For first playthrough, start at 0");
                startPos = Convert.ToInt32(Console.ReadLine());
            }

            Console.Clear();

            // Get number of dialogue elements
            int count = DialogueHandler.FindDialogueElementCount(dialoguePath);

            // Iterate through dialogue in order
            for (int i = startPos; i < count; i++)
            {
                currentDialogue = DialogueHandler.FindDialogue(dialoguePath, i.ToString());

                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(i.ToString("00 | "));
                }

                // Set text color dependent on character
                if (currentDialogue.character == "Juniper")
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else if (currentDialogue.character == "@")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                // If there is no character to write, only write text. Should only be the case if <char> is an empty element
                if (currentDialogue.character == "")
                {
                    Console.WriteLine(currentDialogue.text);
                }
                else
                {
                    Console.WriteLine(currentDialogue.completeDialogue);
                }

                Console.ReadKey(true); // Await user input
            }

            Console.ForegroundColor = ConsoleColor.White; // Reset to default
        }
    }
}