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
    public const int RandomHouseAmount = 5;
    public const int RandomFactoryAmount = 1;
    public const int RandomParkAmount = 1;

    public const float TurnDelay = 2.5f;
    public const float AIEndTurnDelay = 2f;
    public const float AnimationCollection = 0.2f;

    public const float CameraCitySwitchTime = 2f;

    //PREFABS
    public const string uiPrefab = "UI";

    public const string tilePrefab = "Tile";

    public const int buildingCount = 4;
    public const int particleCount = 2;
    public const int factoriesVariations = 5;

    public const string housePrefab = "House";
    public const string factoryPrefab0 = "Factory0";
    public const string factoryPrefab1 = "Factory1";
    public const string factoryPrefab2 = "Factory2";
    public const string factoryPrefab3 = "Factory3";
    public const string factoryPrefab4 = "Factory4";
    public const string parkPrefab0 = "Park0";
    public const string digsitePrefab = "Digsite";


    public static Building[] GetBuildingPrefabs()
    {
        Building[] buildings = new Building[buildingCount];
        buildings[0] = Resources.Load<House>(housePrefab).Initialize();
        buildings[1] = Resources.Load<Factory>(factoryPrefab0).Initialize();
        buildings[2] = Resources.Load<Park>(parkPrefab0).Initialize();
        buildings[3] = Resources.Load<Digsite>(digsitePrefab).Initialize();
        //buildings[2] = Resources.Load<Factory>(factoryPrefab1);
        //buildings[3] = Resources.Load<Factory>(factoryPrefab2);
        //buildings[4] = Resources.Load<Factory>(factoryPrefab3);
        return buildings;
    }

    public const string buildingImagePrefab = "BuildingIcon";
    public const string houseIcon = "HouseIcon";
    public const string factoryIcon = "FactoryIcon";
    public const string parkIcon = "ParkIcon";
    public const string digsiteIcon = "DigsiteIcon";

    public static Sprite[] GetBuildingIcons()
    {
        Sprite[] icons = new Sprite[buildingCount];
        icons[0] = Resources.Load<Sprite>(houseIcon);
        icons[1] = Resources.Load<Sprite>(factoryIcon);
        icons[2] = Resources.Load<Sprite>(parkIcon);
        icons[3] = Resources.Load<Sprite>(digsiteIcon);
        return icons;
    }

    //public const string factoryParticle = "FactoryParticle";
    //public const string parkParticle = "ParkParticle";

    //public static ParticleSystem[] GetParticleEffects()
    //{
    //    ParticleSystem[] particles = new ParticleSystem[particleCount];
    //    particles[0] = Resources.Load<ParticleSystem>(factoryParticle);
    //    particles[0] = Resources.Load<ParticleSystem>(parkParticle);
    //    return particles;
    //}
}
