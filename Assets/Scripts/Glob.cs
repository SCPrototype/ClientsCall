using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Glob {

    public const KeyCode ConfirmButton = KeyCode.F;
    public const KeyCode CancelButton = KeyCode.G;
    public const KeyCode ExamineButton = KeyCode.H;
    public const KeyCode UpButton = KeyCode.UpArrow;
    public const KeyCode DownButton = KeyCode.DownArrow;
    public const KeyCode LeftButton = KeyCode.LeftArrow;
    public const KeyCode RightButton = KeyCode.RightArrow;

    public enum PlayerTypes
    {
        Achiever,
        Explorer,
        Killer,
        Socializer
    }

    public const string AchieverType = "Achiever";
    public const string AchieverExplain = "As an Achiever, you truly enjoy...";
    public const string AchieverRecommend = "Some games you might enjoy as an Achiever are...";
    public const string ExplorerType = "Explorer";
    public const string ExplorerExplain = "As an Explorer, you truly enjoy...";
    public const string ExplorerRecommend = "Some games you might enjoy as an Explorer are...";
    public const string KillerType = "Killer";
    public const string KillerExplain = "As a Killer, you truly enjoy...";
    public const string KillerRecommend = "Some games you might enjoy as an Killer are...";
    public const string SocializerType = "Socializer";
    public const string SocializerExplain = "As a Socializer, you truly enjoy...";
    public const string SocializerRecommend = "Some games you might enjoy as an Socializer are...";

    public const int OptimalBudgetTurn6 = 120;
    public const int OptimalBudgetTurn7 = 211;
    public const int OptimalBudgetTurn8 = 338;

    //GAME VALUES
    public const float GameTimeOut = 120;
    public const float ResetButtonTime = 5;
    public const float EndTurnButtonTime = 2;

    public const int AmountOfAICities = 1; //Dont change this value, it will break the game. (Spaghetti code caused by time restraints.)
    public const int CityWidth = 10;
    public const int CityLength = 10;
    public const int CitySpacing = 62;
    public const float TileSpacing = 0.2f;
    public const int StartingBudget = 140; //Is actually 10, but player gets forced to spend for the tutorial.
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
    public const float CameraBuildingZoomTime = 2;
    public static Vector3 CameraCityOffset = new Vector3(13, 32, -16);
    public static Vector3 CameraBuildingOffset = new Vector3(-1, 7, -6);

    public const int AmountOfRelicsNeededToWin = 10;
    //As a percentage.
    public const int ChanceToMineRelic = 50;

    public const int AmountOfMissilesNeededToWin = 3;
    public const int MissileAnimosityChange = 75; //out of 100

    public const float HappyHouseAnimosityChange = 2f; //out of 100

    public const int WonderHappyHouseReq = 17;

    public const int AmountOfBridgesNeededToWin = 1;

    public const int TurnAmount = 16;
    public const int EventTurnStart = 3;
    public const int EventTurnInterval = 2;

    //PREFABS
    public const string UIPrefab = "UI";

    public const string TilePrefab = "Tile";
    public const string SignPrefab = "SIGN";
    public const string WorldSpaceCanvasPrefab = "WorldSpaceCanvas";
    public static Color HappyColor = new Color(0.25f, 0.5f, 0f, 1);
    public static Color UnhappyColor = new Color(0.46f, 0.46f, 0.28f, 1);
    public static Color NeutralColor = new Color(0.58f, 0.68f, 0.35f);
    public static Color MissileAimColor = new Color(1, 0, 0, 1);

    public const int buildingCount = 8;
    public const int particleCount = 2;
    public const int factoriesVariations = 5;

    public const string housePrefab = "Buildings/House";
    public const string factoryPrefab0 = "Buildings/Factory0";
    public const string factoryPrefab1 = "Buildings/Factory1";
    public const string factoryPrefab2 = "Buildings/Factory2";
    public const string factoryPrefab3 = "Buildings/Factory3";
    public const string factoryPrefab4 = "Buildings/Factory4";
    public const string parkPrefab0 = "Buildings/Park0";
    public const string digsitePrefab = "Buildings/Digsite";
    public const string missileSiloPrefab = "Buildings/MissileSilo";
    public const string wonderPrefab = "Buildings/Wonder";
    public const string bridgePrefab = "Buildings/Bridge Construction Site";
    public const string mayorOfficePrefab = "Buildings/MayorOffice";
    public const string missilePrefab = "Missile";


    public const int amountOfRelics = 4;
    public const string RelicPrefab1 = "Relics/Relic1Material";
    public const string RelicPrefab2 = "Relics/Relic2Material";
    public const string RelicPrefab3 = "Relics/Relic3Material";
    public const string RelicPrefab4 = "Relics/Relic4Material";

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

    public static Material[] GetRelicsMaterials()
    {
        Material[] relics = new Material[amountOfRelics];
        relics[0] = Resources.Load<Material>(RelicPrefab1);
        relics[1] = Resources.Load<Material>(RelicPrefab2);
        relics[2] = Resources.Load<Material>(RelicPrefab3);
        relics[3] = Resources.Load<Material>(RelicPrefab4);
        return relics;
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

    public const string buildingImagePrefab = "Icons/BuildingIcon";
    public const string houseIcon = "Icons/HouseIcon";
    public const string factoryIcon = "Icons/FactoryIcon";
    public const string parkIcon = "Icons/ParkIcon";
    public const string digsiteIcon = "Icons/DigsiteIcon";
    public const string missileSiloIcon = "Icons/MissileSiloIcon";//TODO: Correct icon
    public const string wonderIcon = "Icons/DigsiteIcon";//TODO: Correct icon
    public const string bridgeIcon = "Icons/DigsiteIcon";//TODO: Correct icon

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

    public static Missile GetMissile()
    {
        return Resources.Load<Missile>(missilePrefab);
    }


   
}
