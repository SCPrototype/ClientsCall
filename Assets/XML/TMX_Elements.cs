﻿using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

/*
   AllEvents
   RandomEvent[ ]
      Type
      Description
      Choice[ ]
         Description
         Values
         Cost
         Repercussion[ ]*
            Description
            Values
*/

[XmlRootAttribute("allEvents")]
public class AllEvents
{
    [XmlElement("randomEvent")]
    public RandomEvent[] events { get; set; }

    public AllEvents() { }

    public override string ToString()
    {
        string output = "";
        foreach (RandomEvent randomEvent in events)
        {
            output += randomEvent;
        }
        return output;
    }
}

[XmlRootAttribute("randomEvent")]
public class RandomEvent
{
    [XmlAttribute("type")]
    public int type;

    [XmlAttribute("description")]
    public string description;

    [XmlElement("choice")]
    public Choice[] choices { get; set; }

    public override string ToString()
    {
        string output = "Event: " + type.ToString();
        output += "\t" + description;
        output += "\n";
        foreach (Choice choice in choices)
        {
            output += choice;
        }
        return output;
    }
}

[XmlRootAttribute("choice")]
public class Choice
{
    [XmlAttribute("description")]
    public string description;

    [XmlAttribute("cost")]
    public int cost;

    [XmlAttribute("achieverValue")]
    public int achieverValue;

    [XmlAttribute("explorerValue")]
    public int explorerValue;

    [XmlAttribute("killerValue")]
    public int killerValue;

    [XmlAttribute("socializerValue")]
    public int socializerValue;

    [XmlElement("repercussion")]
    public Repercussion repercussion { get; set; }

    public override string ToString()
    {
        string output = "\tChoice: " + string.Format("\tDescription: {0}\t Cost: {1}\n\t\tAchiever: {2}, Explorer: {3}, Killer: {4}, Socializer: {5}.\n", description, cost, achieverValue, explorerValue, killerValue, socializerValue);
        output += "\n";
        output += repercussion;
        return output;
    }
}

[XmlRootAttribute("repercussion")]
public class Repercussion
{
    [XmlAttribute("description")]
    public string description;

    [XmlAttribute("cost")]
    public int cost;

    public override string ToString()
    {
        string output = "\t\tEffect: " + string.Format("\tDescription: {0}\t Cost: {1}\n", description, cost);
        return output;
    }
}