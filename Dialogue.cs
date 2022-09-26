using System;
using System.IO;
using System.Text;
using System.Xml;

namespace DialogueGame.Dialogue
{
    public class DialogueObject
    {
        // Container for information derived from <dialogue> Elements in Dialogue.xml
        public DialogueObject(string Character, string Text, string Image)
        {
            character = Character;
            text = Text;
            image = Image;
        }

        public string? character, text, image;

        public string completeDialogue{get{
                return character + ": " + text;
        }}
    }

    public class DialogueHandler
    {
        // Contains all the logic for parsing Dialogue.xml and handing results to other objects
        static XmlReader? xr;
        static XmlNodeType? currentNodeType;
        static string? currentNodeName, currentNodeValue;
        //static string? currentCharacter, currentText, currentImagePath;
        //static bool isReading;

        static public XmlReader reader{set{
            xr = value;
        }}

        static public DialogueObject FindDialogue(string id)
        {
            /* 
            Read through each <dialogue> Element to find the requested dialogue given 
            its id, then return the values of its Child Elements as a new DialogueObject

            This is probably not the best way to do this, if the dialogue I'm searching for
            has the highest possible id in a tree that contains hundreds or thousands of
            lines of dialogue, this solution could take a very long time to complete.

            ALSO currently this doesn't work if you read dialogue out of consecutive order.
            I don't want to create an XMLReader every single time I run the method, and I
            can't seem to find a way to return the Reader to Depth 0.
            */

            if (xr == null)
            {
                throw new NullReferenceException("No XMLReader instance found.");
            }

            string c, t, i;

            while (xr.Value != id) // Loop iterates until <dialogue> Element with matching id is found, or until all <dialogue> objects are exhausted
            {
                xr.ReadToFollowing("dialogue");
                xr.MoveToFirstAttribute();
                if (xr.Value == "") // If reader reads beyond final <dialogue> object. NOTE: this means any dialogue with an id of "" will cause an error.
                {
                    Console.WriteLine("Warning: No dialogue with id: \"" + id + "\" was found. Make sure id has been input correctly.");
                    return new DialogueObject("", "", "");
                }
            }

            xr.ReadToFollowing("char"); // Read to <char> Element
            xr.Read(); // Text element should always follow <char>
            c = xr.Value;
            
            xr.ReadToFollowing("text"); // Repeat for remaining Elements
            xr.Read();
            t = xr.Value;

            xr.ReadToFollowing("image");
            xr.Read();
            i = xr.Value;
            
            //Console.WriteLine("\nFinished finding dialogue.");

            return new DialogueObject(c, t, i);
        }

        static void WriteNode()
        {
            // Writes Type, Name, Value to fields

            if (xr == null)
            {
                throw new NullReferenceException("No XMLReader instance found.");
            }

            currentNodeType = xr.NodeType; // Node type (Element, Attribute, Text, etc)
            currentNodeName = xr.Name; // Name of node
            currentNodeValue = xr.Value; // Value contained inside node
        }
    }
}