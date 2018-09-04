using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    private SelectedTileHandler _selectedTileHandler;
    private BuildingHandler _buildingHandler;

    public GameObject BuildingPanel;
    public enum CurrentMode { SELECTINGTILE, BUILDINGTILE };
    public static CurrentMode currentMode;

    // Use this for initialization
    void Start()
    {
        _selectedTileHandler = GetComponent<SelectedTileHandler>();
        _buildingHandler = GetComponent<BuildingHandler>();
        currentMode = CurrentMode.SELECTINGTILE;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            HandleInput();
        }
    }

    public void HandleInput()
    {
        if (currentMode == CurrentMode.SELECTINGTILE)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _selectedTileHandler.ChangeSelectedTile(1, 0);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _selectedTileHandler.ChangeSelectedTile(-1, 0);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _selectedTileHandler.ChangeSelectedTile(0, 1);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _selectedTileHandler.ChangeSelectedTile(0, -1);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                currentMode = CurrentMode.BUILDINGTILE;
                _selectedTileHandler.ResetSelectedTile();
                BuildingPanel.SetActive(true);
            }
        }
        if (currentMode == CurrentMode.BUILDINGTILE)
        {
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                _buildingHandler.PlaceBuilding(SelectedTileHandler.GetSelectedTile(), new Building());
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                currentMode = CurrentMode.SELECTINGTILE;
                BuildingPanel.SetActive(false);
            }
        }
    }
}
