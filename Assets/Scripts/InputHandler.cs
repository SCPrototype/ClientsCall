using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{

    private SelectedTileHandler _selectedTileHandler;
    private BuildingHandler _buildingHandler;

    public GameObject BuildingPanel;
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
            //TODO: Make an event from this and put in buildinghandler.
            //Places a building in placement mode, can switch between buildings.
            if (placementBuilding != null)
            {
                images[1].color = new Color(1, 1, 1, 0.5f);
                images[0].color = new Color(1, 1, 1, 1);
                //TODO: Make an Array and swap between buildings.
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    DestroyPlacementBuilding();
                    placementBuilding = _buildingHandler.PlaceBuilding(SelectedTileHandler.GetSelectedTile(), BigHouse);
                    images[0].color = new Color(1, 1, 1, 0.5f);
                    images[1].color = new Color(1, 1, 1, 1);
                }
                //TODO: Make an Array and swap between buildings.
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    DestroyPlacementBuilding();
                    placementBuilding = _buildingHandler.PlaceBuilding(SelectedTileHandler.GetSelectedTile(), SmallHouse);
                    images[0].color = new Color(1, 1, 1, 1);
                    images[1].color = new Color(1, 1, 1, 0.5f);
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    placementBuilding.SetBuildingPhase(Building.BuildingPhase.INPROGRESS);
                    DestroyPlacementBuilding();
                    currentMode = CurrentMode.SELECTINGTILE;
                    BuildingPanel.SetActive(false);
                    placementBuilding = null;
                }

                if(Input.GetKeyDown(KeyCode.G))
                {
                    DestroyPlacementBuilding();
                    currentMode = CurrentMode.SELECTINGTILE;
                    BuildingPanel.SetActive(false);
                    placementBuilding = null;
                }

            } else
            {
                placementBuilding = _buildingHandler.PlaceBuilding(SelectedTileHandler.GetSelectedTile(), SmallHouse);
            }
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            UIHandler.ShowNotification("Turn has ended");
        }
    }

    private void DestroyPlacementBuilding()
    {
        Building[] buildings = FindObjectsOfType<Building>();

        foreach(Building pBuilding in buildings)
        {
            if(pBuilding.GetBuildingPhase() == Building.BuildingPhase.PLACEMENT)
            {
                Destroy(pBuilding.gameObject);
            }
        }
    }

}
