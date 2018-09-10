using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    private City _myCity;
    private BuildingHandler _buildingHandler;
    private UIHandler _uiHandler;

    public enum DirectionKey { LEFT, RIGHT, UP, DOWN };

    //This one belongs in the game handler.
    public enum CurrentMode { SELECTINGTILE, BUILDINGTILE };
    public static CurrentMode currentMode;

    private Building placementBuilding;

    public InputHandler Initialize(City pCity, BuildingHandler pBuildingHandler, UIHandler pUIHandler)
    {
        _myCity = pCity;
        _buildingHandler = pBuildingHandler;
        _uiHandler = pUIHandler;
        currentMode = CurrentMode.SELECTINGTILE;

        return this;
    }

    // Use this for initialization
    void Start()
    {

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
        //T is for testing, dat doe je met vrienden. U is voor u en mij.
        if(Input.GetKeyDown(KeyCode.T))
        {
            _myCity.CollectFromAllBuildings();
        }
        if (currentMode == CurrentMode.SELECTINGTILE)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _myCity.ChangeSelectedTile(CityManager.DirectionKey.RIGHT);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _myCity.ChangeSelectedTile(CityManager.DirectionKey.LEFT);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _myCity.ChangeSelectedTile(CityManager.DirectionKey.UP);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _myCity.ChangeSelectedTile(CityManager.DirectionKey.DOWN);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                currentMode = CurrentMode.BUILDINGTILE;
                _myCity.GetSelectedTile().Reset();
                _uiHandler.ToggleBuildPanel(true);
            }
        }
        if (currentMode == CurrentMode.BUILDINGTILE)
        {
            //TODO: Make an event from this and put in buildinghandler.
            //Places a building in placement mode, can switch between buildings.
            if (_buildingHandler.PlacementBuildingActive())
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    //BuildingHandler should probably tell UIHandler what to do.
                    _buildingHandler.ChangeBuildingSelection(1);
                    _uiHandler.SetActiveBuildingImage(1);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    _buildingHandler.ChangeBuildingSelection(-1);
                    _uiHandler.SetActiveBuildingImage(-1);
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    _buildingHandler.StartBuilding();
                    currentMode = CurrentMode.SELECTINGTILE;
                    _uiHandler.ToggleBuildPanel(false);
                }

                if (Input.GetKeyDown(KeyCode.G))
                {
                    _buildingHandler.DestroyPlacementBuilding();
                    currentMode = CurrentMode.SELECTINGTILE;
                    _uiHandler.ToggleBuildPanel(false);
                }
            }
            else
            {
                _buildingHandler.ChangeBuildingSelection(0, false);
                _uiHandler.SetActiveBuildingImage(0, false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UIHandler.ShowNotification("Turn has ended");
        }
    }
}