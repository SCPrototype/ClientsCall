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
    private float _budget;
    private bool _collectedThisTurn = false;
    private CustomTile[,] _tileMap;
    private CustomTile _selectedTile;
    private UIHandler _uiHandler;
    private int _amountOfRelics;
    private int _missilesLaunched;
    private int _bridgesBuilt;
    private SoundHandler _soundHandler;
    private MayorOffice _mayorOffice;

    private int _currentTurn = 1;

    public City Initialize(CityManager pManager, int pRows, int pColumns, float pOffset, Vector3 pStartPos)
    {
        _budget = Glob.StartingBudget;

        _myManager = pManager;

        Rows = pRows;
        Columns = pColumns;
        OffSetBetweenTiles = pOffset;

        TilePrefab = (Resources.Load(Glob.TilePrefab) as GameObject).GetComponent<CustomTile>();
        transform.position = pStartPos;

        _eventManager = GameObject.FindGameObjectWithTag("EventMenu").GetComponent<EventManager>();
        _uiHandler = GameInitializer.GetUIHandler();
        _soundHandler = GameInitializer.GetSoundHandler();
 
        DrawMap(pStartPos);
        return this;
    }

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Blink", 0.5f, 0.5f);
        _uiHandler.SetResourcesBars((int)_budget);
        _mayorOffice.UpdateHatPosition(_budget);
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
                    UIHandler.ToggleNotificationPanel(false);

                    CollectFromAllBuildings();
                    if (_myManager is PlayerCityManager)
                    {
                        AICityManager AICity = GameInitializer.GetNextCity(this).GetManager() as AICityManager;
                        AICity.ChangeAnimosity(-Mathf.RoundToInt(GetHappyHouseAmount() * Glob.HappyHouseAnimosityChange), GameInitializer.GetNextCity(this));
                    }
                    _collectedThisTurn = true;
                    if (_currentTurn % Glob.EventTurnInterval == 0 && _myManager is PlayerCityManager)
                    {
                        _eventManager.EnableRandomEvent();
                    }
                    _mayorOffice.UpdateHatPosition(_budget);
                    _uiHandler.SetResourcesBars((int)_budget); //Just in case no buildings collected anything
                    if (_currentTurn >= 6 && _currentTurn <= 8 && _myManager is PlayerCityManager)
                    {
                        switch (_currentTurn)
                        {
                            case 6:
                                int scoreTurn6 = Mathf.Clamp(Glob.OptimalBudgetTurn6 - (int)_budget, 0, 10);
                                GameInitializer.AddAchieverScore(10 - scoreTurn6);
                                break;
                            case 7:
                                int scoreTurn7 = Mathf.Clamp(Glob.OptimalBudgetTurn7 - (int)_budget, 0, 20) / 2;
                                GameInitializer.AddAchieverScore(10 - scoreTurn7);
                                break;
                            case 8:
                                int scoreTurn8 = Mathf.Clamp(Glob.OptimalBudgetTurn8 - (int)_budget, 0, 40) / 4;
                                GameInitializer.AddAchieverScore(10 - scoreTurn8);
                                break;
                            default:
                                break;
                        }
                    }
                }
                _myManager.HandleTurn(this);
            }
        }
        else if (_collectedThisTurn)
        {
            _collectedThisTurn = false;
            _currentTurn++;
        }
        else if (GameInitializer.GetBuildingHandler().GetCurrentCity() == this && !GameInitializer.GetBuildingHandler().IsReadyToBuild())
        {
            _uiHandler.SetResourcesBars((int)_budget); //Just in case no buildings collected anything
        }
    }

    public int GetCurrentTurn()
    {
        return _currentTurn;
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
        _soundHandler.PlaySound(SoundHandler.Sounds.MOVE);
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

        CustomTile targetTile4 = _tileMap[5, 4];
        GameInitializer.GetBuildingHandler().QuickBuildBuilding(this, targetTile4, 7);

        if (_myManager is AICityManager)
        {
            CustomTile targetTile1 = _tileMap[3, 4];
            GameInitializer.GetBuildingHandler().QuickBuildBuilding(this, targetTile1, 0);

            CustomTile targetTile2 = _tileMap[3, 3];
            GameInitializer.GetBuildingHandler().QuickBuildBuilding(this, targetTile2, 1);

            CustomTile targetTile3 = _tileMap[4, 4];
            GameInitializer.GetBuildingHandler().QuickBuildBuilding(this, targetTile3, 2);

            _budget = 10;
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
                if (_myManager.GetCurrentMode() != CityManager.CurrentMode.MISSILEAIM)
                {
                    _selectedTile.InvertColor(new Color(0,0,0,0));
                }
                else
                {
                    _selectedTile.InvertColor(Glob.MissileAimColor);
                }
            }
        }
    }

    public void SetMayorOffice(MayorOffice pMayorOffice)
    {
        _mayorOffice = pMayorOffice;
    }

    public MayorOffice GetMayorOffice()
    {
        return _mayorOffice;
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
        float incomeThisTurn = 0;
        foreach (CustomTile pTile in _tileMap)
        {
            Building building = pTile.GetBuildingOnTile();
            if (building != null)
            {
                if (building is ProductionBuilding)
                {
                    ProductionBuilding productionBuilding = building as ProductionBuilding;
                    incomeThisTurn += productionBuilding.Produce();
                }
                if (building is Digsite)
                {
                    Digsite digSite = building as Digsite;
                    digSite.DoAction();
                }
            }
        }
        ReceiveCollection((int)incomeThisTurn);
    }

    public void ReceiveCollection(int pBudget)
    {
        BudgetChange(pBudget);

    }

    public void BudgetChange(int pChange)
    {
        //Debug.Log("Budget + earnings = " + _budget + " + " + pChange + " = " + (_budget + pChange));
        _budget = Mathf.Clamp(_budget + pChange, 0, Glob.BudgetCap);
        //softcap for now.
        _mayorOffice.UpdateHatPosition(_budget);
        _uiHandler.SetResourcesBars((int)_budget);
    }

   

    public bool CanBuild(int pBuildingCost, int happyHouses = 0)
    {
        if(_budget - pBuildingCost < 0 || GetHappyHouseAmount() < happyHouses)
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

    public void HandleHappiness(CustomTile pTile, bool pHappy)
    {
        int[] tilePos = GetTilePosition(pTile);
        for (int x = 0; x < 3; x++)//Check all bordering tiles
        {
            for (int y = 0; y < 3; y++)
            {
                int xCoordinate = tilePos[0] - 1 + x;
                int yCoordinate = tilePos[1] - 1 + y;
                if ((xCoordinate < 0 || xCoordinate >= _tileMap.GetLength(0)) || (yCoordinate < 0 || yCoordinate >= _tileMap.GetLength(1))) continue;
                CustomTile borderingTile = _tileMap[xCoordinate, yCoordinate];
                if (!borderingTile.GetIsHappy())
                {
                    borderingTile.SetIsHappy(pHappy);
                    if (borderingTile.GetBuildingOnTile() is House && _myManager is PlayerCityManager && pHappy)
                    {
                        GameInitializer.AddSocializerScore(2);
                    }
                }
            }
        }
    }
    public int GetHappyHouseAmount()
    {
        int happyHouses = 0;
        for (int x = 0; x < _tileMap.GetLength(0); x++)//Check every tile
        {
            for (int y = 0; y < _tileMap.GetLength(1); y++)
            {
                if (_tileMap[x,y].GetIsHappy())
                {
                    if (_tileMap[x,y].GetBuildingOnTile() is House)
                    {
                        happyHouses++;
                    }
                }
            }
        }
        return happyHouses;
    }

    public void AddRelic()
    {
        _amountOfRelics++;
        if (_amountOfRelics <= Glob.AmountOfRelicsNeededToWin && _myManager is PlayerCityManager)
        {
            GameInitializer.AddExplorerScore(6);
        }
        if (_amountOfRelics >= Glob.AmountOfRelicsNeededToWin)
        {
            GameInitializer.EndGame(false, this);
        }
    }
    public int GetRelicAmount()
    {
        return _amountOfRelics;
    }

    public void AddMissileLaunched()
    {
        _missilesLaunched++;
        if (_myManager is PlayerCityManager)
        {
            AICityManager AICity = GameInitializer.GetNextCity(this).GetManager() as AICityManager;
            AICity.ChangeAnimosity(50, GameInitializer.GetNextCity(this));
        }
        if (_myManager is PlayerCityManager)
        {
            GameInitializer.AddKillerScore(20);
        }
        if (_missilesLaunched >= Glob.AmountOfMissilesNeededToWin)
        {
            GameInitializer.EndGame(false, this);
        }
    }

    public void AddBridgeBuilt()
    {
        _bridgesBuilt++;

        if (_bridgesBuilt <= Glob.AmountOfBridgesNeededToWin && _myManager is PlayerCityManager)
        {
            GameInitializer.AddSocializerScore(30);
        }
        if (_bridgesBuilt >= Glob.AmountOfBridgesNeededToWin)
        {
            if (GameInitializer.GetNextCity(this).GetBridgesBuilt() >= Glob.AmountOfBridgesNeededToWin)
            {
                GameInitializer.EndGame(true);
            }
        }

    }
    public int GetBridgesBuilt()
    {
        return _bridgesBuilt;
    }

    public float GetScore()
    {
        float score = 0;
        score += _budget;
        float tileScore = 0;
        Debug.Log("Budget and Happiness score: " + score);
        foreach (CustomTile tile in _tileMap)
        {
            tileScore += _myManager.GetTileValue(tile);
        }
        Debug.Log("City tiles score: " + tileScore);

        score += tileScore / 10;

        return score;
    }

    public CityManager GetManager()
    {
        return _myManager;
    }

    public void HighlightTile(int pX, int pY, bool pToggle)
    {
        if (pToggle)
        {
            _tileMap[pX, pY].PlayParticle();
        } else
        {
            _tileMap[pX, pY].StopParticle();
        }
    }
}
