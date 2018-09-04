using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreater : MonoBehaviour {

    public CustomTile TilePrefab;
    public int Rows;
    public int Columns;
    public float OffSetBetweenTiles;
    private static CustomTile[,] _tileMap;

	// Use this for initialization
	void Start () {
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
        SelectedTileHandler.SetSelectedTile(_tileMap[0, 0]);
	}

    public static CustomTile[,] GetTileMap()
    {
        return _tileMap;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
