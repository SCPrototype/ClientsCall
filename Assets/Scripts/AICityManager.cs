using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICityManager : CityManager
{

    private Building[] _buildings;
    private bool _turnEnded = false;
    private float _turnEndTime;

    private int _difficulty;

    private bool _isFocusedOnOwnCity = true;

    private float _prevBuild = 0;

    private int _animosity; //High animosity = aggressive
    //99-85 animosity = missiles
    //84-55 animosity = digsites
    //54-1 animosity = wonder
    //0 animosity = bridge

    private enum AIFocus
    {
        Missiles,
        Digsites,
        Wonder,
        Bridge
    }
    private AIFocus _initialFocus = AIFocus.Wonder;
    private AIFocus _myFocus = AIFocus.Wonder;

    public AICityManager(int pDifficulty = 50, int pAnimosity = 0)
    {
        _difficulty = pDifficulty;

        if (pAnimosity == 0)
        {
            pAnimosity = UnityEngine.Random.Range(43, 55);
        }
        _animosity = pAnimosity; //Starting animosity decides wether the AI will focus on digsites, a wonder, or missiles. Will only change to a bridge if affected by player. Chances for each option: 6, 6, 1 = ~(46.25%, 46.25%, 7.5%)
        //If animosity drops below missiles range, change focus to digsites or wonder (whichever is cheapest).
        _animosity = 99;
        if (_animosity < 49)
        {
            _initialFocus = AIFocus.Wonder;
            _myFocus = AIFocus.Wonder;
        }
        else if (_animosity < 54)
        {
            _initialFocus = AIFocus.Digsites;
            _myFocus = AIFocus.Digsites;
        }
        else if (_animosity < 55)
        {
            _initialFocus = AIFocus.Digsites;
            _myFocus = AIFocus.Missiles;
            _animosity = 99;
        }
        Debug.Log(_animosity);
    }

    private class Move
    {
        public Move(CustomTile pTile, Building pBuilding, int pValue)
        {
            _tile = pTile;
            _building = pBuilding;
            _value = pValue;
        }

        public CustomTile _tile;
        public Building _building;
        public float _value;
    }

    // Use this for initialization
    void Start()
    {
        _buildings = Glob.GetBuildingPrefabs();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void HandleTurn(City pCity)
    {
        if (!_turnEnded)
        {
            if (Time.time - _prevBuild >= Glob.AIBuildDelay)
            {
                if (_buildings == null)
                {
                    _buildings = Glob.GetBuildingPrefabs();
                }

                City targetCity = pCity;
                _prevBuild = Time.time;

                if (currentMode == CurrentMode.MISSILEAIM)
                {
                    targetCity = GameInitializer.GetNextCity(pCity);
                    launchMissile(getBestFactory(targetCity));
                    targetCity = pCity;
                    GameInitializer.GetCameraManager().MoveCameraTo(targetCity.transform.position + Glob.CameraOffset, Glob.CameraCitySwitchTime / 2, 4);
                    //pCity.AddMissileLaunched();
                    return;
                }
                Move myMove = getMove(pCity, _difficulty, pCity.GetCurrentTurn());

                pCity.SetSelectedTile(myMove._tile);

                //Select the tile
                pCity.GetSelectedTile().Reset();

                GameInitializer.GetBuildingHandler().ChangeBuildingSelection(myMove._building);

                GameInitializer.GetBuildingHandler().StartBuilding();
                if (pCity.GetBudget() < 20 && currentMode != CurrentMode.MISSILEAIM)
                {
                    _turnEnded = true;
                    _turnEndTime = Time.time;
                }
                else if (currentMode == CurrentMode.MISSILEAIM)
                {
                    targetCity = GameInitializer.GetNextCity(pCity);

                    GameInitializer.GetCameraManager().MoveCameraTo(targetCity.transform.position + Glob.CameraOffset, Glob.CameraCitySwitchTime / 2, 4);
                    targetCity.SetSelectedTile(getBestFactory(targetCity));
                    _prevBuild = Time.time + Glob.AIMissileDelay;
                }
            }
        }
        else if (Time.time - _turnEndTime >= Glob.AIEndTurnDelay)
        {
            GameInitializer.EndTurn();
            _turnEnded = false;
        }
    }

    public void ChangeAnimosity(int pAnimo, City pCity)
    {
        _animosity = Mathf.Clamp(_animosity + pAnimo, 0, 100);
        Debug.Log("New animosity: " + _animosity);
        if (_animosity <= 0 && pCity.GetBridgesBuilt() < Glob.AmountOfBridgesNeededToWin)
        {
            _myFocus = AIFocus.Bridge;
        }
        else if (_animosity < 56)
        {
            _myFocus = _initialFocus;
        }
        else if (_animosity < 100)
        {
            _myFocus = AIFocus.Missiles;
        }
    }

    private CustomTile getBestFactory(City pCity)
    {
        _isFocusedOnOwnCity = false;
        UIHandler.ShowNotification("BOMBS AWAY!"); //TODO: Placeholder text
        CustomTile[,] enemyCity = pCity.GetTileMap();
        float bestMoveValue = -50;
        int bestMoveX = -1;
        int bestMoveY = -1;
        for (int x = 0; x < enemyCity.GetLength(0); x++)
        {
            for (int y = 0; y < enemyCity.GetLength(1); y++)
            {
                if (enemyCity[x, y].GetBuildingOnTile() is Factory)
                {
                    float moveValue = GetTileValue(enemyCity[x, y]);
                    Debug.Log(moveValue);
                    if (moveValue >= bestMoveValue)
                    {
                        bestMoveValue = moveValue;
                        bestMoveX = x;
                        bestMoveY = y;
                    }
                }
            }
        }

        return enemyCity[bestMoveX, bestMoveY];
    }

    private void launchMissile(CustomTile pTarget)
    {
        Missile missile = Instantiate(Glob.GetMissile());
        missile.WaitWithAnimation(4);
        missile.SetMissileTile(pTarget);
        City targetCity = pTarget.GetCity();
        Destroy(pTarget.GetBuildingOnTile().gameObject);
        Debug.Log("Destroyed the building");
        pTarget.SetBuilding(null);

        SetCurrentMode(CurrentMode.SELECTINGTILE);
        _isFocusedOnOwnCity = true;

    }

    private Move getMove(City pCity, int optimalChance = 65, int subOptimalDiff = 1)
    {
        CustomTile[,] grid = pCity.GetTileMap();
        Move optimalMove = new Move(grid[0, 0], _buildings[0], -50);
        Move subOptimalMove = new Move(grid[0, 0], _buildings[0], -50);
        Move currentMove = new Move(grid[0, 0], _buildings[0], 0);

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j].GetBuildingOnTile() == null)
                {
                    currentMove._tile = grid[i, j];//For every tile on the grid
                    currentMove._value = 0;
                    for (int k = 0; k < Glob.buildingCount; k++)//Place every possible building
                    {
                        if (_buildings[k].GetCost() <= pCity.GetBudget())
                        {
                            currentMove._building = _buildings[k];
                            if (currentMove._building is Bridge && _myFocus == AIFocus.Bridge && pCity.GetBridgesBuilt() < Glob.AmountOfBridgesNeededToWin)
                            {
                                currentMove._value = 100000; //After building part of the bridge, switch focus to initial focus, just in case the player doesn't finish the bridge.
                                _myFocus = _initialFocus;
                            }
                            else if (currentMove._building is MissileSilo && _myFocus == AIFocus.Missiles)
                            {
                                currentMove._value = 150; //TODO: Balance this value
                            }
                            else if (currentMove._building is Digsite && _myFocus == AIFocus.Digsites)
                            {
                                currentMove._value = -currentMove._building.GetCost() + pCity.GetBudget() / 5;
                                Building[] closeBuildings = pCity.GetBuildingsAroundTile(1, currentMove._tile);
                                for (int l = 0; l < closeBuildings.Length; l++)
                                {
                                    if (closeBuildings[l] is House)
                                    {
                                        currentMove._value += 0.01f;
                                    }
                                    else if (closeBuildings[l] is Digsite)
                                    {
                                        currentMove._value += 0.02f;
                                    }
                                }
                                currentMove._value += pCity.GetRelicAmount() * 5;
                            }
                            else if (_myFocus == AIFocus.Wonder && (currentMove._building is Park || currentMove._building is House || currentMove._building is Wonder))
                            {
                                Building[] closeBuildings = pCity.GetBuildingsAroundTile(1, currentMove._tile);
                                if (currentMove._building is Park)
                                {
                                    currentMove._value = -currentMove._building.GetCost() + pCity.GetBudget() / 10;
                                    foreach (Building building in closeBuildings)
                                    {
                                        if (building is House)
                                        {
                                            currentMove._value += 25;
                                            Building[] closeToHouse = pCity.GetBuildingsAroundTile(1, building.GetBuildingTile());
                                            for (int l = 0; l < closeToHouse.Length; l++)
                                            {
                                                if (closeToHouse[l] is Park)
                                                {
                                                    currentMove._value -= 25;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (currentMove._building is House)
                                {
                                    currentMove._value = getMoveValue(currentMove._tile, currentMove._building);
                                    foreach (Building building in closeBuildings)
                                    {
                                        if (building is Park)
                                        {
                                            currentMove._value += 5;
                                            break;
                                        }
                                    }
                                }
                                else if (currentMove._building is Wonder)
                                {
                                    if (pCity.GetHappyHouseAmount() >= Glob.WonderHappyHouseReq)
                                    {
                                        currentMove._value = 100000;
                                    }
                                    else
                                    {
                                        currentMove._value = -50;
                                    }

                                }
                            }
                            else
                            {
                                currentMove._value = getMoveValue(currentMove._tile, currentMove._building);
                            }

                            if (currentMove._value > subOptimalMove._value && currentMove._value < optimalMove._value - subOptimalDiff)
                            {
                                subOptimalMove._tile = currentMove._tile; //Overwrite the sub-optimal move with the current one.
                                subOptimalMove._building = currentMove._building;
                                subOptimalMove._value = currentMove._value;
                            }
                            else if (currentMove._value > optimalMove._value)//If the tile/building combination gives a higher value than the previous optimal move.
                            {
                                optimalMove._tile = currentMove._tile; //Overwrite the optimal move with the current one.
                                optimalMove._building = currentMove._building;
                                optimalMove._value = currentMove._value;
                            }
                        }
                    }
                }
            }
        }

        int rnd = UnityEngine.Random.Range(0, 100);
        Debug.Log(optimalMove._value + " _ " + subOptimalMove._value);
        if (rnd < optimalChance || subOptimalMove._value < 0 || optimalMove._value >= 50000)
        {
            return optimalMove;
        }
        else
        {
            Debug.Log("SUB-OPTIMAL!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            return subOptimalMove;
        }
    }

    private float getMoveValue(CustomTile pTile, Building pBuilding)
    {
        float value = -(pBuilding.GetCost() / 3); //Remove cost from the value of the move.
        City tileCity = pTile.GetCity();

        bool buildingIsProduction = false;
        float happinessValue = 0;
        float moneyValue = 0;
        float collectionValue = 1;
        if (pBuilding is ProductionBuilding)
        {
            buildingIsProduction = true;
            ProductionBuilding prodBuilding = pBuilding as ProductionBuilding;
            happinessValue += prodBuilding.GetHappinessGain();
            moneyValue += prodBuilding.GetMoneyGain();
            collectionValue = 0;
        }

        if (pBuilding is FunctionBuilding)
        {
            return -50;
        }

        Building[] buildingsInRange = tileCity.GetBuildingsAroundTile(1, pTile);
        foreach (Building b in buildingsInRange)
        {
            if (b is CollectionBuilding && buildingIsProduction)
            {
                //If this move places a production building next to a collection building, add value to the move.
                collectionValue += 1;
            }
            else if (b is ProductionBuilding)
            {
                if (!buildingIsProduction)
                {
                    //If this move places a collection building next to a production building, add value to the move.
                    ProductionBuilding prodBuilding = b as ProductionBuilding;
                    happinessValue += prodBuilding.GetHappinessGain();
                    moneyValue += prodBuilding.GetMoneyGain();
                }
                else if (pBuilding.GetType() == b.GetType())
                {
                    ProductionBuilding prodBuilding = pBuilding as ProductionBuilding;
                    happinessValue += prodBuilding.GetHappinessGain() * Glob.FactoryProductionMultiplier;
                    moneyValue += prodBuilding.GetMoneyGain() * Glob.FactoryProductionMultiplier; //If a production building is placed next to a production building of the same type, add value to the move.
                }
                else
                {
                    //If a production building is placed next to a production building of a different type, subtract value from the move.
                    ProductionBuilding prodBuilding = pBuilding as ProductionBuilding;
                    happinessValue -= prodBuilding.GetHappinessGain() * Glob.FactoryProductionMultiplier;
                    moneyValue -= prodBuilding.GetMoneyGain() * Glob.FactoryProductionMultiplier;
                }
            }
        }
        if (tileCity.GetBudget() < 25)
        {
            moneyValue *= 2;
        }

        value += (happinessValue + moneyValue) * collectionValue;
        return value;
    }
}
