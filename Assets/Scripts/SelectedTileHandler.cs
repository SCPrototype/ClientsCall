using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTileHandler : MonoBehaviour {

    private static CustomTile _selectedTile;

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
    
    private void Blink()
    {
        _selectedTile.InvertColor();
    }
}
