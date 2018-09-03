using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreater : MonoBehaviour {

    public CustomTile TilePrefab;
    public int Rows;
    public int Columns;
    private CustomTile[,] tileMap;

	// Use this for initialization
	void Start () {
        tileMap = new CustomTile[Rows, Columns];
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                CustomTile tile = Instantiate(TilePrefab);
                tile.transform.position = new Vector3(row * tile.transform.localScale.x, 0.25f, tile.transform.localScale.z * col);
                tile.transform.parent = this.transform;
                tileMap[row, col] = tile;
            }
        }

        //Sets first tile to active.
        SelectedTileHandler.SetSelectedTile(tileMap[0, 0]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
