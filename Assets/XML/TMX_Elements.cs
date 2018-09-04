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

    /*public Level generateMap()
    {
        Level level = new Level(width, height, tileset.tileWidth, tileset.tileHeight);
        Console.WriteLine("\nGenerating map.");
        Console.WriteLine(this);

        //For each layer this map has.
        for (int i = 0; i < layers.Length; i++)
        {
            TileLayer newLayer = new TileLayer();
            level.AddTileLayer(newLayer);

            int[,] layerTiles = layers[i].data.GetTiles();

            //Write the layers name to the console
            //Console.WriteLine("Layer: " + layers[i].name);

            for (int j = 0; j < layerTiles.GetLength(0); j++)
            {
                for (int k = 0; k < layerTiles.GetLength(1); k++)
                {
                    //Store all characters in a 2D int array.
                    //Console.WriteLine("Layer " + layers[i].name + ":\tRow: " + j + "\tColumn: " + k + "\tTile ID: " + layerTiles[j, k]);

                    /*for (int l = 0; l<tileset.tiles.Length; l++) TODO: Ask the teacher about properties!!!
					{
						if (tileset.tiles[l].id == layerTiles[j,k])
						{
							switch (tileset.tiles[l].properties.)
							{
								default:
									break;
							}
							Console.WriteLine(tileset.tiles[l].properties);
						}
					}*//*

                    //Create a tile using the gathered information.
                    TileObject newTile = new TileObject(tileset, layerTiles[j, k], k, j);
                    //Add the tile to the correct layer.
                    newLayer.AddTile(newTile);
                }
            }
        }
        for (int i = 0; i < objectGroups.Length; i++)
        {
            DataGroupLayer newDataLayer = new DataGroupLayer(width * tileset.tileWidth, height * tileset.tileHeight);
            level.AddDataLayer(newDataLayer);
            for (int j = 0; j < objectGroups[i].MapObjects.Length; j++)
            {
                MapObject p = objectGroups[i].MapObjects[j];
                DataObject newDataObject;
                if (p.polygons != null)
                {
                    newDataObject = new DataObject(p.id, p.name, p.x, p.y, p.width, p.height, p.rotation, p.ellipse, p.properties, newDataLayer, p.polygons.GetPolygons());
                }
                else
                {
                    newDataObject = new DataObject(p.id, p.name, p.x, p.y, p.width, p.height, p.rotation, p.ellipse, p.properties, newDataLayer);
                }
                newDataLayer.AddDataObject(newDataObject);
            }
        }
        return level;
    }*/
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
        string output = type.ToString();
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
        string output = string.Format("\tDescription: {0}\nPollution: {1}, Entertainment: {2}, Health: {3}, Population: {4}, Education: {5}\n", description, pollutionValue, entertainmentValue, healthValue, populationValue, educationValue);
        return output;
    }

    [XmlRootAttribute("repercussion")]
    public class Repercussion
    {
        [XmlAttribute("description")]
        public string description;

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
            string output = string.Format("\tDescription: {0}\nPollution: {1}, Entertainment: {2}, Health: {3}, Population: {4}, Education: {5}\n", description, pollutionValue, entertainmentValue, healthValue, populationValue, educationValue);
            return output;
        }
    }
}