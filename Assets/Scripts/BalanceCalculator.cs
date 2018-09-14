using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceCalculator : MonoBehaviour {

    public bool AutoCalculate = false;

    private int[,] cityGrid = new int[8,8];

    public int FactoryCost = 50;
    public int FactoryEarn = 10;
    public float FactoryMultiplier = 0.05f;

    public int HouseCost = 20;

    public int TargetAmount = 500;
    public int MaxTurns = 16;

    public int StartingMoney = 10;
    public int StartingFactories = 1;
    public int StartingHouses = 1;

    private float _money;
    private int _turn;
    private int _factories;
    private int _houses;

	// Use this for initialization
	void Start () {
        _money = StartingMoney;
        _factories = StartingFactories;
        _houses = StartingHouses;
        _turn = 1;

        cityGrid[2, 3] = 2; //Is a house
        cityGrid[3,3] = 1; //Is a factory

        if (AutoCalculate)
        {
            CalculateBalance();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            DoOneTurn();
            _turn++;
        }
	}

    private void CalculateBalance()
    {
        for (int i = 1; i <= MaxTurns; i++)//For every turn
        {
            for (int j = 0; j < cityGrid.GetLength(0); j++)//Check every tile
            {
                for (int k = 0; k < cityGrid.GetLength(1); k++)
                {
                    _money += GetTileEarning(j, k); //Start of turn earnings
                }
            }

            Debug.Log("Start of Turn " + i + ": I have " + _factories + " factories, " + _houses + " houses, and " + _money + " money.");
            if (_money >= TargetAmount)
            {
                Debug.Log("\t\tI HAVE ENOUGH MONEY TO WIN THE GAME!");
                break;
            }
            while (_money >= HouseCost)
            {
                MakeMove();
            }

            Debug.Log("\tEnd of Turn " + i + ": I have " + _factories + " factories, " + _houses + " houses, and " + _money + " money.");
            string grid = "";
            for (int j = 0; j < cityGrid.GetLength(0); j++)
            {
                for (int k = 0; k < cityGrid.GetLength(0); k++)
                {
                    grid += cityGrid[j, k] + ",";
                }
                grid += "\n";
            }
            Debug.Log(grid);
        }
    }

    private void DoOneTurn()
    {
        for (int j = 0; j < cityGrid.GetLength(0); j++)//Check every tile
        {
            for (int k = 0; k < cityGrid.GetLength(1); k++)
            {
                _money += GetTileEarning(j, k); //Start of turn earnings
            }
        }

        Debug.Log("Start of Turn " + _turn + ": I have " + _factories + " factories, " + _houses + " houses, and " + _money + " money.");
        if (_money >= TargetAmount)
        {
            Debug.Log("\t\tI HAVE ENOUGH MONEY TO WIN THE GAME!");
        }
        while (_money >= HouseCost)
        {
            MakeMove();
        }

        Debug.Log("\tEnd of Turn " + _turn + ": I have " + _factories + " factories, " + _houses + " houses, and " + _money + " money.");
        string grid = "";
        for (int j = 0; j < cityGrid.GetLength(0); j++)
        {
            for (int k = 0; k < cityGrid.GetLength(0); k++)
            {
                grid += cityGrid[j, k] + ",";
            }
            grid += "\n";
        }
        Debug.Log(grid);
    }

    private float GetTileEarning(int j, int k)
    {
        if (cityGrid[j, k] == 1)//If there is a factory on this tile
        {
            int borderingHouses = 0;
            int borderingFactories = 0;
            for (int x = 0; x < 3; x++)//Check all bordering tiles
            {
                for (int y = 0; y < 3; y++)
                {
                    int xCoordinate = j - 1 + x;
                    int yCoordinate = k - 1 + y;
                    if ((xCoordinate < 0 || xCoordinate >= cityGrid.GetLength(0)) || (yCoordinate < 0 || yCoordinate >= cityGrid.GetLength(1))) continue;
                    int building = cityGrid[xCoordinate, yCoordinate];
                    if (building == 1)//Keep track of all bordering factories
                    {
                        borderingFactories++;
                    }
                    else if (building == 2)//Keep track of all bordering houses
                    {
                        borderingHouses++;
                    }
                }
            }
            float boostedEarn = FactoryEarn * (1 + (borderingFactories * FactoryMultiplier)); //Calculate the boosted earn
            return FactoryEarn * borderingHouses;
        }
        return 0;
    }

    private void MakeMove()
    {
        int bestMoveX = 0;
        int bestMoveY = 0;
        int bestMoveBuilding = 0;
        float bestMoveValue = -50;
        for (int x = 0; x < cityGrid.GetLength(0); x++)//Check every tile
        {
            for (int y = 0; y < cityGrid.GetLength(1); y++)
            {
                if (cityGrid[x,y] == 0 && (GetBorderingBuildingsOfType(x,y,1) + GetBorderingBuildingsOfType(x, y, 2)) != 0)//If the current tile is empty, and at least one borderig tile has a building on it.
                {
                    if (_money >= FactoryCost)
                    {
                        float moveValue1 = getMoveValue(x, y, 1);
                        if (moveValue1 > bestMoveValue)
                        {
                            bestMoveValue = moveValue1;
                            bestMoveX = x;
                            bestMoveY = y;
                            bestMoveBuilding = 1;
                        }
                    }
                    if (_money >= HouseCost)
                    {
                        float moveValue2 = getMoveValue(x, y, 2);
                        if (moveValue2 > bestMoveValue)
                        {
                            bestMoveValue = moveValue2;
                            bestMoveX = x;
                            bestMoveY = y;
                            bestMoveBuilding = 2;
                        }
                    }
                }
            }
        }
        cityGrid[bestMoveX, bestMoveY] = bestMoveBuilding;
        if (bestMoveBuilding == 1)
        {
            _money -= FactoryCost;
            _factories++;
        }
        else if (bestMoveBuilding == 2)
        {
            _money -= HouseCost;
            _houses++;
        }
    }

    private int GetBorderingBuildingsOfType(int j, int k, int buildingType)
    {
        int borderingBuildings = 0;
        for (int x = 0; x < 3; x++)//Check all bordering tiles
        {
            for (int y = 0; y < 3; y++)
            {
                int xCoordinate = j - 1 + x;
                int yCoordinate = k - 1 + y;
                if ((xCoordinate < 0 || xCoordinate >= cityGrid.GetLength(0)) || (yCoordinate < 0 || yCoordinate >= cityGrid.GetLength(1))) continue;
                int building = cityGrid[xCoordinate, yCoordinate];
                if (building == buildingType)//Keep track of all bordering buildings
                {
                    borderingBuildings++;
                }
            }
        }
        return borderingBuildings;
    }

    private float getMoveValue(int j, int k, int buildingType)
    {
        int originalBuilding = cityGrid[j, k];
        cityGrid[j, k] = buildingType;
        float value = 0; //Remove cost from the value of the move.

        if (buildingType == 1)
        {
            value -= FactoryCost;
        }
        else if (buildingType == 2)
        {
            value -= HouseCost;
        }

        for (int x = 0; x < cityGrid.GetLength(0); x++)//Check every tile
        {
            for (int y = 0; y < cityGrid.GetLength(1); y++)
            {
                value += GetTileEarning(x, y); //Start of turn earnings
            }
        }
        cityGrid[j, k] = originalBuilding;
        return value;
    }
}
