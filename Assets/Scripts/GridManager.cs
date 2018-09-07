using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public CustomTile TilePrefab;
    public int Rows;
    public int Columns;
    public float OffSetBetweenTiles;

    private static CustomTile[,] _tileMap;
    private static CustomTile _selectedTile;


    // Use this for initialization
    void Start()
    {
        DrawMap();
        InvokeRepeating("Blink", 0.5f, 0.5f);
    }

    public static CustomTile[,] GetTileMap()
    {
        return _tileMap;
    }
    public static void SetSelectedTile(CustomTile pCustomTile)
    {
        _selectedTile = pCustomTile;
    }
    public static CustomTile GetSelectedTile()
    {
        return _selectedTile;
    }
    public void ChangeSelectedTile(InputHandler.DirectionKey pDirection)
    {
        _selectedTile.Reset();
        int[] Position = GetTilePosition(_selectedTile);
        switch (pDirection)
        {
            case InputHandler.DirectionKey.LEFT:
                Position[0] = Mathf.Clamp(Position[0] - 1, 0, _tileMap.GetLength(0) - 1);
                break;
            case InputHandler.DirectionKey.RIGHT:
                Position[0] = Mathf.Clamp(Position[0] + 1, 0, _tileMap.GetLength(0) - 1);
                break;
            case InputHandler.DirectionKey.UP:
                Position[1] = Mathf.Clamp(Position[1] + 1, 0, _tileMap.GetLength(1) - 1);
                break;
            case InputHandler.DirectionKey.DOWN:
                Position[1] = Mathf.Clamp(Position[1] - 1, 0, _tileMap.GetLength(1) - 1);
                break;
        }
        _selectedTile = GetTileAtPosition(Position[0], Position[1]);
    }
    private void DrawMap()
    {
        _tileMap = new CustomTile[Rows, Columns];
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                CustomTile tile = Instantiate(TilePrefab);
                tile.transform.position = new Vector3((row * tile.transform.localScale.x) + row * OffSetBetweenTiles, 0, (tile.transform.localScale.z * col) + col * OffSetBetweenTiles);
                tile.transform.parent = this.transform;
                _tileMap[row, col] = tile;
            }
        }
        //Sets first tile to active.
        _selectedTile = GetTileAtPosition(0, 0);
    }
    private void Blink()
    {
        if (InputHandler.currentMode == InputHandler.CurrentMode.SELECTINGTILE)
        {
            _selectedTile.InvertColor();
        }
    }

    //Gets the Tile position on the tilemap as X and Y coordinate.
    public static int[] GetTilePosition(CustomTile pCustomTile)
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

    public static CustomTile GetTileAtPosition(int xCoordinate, int yCoordinate)
    {
        return _tileMap[xCoordinate, yCoordinate];
    }

    /// <summary>
    /// Returns the buildings in a list, around the selected tile. First parameter gives the radius of it.
    /// </summary>
    /// <param name="pAmountOfTiles"></param>
    /// <param name="pTargetTile"></param>
    /// <returns></returns>
    public static List<Building> GetBuildingsAroundTile(int pAmountOfTiles, CustomTile pTargetTile)
    {
        List<Building> Buildings = new List<Building>();
        int[] Coordinates = GetTilePosition(pTargetTile);
        //This is the maximum difference between the first tile and the last tile.
        int MaxOffSet = (pAmountOfTiles * 2) + 1;

        for (int x = 0; x < MaxOffSet; x++)
        {
            for (int y = 0; y < MaxOffSet; y++)
            {
                if (pAmountOfTiles + y == 0 && pAmountOfTiles + x == 0) continue;
                Building building = _tileMap[Coordinates[0] - pAmountOfTiles + y, Coordinates[1] - pAmountOfTiles + x].GetBuildingOnTile();
                if (building != null) Buildings.Add(building);
            }
        }

        foreach (Building building in Buildings)
        {
            Debug.Log(building.name);
        }
        return Buildings;
    }
}
