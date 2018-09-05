using System;
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

    [XmlAttribute("pollutionValue")]
    public int pollutionValue;

    [XmlAttribute("entertainmentValue")]
    public int entertainmentValue;

    [XmlAttribute("healthValue")]
    public int healthValue;

    [XmlAttribute("populationValue")]
    public int populationValue;

    [XmlAttribute("educationValue")]
    public int educationValue;

    [XmlElement("repercussion")]
    public Repercussion[] repercussions { get; set; }

    public override string ToString()
    {
        string output = "\tChoice: " + string.Format("\tDescription: {0}\t Cost: {1}\n\t\tPollution: {2}, Entertainment: {3}, Health: {4}, Population: {5}, Education: {6}\n", description, cost, pollutionValue, entertainmentValue, healthValue, populationValue, educationValue);
        output += "\n";
        foreach (Repercussion repercussion in repercussions)
        {
            output += repercussion;
        }
        return output;
    }

    public int GetHappiness()
    {
        return -pollutionValue + entertainmentValue + healthValue + populationValue + educationValue;
    }
}

[XmlRootAttribute("repercussion")]
public class Repercussion
{
    [XmlAttribute("description")]
    public string description;

    [XmlAttribute("cost")]
    public int cost;

    [XmlAttribute("pollutionValue")]
    public int pollutionValue;

    [XmlAttribute("entertainmentValue")]
    public int entertainmentValue;

    [XmlAttribute("healthValue")]
    public int healthValue;

    [XmlAttribute("populationValue")]
    public int populationValue;

    [XmlAttribute("educationValue")]
    public int educationValue;

    public override string ToString()
    {
        string output = "\t\tEffect: " + string.Format("\tDescription: {0}\t Cost: {1}\n\t\t\tPollution: {2}, Entertainment: {3}, Health: {4}, Population: {5}, Education: {6}\n", description, cost, pollutionValue, entertainmentValue, healthValue, populationValue, educationValue);
        return output;
    }

    public int GetHappiness()
    {
        return -pollutionValue + entertainmentValue + healthValue + populationValue + educationValue;
    }
}