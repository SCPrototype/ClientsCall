using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCityManager : CityManager
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void HandleTurn(City pCity)
    {
        //T is for testing, dat doe je met vrienden. U is voor u en mij.
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                pCity.CollectFromAllBuildings();
            }
            if (currentMode == CurrentMode.SELECTINGTILE)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    pCity.ChangeSelectedTile(DirectionKey.RIGHT);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    pCity.ChangeSelectedTile(DirectionKey.LEFT);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    pCity.ChangeSelectedTile(DirectionKey.UP);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    pCity.ChangeSelectedTile(DirectionKey.DOWN);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (pCity.GetSelectedTile().GetBuildingOnTile() == null)
                    {
                        currentMode = CurrentMode.BUILDINGTILE;
                        pCity.GetSelectedTile().Reset();
                        GameInitializer.GetUIHandler().ToggleBuildPanel(true);
                    }
                    else
                    {
                        currentMode = CurrentMode.EXAMINEMODE;
                        GameInitializer.GetUIHandler().SetExamineMode(pCity.GetSelectedTile().GetBuildingOnTile());
                        pCity.GetSelectedTile().Reset();
                        
                    }
                }
            }
            if (currentMode == CurrentMode.BUILDINGTILE)
            {
                //Places a building in placement mode, can switch between buildings.
                if (GameInitializer.GetBuildingHandler().PlacementBuildingActive())
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        //BuildingHandler should probably tell UIHandler what to do.
                        GameInitializer.GetBuildingHandler().ChangeBuildingSelection(1);
                        GameInitializer.GetUIHandler().SetActiveBuildingImage(1);
                    }
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        GameInitializer.GetBuildingHandler().ChangeBuildingSelection(-1);
                        GameInitializer.GetUIHandler().SetActiveBuildingImage(-1);
                    }

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (GameInitializer.GetBuildingHandler().StartBuilding())
                        {
                            currentMode = CurrentMode.SELECTINGTILE;
                            GameInitializer.GetUIHandler().ToggleBuildPanel(false);
                        }
                        else
                        {
                            //TODO
                            //Error, not enough funds.
                        }

                    }

                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        GameInitializer.GetBuildingHandler().DestroyPlacementBuilding();
                        currentMode = CurrentMode.SELECTINGTILE;
                        GameInitializer.GetUIHandler().ToggleBuildPanel(false);
                    }
                }
                else
                {
                    GameInitializer.GetBuildingHandler().ChangeBuildingSelection(0, false);
                    GameInitializer.GetUIHandler().SetActiveBuildingImage(0, false);
                }
            }
            if(currentMode == CurrentMode.EXAMINEMODE)
            {
                if(Input.GetKeyDown(KeyCode.G))
                {
                    GameInitializer.GetUIHandler().ToggleExaminePanel(false);
                    currentMode = CurrentMode.SELECTINGTILE;
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameInitializer.EndTurn();
                //Debug.Log(GameInitializer.GetBuildingHandler().GetCurrentCity());
                UIHandler.ShowNotification("Turn has ended");
            }
        }
    }
}
