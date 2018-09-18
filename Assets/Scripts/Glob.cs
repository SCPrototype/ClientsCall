using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Glob {

    //GAME VALUES
    public const int AmountOfAICities = 1; //Dont change this value, it will break the game. (Spaghetti code caused by time restraints.)
    public const int CityWidth = 10;
    public const int CityLength = 10;
    public const int CitySpacing = 58;
    public const float TileSpacing = 0.2f;
    public const int RandomHouseAmount = 1;
    public const int RandomFactoryAmount = 1;
    public const int RandomParkAmount = 1;
    public const int StartingBudget = 500;
    public const int BudgetCap = 500;
    public const float FactoryProductionMultiplier = 0.05f;

    public const int EasyAIDifficulty = 50;
    public const int HardAIDifficulty = 85;

    public const float TurnDelay = 3f;
    public const float AIEndTurnDelay = 0.5f;
    public const float AIBuildDelay = 0.5f;
    public const float AIMissileDelay = 1.5f;
    public const float AnimationCollection = 0.2f;

    public const float CameraCitySwitchTime = 2.5f;
    public static Vector3 CameraOffset = new Vector3(3, 31, -10);

    public const int AmountOfRelicsNeededToWin = 10;
    //As a percentage.
    public const int ChanceToMineRelic = 50;

    public const int AmountOfMissilesNeededToWin = 3;
    public const int MissileAnimosityChange = 75; //out of 100

    public const float HappyHouseAnimosityChange = 2f; //out of 100

    public const int WonderHappyHouseReq = 17;

    public const int AmountOfBridgesNeededToWin = 1;

    public const int TurnAmount = 16;
    public const int EventTurnInterval = 4;

    //PREFABS
    public const string UIPrefab = "UI";

    public const string TilePrefab = "Tile";
    public static Color HappyColor = new Color(0.25f, 0.5f, 0f, 1);
    public static Color UnhappyColor = new Color(0.46f, 0.46f, 0.28f, 1);
    public static Color MissileAimColor = new Color(1, 0, 0, 1);

    public const int buildingCount = 8;
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
    public const string missileSiloPrefab = "MissileSilo";
    public const string wonderPrefab = "Wonder";
    public const string bridgePrefab = "Bridge Construction Site";
    public const string mayorOfficePrefab = "MayorOffice";
    public const string missilePrefab = "Missile";

    //Sounds
    public const int amountOfSounds = 11;
    public const string confirmSound = "Sounds/Confirm";
    public const string endTurnSound = "Sounds/EndTurn";
    public const string errorSound = "Sounds/Error";
    public const string loseSound = "Sounds/Lose";
    public const string missileHitSound = "Sounds/MissileHit";
    public const string missileLaunchSound = "Sounds/MissileLaunch";
    public const string moneySound = "Sounds/Money";
    public const string moveSound = "Sounds/Move";
    public const string popUpSound = "Sounds/PopUp";
    public const string winSound = "Sounds/Win";
    public const string backgroundMusic = "Sounds/BackgroundSounds";


    public static Building[] GetBuildingPrefabs()
    {
        Building[] buildings = new Building[buildingCount];
        buildings[0] = Resources.Load<House>(housePrefab).Initialize();
        buildings[1] = Resources.Load<Factory>(factoryPrefab0).Initialize();
        buildings[2] = Resources.Load<Park>(parkPrefab0).Initialize();
        buildings[3] = Resources.Load<Digsite>(digsitePrefab).Initialize();
        buildings[4] = Resources.Load<MissileSilo>(missileSiloPrefab).Initialize();
        buildings[5] = Resources.Load<Wonder>(wonderPrefab).Initialize();
        buildings[6] = Resources.Load<Bridge>(bridgePrefab).Initialize();
        buildings[7] = Resources.Load<MayorOffice>(mayorOfficePrefab).Initialize();
        //buildings[2] = Resources.Load<Factory>(factoryPrefab1);
        //buildings[3] = Resources.Load<Factory>(factoryPrefab2);
        //buildings[4] = Resources.Load<Factory>(factoryPrefab3);
        return buildings;
    }

    public static Missile GetMissile()
    {
        return Resources.Load<Missile>(missilePrefab).Initialize();
    }

    public static Factory[] GetFactoriesPrefabs()
    {
        Factory[] factories = new Factory[factoriesVariations];
        factories[0] = Resources.Load<Factory>(factoryPrefab0).Initialize();
        factories[1] = Resources.Load<Factory>(factoryPrefab1).Initialize();
        factories[2] = Resources.Load<Factory>(factoryPrefab2).Initialize();
        factories[3] = Resources.Load<Factory>(factoryPrefab3).Initialize();
        factories[4] = Resources.Load<Factory>(factoryPrefab4).Initialize();
        return factories;
    }

    public const string buildingImagePrefab = "BuildingIcon";
    public const string houseIcon = "HouseIcon";
    public const string factoryIcon = "FactoryIcon";
    public const string parkIcon = "ParkIcon";
    public const string digsiteIcon = "DigsiteIcon";
    public const string missileSiloIcon = "DigsiteIcon";//TODO: Correct icon
    public const string wonderIcon = "DigsiteIcon";//TODO: Correct icon
    public const string bridgeIcon = "DigsiteIcon";//TODO: Correct icon

    public static Sprite[] GetBuildingIcons()
    {
        Sprite[] icons = new Sprite[buildingCount];
        icons[0] = Resources.Load<Sprite>(houseIcon);
        icons[1] = Resources.Load<Sprite>(factoryIcon);
        icons[2] = Resources.Load<Sprite>(parkIcon);
        icons[3] = Resources.Load<Sprite>(digsiteIcon);
        icons[4] = Resources.Load<Sprite>(missileSiloIcon);
        icons[5] = Resources.Load<Sprite>(wonderIcon);
        icons[6] = Resources.Load<Sprite>(bridgeIcon);
        return icons;
    }

    public static AudioClip[] GetAudioClips()
    {
        AudioClip[] audioClips = new AudioClip[amountOfSounds];
        audioClips[0] = Resources.Load<AudioClip>(confirmSound);
        audioClips[1] = Resources.Load<AudioClip>(endTurnSound);
        audioClips[2] = Resources.Load<AudioClip>(errorSound);
        audioClips[3] = Resources.Load<AudioClip>(loseSound);
        audioClips[4] = Resources.Load<AudioClip>(missileHitSound);
        audioClips[5] = Resources.Load<AudioClip>(missileLaunchSound);
        audioClips[6] = Resources.Load<AudioClip>(moneySound);
        audioClips[7] = Resources.Load<AudioClip>(moveSound);
        audioClips[8] = Resources.Load<AudioClip>(popUpSound);
        audioClips[9] = Resources.Load<AudioClip>(winSound);
        audioClips[10] = Resources.Load<AudioClip>(backgroundMusic);
        return audioClips;
    }


   
}
