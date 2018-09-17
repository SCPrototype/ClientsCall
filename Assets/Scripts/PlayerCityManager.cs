﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCityManager : CityManager
{

    private bool _isFocusedOnOwnCity = true;
    private SoundHandler _soundHandler;

    // Use this for initialization
    void Start()
    {
        _soundHandler = GameInitializer.GetSoundHandler();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void HandleTurn(City pCity)
    {
        if (_soundHandler == null)
        {
            _soundHandler = GameInitializer.GetSoundHandler();
        }
        City targetCity = pCity;
        if (currentMode == CurrentMode.MISSILEAIM && _isFocusedOnOwnCity)
        {
            targetCity = GameInitializer.GetNextCity(targetCity);
            GameInitializer.GetCameraManager().MoveCameraTo(targetCity.transform.position + Glob.CameraOffset, Glob.CameraCitySwitchTime / 2);
            _isFocusedOnOwnCity = false;
            UIHandler.ShowNotification("BOMBS AWAY!"); //TODO: Placeholder text
        }

        if (Input.anyKeyDown)
        {
            if (!_isFocusedOnOwnCity)
            {
                targetCity = GameInitializer.GetNextCity(targetCity);
            }
            if (Input.GetKeyDown(KeyCode.T) && currentMode == CurrentMode.SELECTINGTILE)
            {
                if (_isFocusedOnOwnCity)
                {
                    targetCity = GameInitializer.GetNextCity(pCity);
                    GameInitializer.GetCameraManager().MoveCameraTo(targetCity.transform.position + Glob.CameraOffset, Glob.CameraCitySwitchTime/2);
                    _isFocusedOnOwnCity = false;
                } else
                {
                    targetCity = pCity;
                    GameInitializer.GetCameraManager().MoveCameraTo(targetCity.transform.position + Glob.CameraOffset, Glob.CameraCitySwitchTime/2);
                    _isFocusedOnOwnCity = true;
                }
            }
            if (currentMode == CurrentMode.SELECTINGTILE || currentMode == CurrentMode.MISSILEAIM)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    targetCity.ChangeSelectedTile(DirectionKey.RIGHT);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    targetCity.ChangeSelectedTile(DirectionKey.LEFT);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    targetCity.ChangeSelectedTile(DirectionKey.UP);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    targetCity.ChangeSelectedTile(DirectionKey.DOWN);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (targetCity.GetSelectedTile().GetBuildingOnTile() == null)
                    {
                        if (_isFocusedOnOwnCity)
                        {
                            _soundHandler.PlaySound(SoundHandler.Sounds.CONFIRM);
                            SetCurrentMode(CurrentMode.BUILDINGTILE);
                            targetCity.GetSelectedTile().Reset();
                            GameInitializer.GetUIHandler().ToggleBuildPanel(true);
                        }
                    }
                    else if (currentMode == CurrentMode.MISSILEAIM)
                    {
                        _soundHandler.PlaySound(SoundHandler.Sounds.MISSILEHIT);
                        Destroy(targetCity.GetSelectedTile().GetBuildingOnTile().gameObject);
                        targetCity.GetSelectedTile().SetBuilding(null);
                        SetCurrentMode(CurrentMode.SELECTINGTILE);
                        _isFocusedOnOwnCity = true;
                        targetCity = pCity;
                        GameInitializer.GetCameraManager().MoveCameraTo(targetCity.transform.position + Glob.CameraOffset, Glob.CameraCitySwitchTime / 2);
                        pCity.AddMissileLaunched();
                    }
                    else
                    {
                        SetCurrentMode(CurrentMode.EXAMINEMODE);
                        GameInitializer.GetUIHandler().SetExamineMode(targetCity.GetSelectedTile().GetBuildingOnTile());
                        targetCity.GetSelectedTile().Reset();

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
                        _soundHandler.PlaySound(SoundHandler.Sounds.MOVE);
                        GameInitializer.GetBuildingHandler().ChangeBuildingSelection(1);
                        GameInitializer.GetUIHandler().SetActiveBuildingImage(1);
                    }
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        _soundHandler.PlaySound(SoundHandler.Sounds.MOVE);
                        GameInitializer.GetBuildingHandler().ChangeBuildingSelection(-1);
                        GameInitializer.GetUIHandler().SetActiveBuildingImage(-1);
                    }

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (GameInitializer.GetBuildingHandler().StartBuilding())
                        {
                            if (currentMode == CurrentMode.BUILDINGTILE)//If nothing changed the current mode
                            {
                                SetCurrentMode(CurrentMode.SELECTINGTILE);
                            }
                            GameInitializer.GetUIHandler().ToggleBuildPanel(false);
                        }
                        else
                        {
                            _soundHandler.PlaySound(SoundHandler.Sounds.ERROR);
                        }

                    }

                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        GameInitializer.GetBuildingHandler().DestroyPlacementBuilding();
                        SetCurrentMode(CurrentMode.SELECTINGTILE);
                        GameInitializer.GetUIHandler().ToggleBuildPanel(false);
                    }
                }
                else
                {
                    GameInitializer.GetBuildingHandler().ChangeBuildingSelection(0, false);
                    GameInitializer.GetUIHandler().SetActiveBuildingImage(0, false);
                }
            }
            if (currentMode == CurrentMode.EXAMINEMODE)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    GameInitializer.GetUIHandler().ToggleExaminePanel(false);
                    SetCurrentMode(CurrentMode.SELECTINGTILE);
                }
            }
            if (Input.GetKeyDown(KeyCode.Return) && currentMode != CurrentMode.MISSILEAIM)
            {
                GameInitializer.EndTurn();
                //Debug.Log(GameInitializer.GetBuildingHandler().GetCurrentCity());
                UIHandler.ShowNotification("Turn has ended");
            }
        }
    }
}
