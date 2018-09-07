using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Glob {

    public const string uiPrefab = "UI";

    public const string tilePrefab = "Tile";

    public const int buildingCount = 2;

    public const string housePrefab = "House";
    public const string factoryPrefab = "Factory";
    public const string parkPrefab = "Park";
    public static Building[] GetBuildingPrefabs()
    {
        Building[] buildings = new Building[buildingCount];
        buildings[0] = Resources.Load<Building>(housePrefab);
        buildings[1] = Resources.Load<Building>(factoryPrefab);
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
