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
            currentDialogue = DialogueHandler.FindDialogue("2");
            Console.WriteLine(currentDialogue.completeDialogue + "\n");

            currentDialogue = DialogueHandler.FindDialogue("4");
            Console.WriteLine(currentDialogue.completeDialogue + "\n");

            currentDialogue = DialogueHandler.FindDialogue("0");
            Console.WriteLine(currentDialogue.completeDialogue + "\n");

            /*
            OUTPUT:
            Character 1: My name My name is Character 1 and I am saying words

            Sheesh: here is a sentence.
            this sentence is on a new line, i wonder how the program will handle that

            Character 1: Text goes here :3
            
            */

            // for (int i = 0; i <= 4; i++)
            // {
            //     currentDialogue = DialogueHandler.FindDialogue(i.ToString());
            //     Console.WriteLine(currentDialogue.completeDialogue + "\n");
            // }
        }
    }
}