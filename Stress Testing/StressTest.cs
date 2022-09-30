using System;
using System.IO;
using System.Text;
using System.Xml;

namespace DialogueGame.StressTest
{
    public class XMLGenerator
    {
        static XmlWriter? xw;
        static XmlWriterSettings? settings;
        public static void NewXMLFile(string path, int count)
        {
            //Generate a new .xml file formatted after Dialogue.xml. Generate random strings for each <dialogue> object child Element

            settings = new XmlWriterSettings();
            settings.Indent = true;
            xw = XmlWriter.Create(path, settings);

            // root element
            xw.WriteStartElement("tree");

            for (int i = 0; i < count; i++)
            {
                // Create <dialogue> object
                xw.WriteStartElement("dialogue");
                xw.WriteAttributeString("id", i.ToString());

                // <dialogue> children
                xw.WriteElementString("char", "some character name");
                xw.WriteElementString("text", "some text");
                xw.WriteElementString("image", "some image path");
                xw.WriteEndElement();
            }

            // root element
            xw.WriteEndElement();
            xw.Close();
        }
    }
}