using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    CityManager _myManager;

    private CustomTile TilePrefab;
    private int Rows;
    private int Columns;
    private float OffSetBetweenTiles;
    private EventManager _eventManager;
    private float _budget = 75;
    private float _happiness = 75;
    private bool _collectedThisTurn = false;
    private CustomTile[,] _tileMap;
    private CustomTile _selectedTile;
    private UIHandler _uiHandler;

    private int _currentTurn = 1;

    public City Initialize(CityManager pManager, int pRows, int pColumns, float pOffset, Vector3 pStartPos)
    {
        _myManager = pManager;

        Rows = pRows;
        Columns = pColumns;
        OffSetBetweenTiles = pOffset;

        TilePrefab = (Resources.Load(Glob.tilePrefab) as GameObject).GetComponent<CustomTile>();
        transform.position = pStartPos;

        _eventManager = GameObject.FindGameObjectWithTag("EventMenu").GetComponent<EventManager>();
        _uiHandler = GameInitializer.GetUIHandler();
 
        DrawMap(pStartPos);
        return this;
    }

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Blink", 0.5f, 0.5f);
        _uiHandler.SetResourcesBars((int)_budget, (int)_happiness);
    }

    void Update()
    {
        if (GameInitializer.GetBuildingHandler().GetCurrentCity() == this && GameInitializer.GetBuildingHandler().IsReadyToBuild())
        {
            if (!GameInitializer.GetPaused()) {
                if (_currentTurn > Glob.TurnAmount)
                {
                    GameInitializer.EndGame();
                    return;
                }
                if (!_collectedThisTurn)
                {
                    GameInitializer.GetUIHandler().SetTurnText(_currentTurn);
                    CollectFromAllBuildings();
                    _collectedThisTurn = true;
                    if (_budget < 18)
                    {
                        _myManager.TaxCity(this);
                    }
                    if (_currentTurn % Glob.EventTurnInterval == 0 && _myManager is PlayerCityManager)
                    {
                        _eventManager.EnableRandomEvent();
                    }
                    _uiHandler.SetResourcesBars((int)_budget, (int)_happiness); //Just in case no buildings collected anything
                }
                _myManager.HandleTurn(this);
            }
        }
        else if (_collectedThisTurn)
        {
            _collectedThisTurn = false;
            _currentTurn++;
        }
    }


    public CustomTile[,] GetTileMap()
    {
        return _tileMap;
    }
    public void SetSelectedTile(CustomTile pCustomTile)
    {
        _selectedTile = pCustomTile;
    }
    public CustomTile GetSelectedTile()
    {
        return _selectedTile;
    }
    public void ChangeSelectedTile(CityManager.DirectionKey pDirection)
    {
        _selectedTile.Reset();
        int[] Position = GetTilePosition(_selectedTile);
        switch (pDirection)
        {
            case CityManager.DirectionKey.LEFT:
                Position[0] = Mathf.Clamp(Position[0] - 1, 0, _tileMap.GetLength(0) - 1);
                break;
            case CityManager.DirectionKey.RIGHT:
                Position[0] = Mathf.Clamp(Position[0] + 1, 0, _tileMap.GetLength(0) - 1);
                break;
            case CityManager.DirectionKey.UP:
                Position[1] = Mathf.Clamp(Position[1] + 1, 0, _tileMap.GetLength(1) - 1);
                break;
            case CityManager.DirectionKey.DOWN:
                Position[1] = Mathf.Clamp(Position[1] - 1, 0, _tileMap.GetLength(1) - 1);
                break;
        }
        _selectedTile = GetTileAtPosition(Position[0], Position[1]);
    }
    private void DrawMap(Vector3 startPos)
    {
        _tileMap = new CustomTile[Rows, Columns];
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                CustomTile tile = Instantiate(TilePrefab);
                tile.SetCity(this);
                tile.transform.position = startPos + new Vector3((row * tile.transform.localScale.x) + row * OffSetBetweenTiles, 0, (tile.transform.localScale.z * col) + col * OffSetBetweenTiles);
                tile.transform.parent = this.transform;
                _tileMap[row, col] = tile;
            }
        }

        for (int i = 0; i < Glob.RandomHouseAmount; i++)
        {
            CustomTile targetTile = _tileMap[Random.Range(0, Rows), Random.Range(0, Columns)];
            if (targetTile.GetBuildingOnTile() == null)
            {
                GameInitializer.GetBuildingHandler().QuickBuildBuilding(this, targetTile, 0);
            } else
            {
                i--;
            }
        }
        for (int i = 0; i < Glob.RandomFactoryAmount; i++)
        {
            CustomTile targetTile = _tileMap[Random.Range(0, Rows), Random.Range(0, Columns)];
            if (targetTile.GetBuildingOnTile() == null)
            {
                GameInitializer.GetBuildingHandler().QuickBuildBuilding(this, targetTile, 1);
            }
            else
            {
                i--;
            }
        }
        for (int i = 0; i < Glob.RandomParkAmount; i++)
        {
            CustomTile targetTile = _tileMap[Random.Range(0, Rows), Random.Range(0, Columns)];
            if (targetTile.GetBuildingOnTile() == null)
            {
                GameInitializer.GetBuildingHandler().QuickBuildBuilding(this, targetTile, 2);
            }
            else
            {
                i--;
            }
        }

        //Sets first tile to active.
        _selectedTile = GetTileAtPosition(0, 0);
    }
    private void Blink()
    {
        if (_myManager.GetCurrentMode() != CityManager.CurrentMode.WAITINGFORTURN)
        {
            if (_selectedTile != null)
            {
                _selectedTile.InvertColor();
            }
        }
    }

    //Gets the Tile position on the tilemap as X and Y coordinate.
    public int[] GetTilePosition(CustomTile pCustomTile)
    {
        int[] coOrdinates = new int[2];
        int arrayWidth = _tileMap.GetLength(0);
        int arrayLength = _tileMap.GetLength(1);

        for (int xCo = 0; xCo < arrayWidth; xCo++)
        {
            for (int yCo = 0; yCo < arrayLength; yCo++)
            {
                if (_tileMap[xCo, yCo].Equals(pCustomTile))
                {
                    coOrdinates[0] = xCo;
                    coOrdinates[1] = yCo;
                    return coOrdinates;
                }
            }
        }
        return coOrdinates;
    }

    public CustomTile GetTileAtPosition(int xCoordinate, int yCoordinate)
    {
        return _tileMap[xCoordinate, yCoordinate];
    }

    /// <summary>
    /// Returns the buildings in a list, around the selected tile. First parameter gives the radius of it.
    /// </summary>
    /// <param name="pAmountOfTiles"></param>
    /// <param name="pTargetTile"></param>
    /// <returns></returns>
    public Building[] GetBuildingsAroundTile(int pAmountOfTiles, CustomTile pTargetTile)
    {
        List<Building> Buildings = new List<Building>();
        int[] Coordinates = GetTilePosition(pTargetTile);
        //This is the maximum difference between the first tile and the last tile.
        int MaxOffSet = (pAmountOfTiles * 2) + 1;

        for (int x = 0; x < MaxOffSet; x++)
        {
            for (int y = 0; y < MaxOffSet; y++)
            {
                int xCoordinate = Coordinates[0] - pAmountOfTiles + x;
                int yCoordinate = Coordinates[1] - pAmountOfTiles + y;
                if ((xCoordinate < 0 || xCoordinate >= _tileMap.GetLength(0)) || (yCoordinate < 0 || yCoordinate >= _tileMap.GetLength(1))) continue;
                Building building = _tileMap[xCoordinate, yCoordinate].GetBuildingOnTile();
                if (building != null) Buildings.Add(building);
            }
        }

        return Buildings.ToArray();
    }

    public void CollectFromAllBuildings()
    {
        foreach (CustomTile pTile in _tileMap)
        {
            Building building = pTile.GetBuildingOnTile();
            if (building != null)
            {
                if (building is ProductionBuilding)
                {
                    ProductionBuilding productionBuilding = building as ProductionBuilding;
                    productionBuilding.Produce();
                }
            }
        }
    }

    public void ReceiveCollection(int pBudget, int pHappiness)
    {
        BudgetChange(pBudget);
        HappinessChange(pHappiness);
    }

    public void BudgetChange(int pChange)
    {
        //Debug.Log("Budget + earnings = " + _budget + " + " + pChange + " = " + (_budget + pChange));
        _budget += pChange;
        //softcap for now.
        _budget = Mathf.Clamp(_budget, 0, 100);

        _uiHandler.SetResourcesBars((int)_budget, (int)_happiness);
    }

    public void HappinessChange(int pChange)
    {
        _happiness += pChange;
        //softcap for now.
        _happiness = Mathf.Clamp(_happiness, 0, 100);

        _uiHandler.SetResourcesBars((int)_budget, (int)_happiness);
    }

    public bool CanBuild(int pBuildingCost)
    {
        if(_budget - pBuildingCost < 0)
        {
            return false;
        } else
        {
            return true;
        }
    }

    public void SetCurrentMode(CityManager.CurrentMode pMode)
    {
        _myManager.SetCurrentMode(pMode);
    }

    public float GetBudget()
    {
        return _budget;
    }
    public float GetHappiness()
    {
        return _happiness;
    }

    public float GetScore()
    {
        float score = 0;
        score += _budget * (1 + (_happiness/25)); //100% happiness: max score is 500, 0% happiness: max score is 100
        float tileScore = 0;
        Debug.Log("Budget and Happiness score: " + score);
        foreach (CustomTile tile in _tileMap)
        {
            tileScore += _myManager.GetTileValue(tile);//Only factories: +/- 1000 tileScore
        }
        Debug.Log("City tiles score: " + tileScore);

        score += tileScore / 10;

        return score;
    }
}
