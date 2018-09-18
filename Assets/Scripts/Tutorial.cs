using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    private const string _welcome = "Hey, my name is Atlantropa. I'm here to help you get the hang of it. \nWelcome to your city.Right now it’s rather… what's the word for it? Quaint. But soon it could be a powerful beast. Let's get you started. Press the first button if you're ready to continue.";
    private const string _tileSelect = "With that joystick in front of you, you can move around. Then when you found your perfect patch of green, you can select it by pressing the first button.";
    private bool _tileSelected = false;
    private int _tileSelectedX = 3;
    private int _tileSelectedY = 3;
    private const string _buildFactory = "Now by pressing it again, you build a factory. Factories give “your” people work and yourself a nice paycheck. Speaking of those.";
    private bool _factoryBuilt = false;
    private const string _buildHouse = "Select the tile next to the factory and press the first button again, now select the second icon in the list and build it.";
    private bool _houseBuilt = false;
    private int _houseBuiltX = 3;
    private int _houseBuiltY = 4;
    private const string _explainHouse = "That is a house, a house houses people, people who can work in factories and without them, your factories won't work. People also do one other thing, complain, unendingly complain. But you do have a tool to mitigate that somewhat.";
    private const string _explainHappiness = "Did you notice that when you placed your factory the ground around it turned an ugly brown colour? This displays the happiness of the people in that area. What people, you ask? I honestly am not sure, but that house tile has the brown grass as well. So let's fix that.";
    private const string _buildPark = "Open the build menu back up, you can see the third item has opened up. Build that park somewhere near the house.";
    private bool _parkBuilt = false;
    private int _parkBuiltX = 4;
    private int _parkBuiltY = 4;
    private const string _explainHappiness2 = "Notice that the grass around the park has turned back to a healthy green. Look at that happy little house. You will unlock certain actions when you have enough happy houses.";
    private const string _endTurn = "That's enough micromanaging for now, let's get the full picture. We are out of money anyway. Hold the second button to end your turn.";
    private bool _turnEnded = false;
    private const string _enemy = "That is AIton, the bastards settled just on the other side of the river, and as they grow, they take what we potentially need to surpass them.";
    private const string _startOfTurn = "Now at the start of the round, you get the money from the factories, and we can use that to buy the tools to end this rivalry. Let's open the build menu again and see what we got.";
    private const string _digsite = "First up, the dig-sites. After you build one, they will look for hidden treasure deep underground. And sometimes they find something worthy enough to put in a museum. Now, this might sound a bit crazy. But what if we filled our museum to the brim with treasure? It will be great, we can explore the entire region in peace.";
    private const string _wonder = "Not quite your style? How about we build a monument, the greatest achievement of mankind? Certainly, ours will be Greater.";
    private const string _bridges = "Then there were bridges, this is not my kind of thing. But there are three places to build these if you complete that project. I'm sure that both cities will benefit. However, AIton won't want to merge with a city that treats its people, or it neighbours like garbage.";
    private const string _missiles = "Now, this is my yam. 20 tons of pure excitement. Build 3 of these, and I'm sure no-one would ever dare to settle near us again. The valley and all its resources are ours. The people will rejoice. Let's build them right… now… Well, it seems that these are a bit too expensive for us right now we need to expand our city first.";
    private const string _goodluck = "Right, you seem to have everything under control. I’ll hand the reigns over to you. Good luck, and may your city prosper.";
    private const string _exitButton = "Yeah, before I forget. If you want to exit the build menu just press the second button and if you want to reset, just hold it down. If you ever need me again, you can ring me up with that third button.";
    private const string _bye = "Atlantropa out.";

    private string[] _allText = new string[18];
    private int _currentText = 0;

    private City _myCity;

    private bool _tutorialActive = true;

    // Use this for initialization
    void Start()
    {
        _myCity = GameInitializer.GetCurrentCity();

        _allText[0] = _welcome;
        _allText[1] = _tileSelect;
        _allText[2] = _buildFactory;
        _allText[3] = _buildHouse;
        _allText[4] = _explainHouse;
        _allText[5] = _explainHappiness;
        _allText[6] = _buildPark;
        _allText[7] = _explainHappiness2;
        _allText[8] = _endTurn;
        _allText[9] = _enemy;
        _allText[10] = _startOfTurn;
        _allText[11] = _digsite;
        _allText[12] = _wonder;
        _allText[13] = _bridges;
        _allText[14] = _missiles;
        _allText[15] = _goodluck;
        _allText[16] = _exitButton;
        _allText[17] = _bye;

        GameInitializer.SetPaused(true);
        UIHandler.ShowNotification(_allText[_currentText]);
    }

    // Update is called once per frame
    void Update()
    {
        if (_tutorialActive)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_currentText == 1 && !_tileSelected || _currentText == 3 && !_tileSelected || _currentText == 6 && !_tileSelected || _currentText == 8 && !_turnEnded) //If the player needs to select the correct tile first.
                {
                    return;
                }
                else if (_currentText == 3 && _tileSelected && !_houseBuilt)
                {
                    GameInitializer.GetUIHandler().ToggleBuildPanel(true);
                    GameInitializer.GetBuildingHandler().ChangeBuildingSelection(0, false);
                    GameInitializer.GetUIHandler().SetActiveBuildingImage(0, false);
                    GameInitializer.GetBuildingHandler().PlaceBuilding(_myCity.GetSelectedTile());
                    _houseBuilt = true;
                    return;
                }
                else if (_currentText == 6 && _tileSelected && !_parkBuilt)
                {
                    GameInitializer.GetUIHandler().ToggleBuildPanel(true);
                    GameInitializer.GetBuildingHandler().ChangeBuildingSelection(2, false);
                    GameInitializer.GetUIHandler().SetActiveBuildingImage(2, false);
                    GameInitializer.GetBuildingHandler().PlaceBuilding(_myCity.GetSelectedTile());
                    _parkBuilt = true;
                    return;
                }

                _tileSelected = false;


                _currentText++;


                if (_currentText == 1)
                {
                    _myCity.HighlightTile(_tileSelectedX, _tileSelectedY, true); //TODO: Remove highlight after action.
                }
                else if (_currentText == 2)
                {
                    _myCity.HighlightTile(_tileSelectedX, _tileSelectedY, false);
                    GameInitializer.GetUIHandler().ToggleBuildPanel(true);
                    GameInitializer.GetBuildingHandler().ChangeBuildingSelection(1, false);
                    GameInitializer.GetUIHandler().SetActiveBuildingImage(1, false);
                    GameInitializer.GetBuildingHandler().PlaceBuilding(_myCity.GetSelectedTile());
                }
                else if (_currentText == 3)
                {
                    GameInitializer.GetBuildingHandler().StartBuilding();
                    GameInitializer.GetUIHandler().ToggleBuildPanel(false);

                    _myCity.HighlightTile(_houseBuiltX, _houseBuiltY, true);
                }
                else if (_currentText == 4)
                {
                    _myCity.HighlightTile(_houseBuiltX, _houseBuiltY, false);
                    GameInitializer.GetBuildingHandler().StartBuilding();
                    GameInitializer.GetUIHandler().ToggleBuildPanel(false);
                }
                else if (_currentText == 6)
                {
                    _myCity.HighlightTile(_parkBuiltX, _parkBuiltY, true);
                }
                else if (_currentText == 7)
                {
                    _myCity.HighlightTile(_parkBuiltX, _parkBuiltY, false);
                    GameInitializer.GetBuildingHandler().StartBuilding();
                    GameInitializer.GetUIHandler().ToggleBuildPanel(false);

                }
                else if (_currentText == 10)
                {
                    _myCity.CollectFromAllBuildings();
                    _myCity.ChangeSelectedTile(CityManager.DirectionKey.DOWN);

                    GameInitializer.GetUIHandler().SetResourcesBars((int)_myCity.GetBudget());

                    GameInitializer.GetCameraManager().MoveCameraTo(_myCity.transform.position + Glob.CameraOffset, Glob.CameraCitySwitchTime);
                }
                else if (_currentText == 11)
                {
                    GameInitializer.GetUIHandler().ToggleBuildPanel(true);
                    GameInitializer.GetBuildingHandler().ChangeBuildingSelection(3, false);
                    GameInitializer.GetUIHandler().SetActiveBuildingImage(3, false);
                    GameInitializer.GetBuildingHandler().PlaceBuilding(_myCity.GetSelectedTile());
                }
                else if (_currentText == 12)
                {
                    GameInitializer.GetBuildingHandler().ChangeBuildingSelection(5, false);
                    GameInitializer.GetUIHandler().SetActiveBuildingImage(5, false);
                    GameInitializer.GetBuildingHandler().PlaceBuilding(_myCity.GetSelectedTile());
                }
                else if (_currentText == 13)
                {
                    GameInitializer.GetBuildingHandler().ChangeBuildingSelection(6, false);
                    GameInitializer.GetUIHandler().SetActiveBuildingImage(6, false);
                    GameInitializer.GetBuildingHandler().PlaceBuilding(_myCity.GetSelectedTile());
                }
                else if (_currentText == 14)
                {
                    GameInitializer.GetBuildingHandler().ChangeBuildingSelection(4, false);
                    GameInitializer.GetUIHandler().SetActiveBuildingImage(4, false);
                    GameInitializer.GetBuildingHandler().PlaceBuilding(_myCity.GetSelectedTile());
                }
                else if (_currentText == 17)
                {
                    GameInitializer.GetBuildingHandler().DestroyPlacementBuilding();
                    GameInitializer.GetUIHandler().ToggleBuildPanel(false);
                    //GameInitializer.SetPaused(false);
                    //_tutorialActive = false;
                }
                else if (_currentText >= 18)
                {
                    //GameInitializer.GetBuildingHandler().DestroyPlacementBuilding();
                    //GameInitializer.GetUIHandler().ToggleBuildPanel(false);
                    GameInitializer.SetPaused(false);
                    _tutorialActive = false;
                    return;
                }

                UIHandler.ShowNotification(_allText[_currentText]);//Display the new information.
            }
            if (_currentText == 1 || _currentText == 3 && !_houseBuilt || _currentText == 6 && !_parkBuilt || _currentText == 8 && !_turnEnded)
            {
                handleInput();
            }
        }
    }

    private void handleInput()
    {
        if (_currentText != 8)
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
        }
        int[] currentTile = _myCity.GetTilePosition(_myCity.GetSelectedTile());
        if (_currentText == 1)
        {
            if (currentTile[0] == _tileSelectedX && currentTile[1] == _tileSelectedY)
            {
                _tileSelected = true;
            }
            else
            {
                _tileSelected = false;
            }
        }
        else if (_currentText == 3)
        {
            if (currentTile[0] == _houseBuiltX && currentTile[1] == _houseBuiltY)
            {
                _tileSelected = true;
            }
            else
            {
                _tileSelected = false;
            }
        }
        else if (_currentText == 6)
        {
            if (currentTile[0] == _parkBuiltX && currentTile[1] == _parkBuiltY)
            {
                _tileSelected = true;
            }
            else
            {
                _tileSelected = false;
            }
        }
        else if (_currentText == 8)
        {
            if (Input.GetKeyDown(KeyCode.Return))//TODO: Correct button
            {
                GameInitializer.GetCameraManager().MoveCameraTo(GameInitializer.GetNextCity(_myCity).transform.position + Glob.CameraOffset, Glob.CameraCitySwitchTime);
                _turnEnded = true;
                _currentText++;
                UIHandler.ShowNotification(_allText[_currentText]);
            }
        }
    }

    /*TODO:
     Welcome the player.
     Explain joystick movement.
     Build a factory on specific spot.
     Build a house next to the factory, on a specific spot.
     Build a park next to the house, on a specific spot.
     Hold button to end turn.
     Explain enemy.
     Show start of turn money gain.
     Explain dig-sites.
     Explain Wonder/Monument.
     Explain bridges.
     Explain missiles.
     Wish them luck, and explain about the cancel button to exit the build menu.
    */
}
