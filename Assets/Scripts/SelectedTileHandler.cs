using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTileHandler : MonoBehaviour
{

    private static CustomTile _selectedTile;
    private CustomTile[,] _tileList;

    //Assuming the list always starts at 0,0
    private int xIndexOfList = 0;
    private int yIndexOfList = 0;

    public static void SetSelectedTile(CustomTile pCustomTile)
    {
        _selectedTile = pCustomTile;
    }

    public static CustomTile GetSelectedTile()
    {
        return _selectedTile;
    }

    private void Start()
    {
        InvokeRepeating("Blink", 0.5f, 0.5f);
    }

    private void InitializeList()
    {
        if (_tileList == null)
        {
            _tileList = TileCreater.GetTileMap();
        }
    }

    public void ChangeSelectedTile(int x, int y)
    {
        InitializeList();
        //Assuming the list starts at 0, need a better way to keep track of the index.
        //Bigger than 0 and lower than max length.
        if (xIndexOfList >= 0 && xIndexOfList < _tileList.GetLength(0) && yIndexOfList >= 0 && yIndexOfList < _tileList.GetLength(1))
        {            
            xIndexOfList += x;
            if (xIndexOfList < 0)
            { xIndexOfList = 0; } if (xIndexOfList >= _tileList.GetLength(0))
            { xIndexOfList = _tileList.GetLength(0) -1; }
            yIndexOfList += y;
            if (yIndexOfList < 0)
            { yIndexOfList = 0; } if (yIndexOfList >= _tileList.GetLength(1))
            { yIndexOfList = _tileList.GetLength(1) -1; }

            _selectedTile.Reset();
            _selectedTile = _tileList[xIndexOfList, yIndexOfList];
        }
        //int[] tilePosition = TileCreater.GetTilePosition(_selectedTile);
        //Debug.Log("Current selected tile is: " + tilePosition[0] + ", " + tilePosition[1]);

    }

    public void ResetSelectedTile()
    {
        _selectedTile.Reset();
    }

    private void Blink()
    {
        if (InputHandler.currentMode == InputHandler.CurrentMode.SELECTINGTILE)
        {
            _selectedTile.InvertColor();
        }
    }
}
