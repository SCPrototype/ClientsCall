using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICityManager : CityManager {

    private Building[] _buildings;
    private bool _turnEnded = false;
    private float _turnEndTime;

    private int _minimalHappiness = 0;
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

    public AICityManager (int pMinHappiness)
    {
        _minimalHappiness = pMinHappiness;
        _animosity = UnityEngine.Random.Range(25, 90); //Starting animosity decides wether the AI will focus on digsites, a wonder, or missiles. Will only change to a bridge if affected by player. Chances for each option: 30, 30, 5 = ~(46.25%, 46.25%, 7.5%)
        //If animosity drops below missiles range, change focus to digsites or wonder (whichever is cheapest).
        if (_animosity < 55)
        {
            _initialFocus = AIFocus.Wonder;
            _myFocus = AIFocus.Wonder;
        }
        else if (_animosity < 85)
        {
            _initialFocus = AIFocus.Digsites;
            _myFocus = AIFocus.Digsites;
        }
        else if (_animosity < 90)
        {
            _initialFocus = AIFocus.Digsites;
            _myFocus = AIFocus.Missiles;
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
	void Start () {
        _buildings = Glob.GetBuildingPrefabs();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public override void HandleTurn(City pCity)
    {
        if (!_turnEnded)
        {
            if (_buildings == null)
            {
                _buildings = Glob.GetBuildingPrefabs();
            }

            Move myMove = getMove(pCity);

            pCity.SetSelectedTile(myMove._tile);

            //Select the tile
            pCity.GetSelectedTile().Reset();

            GameInitializer.GetBuildingHandler().ChangeBuildingSelection(myMove._building);

            GameInitializer.GetBuildingHandler().StartBuilding();
            if (pCity.GetBudget() < 20)
            {
                _turnEnded = true;
                _turnEndTime = Time.time;
            }
        }
        else if (Time.time - _turnEndTime >= Glob.AIEndTurnDelay)
        {
            GameInitializer.EndTurn();
            _turnEnded = false;
        }
    }

    public void ChangeAnimosity(int pAnimo)
    {
        _animosity = Mathf.Clamp(_animosity + pAnimo, 0, 100);
        Debug.Log("New animosity: " + _animosity);
        if (_animosity <= 0)
        {
            _myFocus = AIFocus.Bridge;
        }
        else if (_animosity < 85)
        {
            _myFocus = _initialFocus;
        }
        else if (_animosity < 100)
        {
            _myFocus = AIFocus.Missiles;
        }
    }

    private Move getMove(City pCity, int optimalChance = 75, int subOptimalDiff = 2)
    {
        CustomTile[,] grid = pCity.GetTileMap();
        Move optimalMove = new Move(grid[0,0], _buildings[0], -50);
        Move subOptimalMove = new Move(grid[0,0], _buildings[0], -50);
        Move currentMove = new Move(grid[0, 0], _buildings[0], 0);

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i,j].GetBuildingOnTile() == null) {
                    currentMove._tile = grid[i, j];//For every tile on the grid
                    currentMove._value = 0;
                    for (int k = 0; k < Glob.buildingCount; k++)//Place every possible building
                    {
                        if (_buildings[k].GetCost() <= pCity.GetBudget())
                        {
                            currentMove._building = _buildings[k];
                            if (_buildings[k] is MissileSilo && _myFocus == AIFocus.Missiles)
                            {
                                currentMove._value = 50; //TODO: Balance this value
                            }
                            else if (_buildings[k] is Digsite && _myFocus == AIFocus.Digsites)
                            {
                                currentMove._value = 50;//TODO: Balance this value
                            }
                            else if (_buildings[k] is Park && _myFocus == AIFocus.Wonder)
                            {
                                currentMove._value = 50;//TODO: Check for multiple bordering houses.
                            }
                            currentMove._value = getMoveValue(currentMove._tile, currentMove._building);
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
        if (rnd < optimalChance || subOptimalMove._value < 0)
        {
            return optimalMove;
        } else
        {
            return subOptimalMove;
        }
    }

    private float getMoveValue(CustomTile pTile, Building pBuilding)
    {
        float value = -(pBuilding.GetCost()/3); //Remove cost from the value of the move.
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
                    happinessValue += prodBuilding.GetHappinessGain(); //TODO: Make sure the 5% multipliers for neighbouring production buildings is included here.
                    moneyValue += prodBuilding.GetMoneyGain();
                }
                else if (pBuilding.GetType() == b.GetType())
                {
                    ProductionBuilding prodBuilding = pBuilding as ProductionBuilding;
                    happinessValue += prodBuilding.GetHappinessGain() * 0.05f; //TODO: Store this multiplier value in the glob.
                    moneyValue += prodBuilding.GetMoneyGain() * 0.05f; //If a production building is placed next to a production building of the same type, add value to the move.
                } else
                {
                    //If a production building is placed next to a production building of a different type, subtract value from the move.
                    ProductionBuilding prodBuilding = pBuilding as ProductionBuilding;
                    happinessValue -= prodBuilding.GetHappinessGain() * 0.05f; //TODO: Store this multiplier value in the glob.
                    moneyValue -= prodBuilding.GetMoneyGain() * 0.05f;
                }
            }
        }
        if (tileCity.GetBudget() < 25)
        {
            moneyValue *= 2;
        }
        else if (tileCity.GetHappiness() < _minimalHappiness)
        {
            happinessValue *= 2;
        }
        value += (happinessValue + moneyValue) * collectionValue;
        return value;
    }
}
