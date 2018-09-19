using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCityManager : CityManager
{

    private bool _isFocusedOnOwnCity = true;
    private float _cancelKeyPressed = 0;
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
            GameInitializer.GetCameraManager().MoveCameraTo(targetCity.transform.position + Glob.CameraCityOffset, Glob.CameraCitySwitchTime / 2, 4);
            _isFocusedOnOwnCity = false;
            UIHandler.ShowNotification("BOMBS AWAY!"); //TODO: Placeholder text
        }

        if (Input.anyKeyDown)
        {
            if (!_isFocusedOnOwnCity)
            {
                targetCity = GameInitializer.GetNextCity(targetCity);
            }
            if (Input.GetKeyDown(Glob.ExamineButton) && currentMode == CurrentMode.SELECTINGTILE)
            {
                if (_isFocusedOnOwnCity)
                {
                    targetCity = GameInitializer.GetNextCity(pCity);
                    GameInitializer.GetCameraManager().MoveCameraTo(targetCity.transform.position + Glob.CameraCityOffset, Glob.CameraCitySwitchTime / 2);
                    _isFocusedOnOwnCity = false;
                }
                else
                {
                    targetCity = pCity;
                    GameInitializer.GetCameraManager().MoveCameraTo(targetCity.transform.position + Glob.CameraCityOffset, Glob.CameraCitySwitchTime / 2);
                    _isFocusedOnOwnCity = true;
                }
            }
            if (currentMode == CurrentMode.SELECTINGTILE || currentMode == CurrentMode.MISSILEAIM)
            {
                if (Input.GetKeyDown(Glob.RightButton))
                {
                    targetCity.ChangeSelectedTile(DirectionKey.RIGHT);
                }
                if (Input.GetKeyDown(Glob.LeftButton))
                {
                    targetCity.ChangeSelectedTile(DirectionKey.LEFT);
                }
                if (Input.GetKeyDown(Glob.UpButton))
                {
                    targetCity.ChangeSelectedTile(DirectionKey.UP);
                }
                if (Input.GetKeyDown(Glob.DownButton))
                {
                    targetCity.ChangeSelectedTile(DirectionKey.DOWN);
                }
                if (Input.GetKeyDown(Glob.ConfirmButton))
                {
                    if (targetCity.GetSelectedTile().GetBuildingOnTile() == null)
                    {
                        if (_isFocusedOnOwnCity)
                        {
                            //_soundHandler.PlaySound(SoundHandler.Sounds.CONFIRM);
                            SetCurrentMode(CurrentMode.BUILDINGTILE);
                            targetCity.GetSelectedTile().Reset();
                            GameInitializer.GetUIHandler().ToggleBuildPanel(true);
                        }
                    }
                    else if (currentMode == CurrentMode.MISSILEAIM)
                    {
                        Debug.Log(GameInitializer.GetCameraManager().isAnimationDone());
                        if (GameInitializer.GetCameraManager().isAnimationDone())
                        {
                            Missile missile = Instantiate(Glob.GetMissile());
                            missile.SetMissileTile(targetCity.GetSelectedTile());
                            Destroy(targetCity.GetSelectedTile().GetBuildingOnTile().gameObject);
                            targetCity.GetSelectedTile().SetBuilding(null);
                            SetCurrentMode(CurrentMode.SELECTINGTILE);
                            _isFocusedOnOwnCity = true;
                            targetCity = pCity;
                            //AddMissile with sound.
                            GameInitializer.GetCameraManager().MoveCameraTo(targetCity.transform.position + Glob.CameraCityOffset, Glob.CameraCitySwitchTime / 2, 4);
                            pCity.AddMissileLaunched();
                        }
                        else
                        { Debug.Log("nawh"); }

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
                    if (Input.GetKeyDown(Glob.DownButton) || Input.GetKeyDown(Glob.RightButton))
                    {
                        //BuildingHandler should probably tell UIHandler what to do.
                        _soundHandler.PlaySound(SoundHandler.Sounds.MOVE);
                        GameInitializer.GetBuildingHandler().ChangeBuildingSelection(1);
                        GameInitializer.GetUIHandler().SetActiveBuildingImage(1);
                    }
                    if (Input.GetKeyDown(Glob.UpButton) || Input.GetKeyDown(Glob.LeftButton))
                    {
                        _soundHandler.PlaySound(SoundHandler.Sounds.MOVE);
                        GameInitializer.GetBuildingHandler().ChangeBuildingSelection(-1);
                        GameInitializer.GetUIHandler().SetActiveBuildingImage(-1);
                    }

                    if (Input.GetKeyDown(Glob.ConfirmButton))
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

                    if (Input.GetKeyDown(Glob.CancelButton))
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
                if (Input.GetKeyDown(Glob.CancelButton))
                {
                    GameInitializer.GetUIHandler().ToggleExaminePanel(false);
                    SetCurrentMode(CurrentMode.SELECTINGTILE);
                }
            }
            if (Input.GetKeyDown(Glob.CancelButton) && currentMode != CurrentMode.MISSILEAIM)
            {
                _cancelKeyPressed = Time.time;
            }
        }
        if (Time.time - _cancelKeyPressed >= Glob.EndTurnButtonTime && Input.GetKey(Glob.CancelButton))
        {
            GameInitializer.EndTurn();
            //Debug.Log(GameInitializer.GetBuildingHandler().GetCurrentCity());
            UIHandler.ShowNotification("Turn has ended");
        }
    }
}
