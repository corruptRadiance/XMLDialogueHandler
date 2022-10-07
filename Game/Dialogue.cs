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
        static public string? Path;

        static public DialogueObject FindDialogue(string path, string id)
        {
            /* 
            Read through each <dialogue> Element to find the requested dialogue given 
            its id, then return the values of its Child Elements as a new DialogueObject

            I previously thought this would be inefficient but it actually seems to work
            pretty quickly even when pulling the final object from a list of 10k
            */

            xr = XmlReader.Create(path); // Creating an instance of the reader every time sounds very inefficient but seems like an ok band-aid fix for now

            string c, t, i;

            while (xr.Value != id) // Loop iterates until <dialogue> Element with matching id is found, or until all <dialogue> objects are exhausted
            {
                xr.ReadToFollowing("dialogue");
                xr.MoveToFirstAttribute();
                if (xr.Value == "") // If reader reads beyond final <dialogue> object. NOTE: this means any dialogue with an id of "" will cause an error.
                {
                    Console.WriteLine("Warning: No dialogue with id: \"" + id + "\" was found. Make sure id has been input correctly.");
                    return new DialogueObject("", "", ""); // I want to return null here but that causes a crash and that's like not the point of XML so 
                    // return null;
                }
            }

            /*
            PUT THIS IN A FUCKIN LOOP YOU MONGREL
            */

            xr.ReadToFollowing("char"); // Read to <char> Element
            xr.Read(); // Text element should always follow <char>
            if (xr.IsEmptyElement)
            {
                c = "";
            }
            c = xr.Value;
            
            xr.ReadToFollowing("text"); // Repeat for remaining Elements
            xr.Read();
            if (xr.IsEmptyElement)
            {
                t = "";
            }
            t = xr.Value;

            xr.ReadToFollowing("image");
            xr.Read();
            if (xr.IsEmptyElement)
            {
                i = "";
            }
            i = xr.Value;

            return new DialogueObject(c, t, i);
        }

        static public int FindDialogueElementCount(string path)
        {
            int c = 0;
            xr = XmlReader.Create(path);
            
            while(xr.ReadToFollowing("dialogue"))
            {
                c++;
            }

            return c;
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