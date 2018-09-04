using System;
using System.IO;
using System.Xml.Serialization;


public class TMX_Parser
{
    public TMX_Parser()
    {
    }

    public void Parse(string filename, out AllEvents output)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AllEvents));

        TextReader reader = new StreamReader(filename);
        AllEvents allEvents = serializer.Deserialize(reader) as AllEvents;
        reader.Close();

        output = allEvents;
    }
}
