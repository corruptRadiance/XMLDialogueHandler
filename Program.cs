using System;
using System.IO;
using System.Text;
using System.Xml;
using DialogueGame.Dialogue;

namespace DialogueGame
{
    class Program
    {
        // Write dialogue to console in order, awaiting user input between statements

        static DialogueObject? currentDialogue;

        static void Main()
        {
            DialogueHandler.reader = XmlReader.Create("Dialogue.xml");

            for (int i = 0; i <= 4; i++)
            {
                currentDialogue = DialogueHandler.FindDialogue(i.ToString());
                Console.WriteLine(currentDialogue.completeDialogue);
            }
        }
    }
}