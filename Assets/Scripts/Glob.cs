using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Glob {

    //GAME VALUES
    public const int AmountOfAICities = 1;
    public const int CityWidth = 7;
    public const int CityLength = 7;
    public const int CitySpacing = 50;
    public const float TileSpacing = 0.2f;

    public const float TurnDelay = 1f;

    //PREFABS
    public const string uiPrefab = "UI";

    public const string tilePrefab = "Tile";

    public const int buildingCount = 2;

    public const string housePrefab = "House";
    public const string factoryPrefab = "Factory";
    public const string parkPrefab = "Park";
    public static Building[] GetBuildingPrefabs()
    {
        Building[] buildings = new Building[buildingCount];
        buildings[0] = Resources.Load<House>(housePrefab);
        buildings[1] = Resources.Load<Factory>(factoryPrefab);
        return buildings;
    }

    public const string buildingImagePrefab = "BuildingIcon";
    public const string houseIcon = "HouseIcon";
    public const string factoryIcon = "FactoryIcon";
    public const string parkIcon = "ParkIcon";
    public static Sprite[] GetBuildingIcons()
    {
        Sprite[] icons = new Sprite[buildingCount];
        icons[0] = Resources.Load<Sprite>(houseIcon);
        icons[1] = Resources.Load<Sprite>(factoryIcon);
        return icons;
    }
}
