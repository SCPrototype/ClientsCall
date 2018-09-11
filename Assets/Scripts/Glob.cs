using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Glob {

    //GAME VALUES
    public const int AmountOfAICities = 3;
    public const int CityWidth = 7;
    public const int CityLength = 7;
    public const int CitySpacing = 50;
    public const float TileSpacing = 0.2f;
    public const int RandomHouseAmount = 5;
    public const int RandomFactoryAmount = 1;
    public const int RandomParkAmount = 1;

    public const float TurnDelay = 2.5f;
    public const float AIEndTurnDelay = 0.5f;

    public const float CameraCitySwitchTime = 2;

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
        buildings[0] = Resources.Load<House>(housePrefab).Initialize();
        buildings[1] = Resources.Load<Factory>(factoryPrefab).Initialize();
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
