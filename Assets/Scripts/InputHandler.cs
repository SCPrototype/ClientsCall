using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{

    private GridManager _gridManager;
    private BuildingHandler _buildingHandler;

    public GameObject BuildingPanel;
    public enum DirectionKey { LEFT, RIGHT, UP, DOWN };

    //This one belongs in the game handler.
    public enum CurrentMode { SELECTINGTILE, BUILDINGTILE };
    public static CurrentMode currentMode;

    private Building placementBuilding;
    public Building SmallHouse;
    public Building BigHouse;

    //TODO: UI handler and shit.
    
    public Image[] images;

    // Use this for initialization
    void Start()
    {
        _gridManager = GetComponent<GridManager>();
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
        if(Input.GetKeyDown(KeyCode.T))
        {
            GridManager.GetBuildingsAroundTile(1, GridManager.GetSelectedTile());
        }
        if (currentMode == CurrentMode.SELECTINGTILE)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _gridManager.ChangeSelectedTile(DirectionKey.RIGHT);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _gridManager.ChangeSelectedTile(DirectionKey.LEFT);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _gridManager.ChangeSelectedTile(DirectionKey.UP);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _gridManager.ChangeSelectedTile(DirectionKey.DOWN);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                currentMode = CurrentMode.BUILDINGTILE;
                //_selectedTileHandler.ResetSelectedTile();
                BuildingPanel.SetActive(true);
            }
        }
        if (currentMode == CurrentMode.BUILDINGTILE)
        {
            //TODO: Make an event from this and put in buildinghandler.
            //Places a building in placement mode, can switch between buildings.
            if (placementBuilding != null)
            {
               
                //TODO: Make an Array and swap between buildings.
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    DestroyPlacementBuilding();
                    placementBuilding = _buildingHandler.PlaceBuilding(GridManager.GetSelectedTile(), BigHouse);
                }
                //TODO: Make an Array and swap between buildings.
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    DestroyPlacementBuilding();
                    placementBuilding = _buildingHandler.PlaceBuilding(GridManager.GetSelectedTile(), SmallHouse);
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    _buildingHandler.StartBuilding(placementBuilding, GridManager.GetSelectedTile());
                    DestroyPlacementBuilding();
                    currentMode = CurrentMode.SELECTINGTILE;
                    BuildingPanel.SetActive(false);
                    placementBuilding = null;
                }

                if (Input.GetKeyDown(KeyCode.G))
                {
                    DestroyPlacementBuilding();
                    currentMode = CurrentMode.SELECTINGTILE;
                    BuildingPanel.SetActive(false);
                    placementBuilding = null;
                }

            }
            else
            {
                placementBuilding = _buildingHandler.PlaceBuilding(GridManager.GetSelectedTile(), SmallHouse);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UIHandler.ShowNotification("Turn has ended");
        }
    }

    private void DestroyPlacementBuilding()
    {
        Building[] buildings = FindObjectsOfType<Building>();

        foreach (Building pBuilding in buildings)
        {
            if (pBuilding.GetBuildingPhase() == Building.BuildingPhase.PLACEMENT)
            {
                Destroy(pBuilding.gameObject);
            }
        }
    }

}