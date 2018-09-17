using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {

    private City _playerCity;
    private static City[] _allCities;
    private static int _currentCity = 0;
    private static BuildingHandler _buildHandler;
    private static UIHandler _gameUIHandler;
    private static CameraManager _cameraManager;
    private static SoundHandler _soundHandler;

    private static bool _hardModeEnabled = false;

    private static bool _isPaused = false;

    public static void ResetGame()
    {
        Application.LoadLevel(0);
        
    }

	// Use this for initialization
	void Start () {
        _cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
        _buildHandler = new GameObject("BuildingHandler").AddComponent<BuildingHandler>();
        _gameUIHandler = Instantiate((Resources.Load(Glob.UIPrefab) as GameObject).GetComponent<UIHandler>());
        _soundHandler = new GameObject("SoundHandler").AddComponent<SoundHandler>();
        _allCities = new City[Glob.AmountOfAICities + 1];
        _playerCity = new GameObject("PlayerCity").AddComponent<City>().Initialize(new PlayerCityManager(), Glob.CityWidth, Glob.CityLength, Glob.TileSpacing, new Vector3(0, 0, 0));
        _allCities[0] = _playerCity;
        for (int i = 1; i < Glob.AmountOfAICities+1; i++)
        {
            if (_hardModeEnabled)
            {
                _allCities[i] = new GameObject("AICity" + i).AddComponent<City>().Initialize(new AICityManager(Glob.HardAIDifficulty), Glob.CityWidth, Glob.CityLength, Glob.TileSpacing, new Vector3(Glob.CitySpacing * i, 0, 0));
            }
            else
            {
                _allCities[i] = new GameObject("AICity" + i).AddComponent<City>().Initialize(new AICityManager(Glob.EasyAIDifficulty), Glob.CityWidth, Glob.CityLength, Glob.TileSpacing, new Vector3(Glob.CitySpacing * i, 0, 0));
            }
        }
        _buildHandler.SetCurrentCity(_allCities[0]);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetGame();
        }
    }

    public static City GetCurrentCity()
    {
        return _allCities[_currentCity];
    }
    public static City GetNextCity(City pCity)
    {
        for (int i = 0; i < _allCities.Length; i++)
        {
            if (_allCities[i] == pCity)
            {
                if (i+1 >= _allCities.Length)
                {
                    return _allCities[0];
                }
                return _allCities[i+1];
            }
        }
        return null;
    }

    public static SoundHandler GetSoundHandler()
    {
        return _soundHandler;
    }

    public static void EndTurn()
    {
        _soundHandler.PlaySound(SoundHandler.Sounds.ENDTURN);
        _currentCity++;
        if (_currentCity >= _allCities.Length)
        {
            _currentCity = 0;
            
        }
        _buildHandler.SetCurrentCity(_allCities[_currentCity]);
        _gameUIHandler.ToggleBuildPanel(false);
        _gameUIHandler.ToggleExaminePanel(false);
    }

    public static void EndGame(bool pBothWin = false, City pWinner = null)
    {
        _isPaused = true;
        if (pWinner == null && !pBothWin)
        {
            pWinner = calculateWinner();
        }

        if (pBothWin)
        {
            _soundHandler.PlaySound(SoundHandler.Sounds.WIN);
            UIHandler.ShowNotification("Through the combined effort on building the bridge, both cities win!");
            return;
        }
        else if (pWinner == _allCities[0])
        {
            _soundHandler.PlaySound(SoundHandler.Sounds.WIN);
        }
        else if (pWinner == _allCities[1])
        {
            _soundHandler.PlaySound(SoundHandler.Sounds.LOSE);
        }

        UIHandler.ShowNotification("The winner is: " + pWinner.gameObject.name + ", with a score of " + pWinner.GetScore() + "!"); //TODO: Correct win message.
        Debug.Log("The winner is: " + pWinner.gameObject.name + ", with a score of " + pWinner.GetScore() + "!");
    }

    private static City calculateWinner()
    {
        City winner = null;
        float winnerScore = 0;
        for (int i = 0; i < _allCities.Length; i++)
        {
            if (_allCities[i].GetScore() > winnerScore)
            {
                winner = _allCities[i];
                winnerScore = _allCities[i].GetScore();
            }
        }
        return winner;
    }

    public static UIHandler GetUIHandler()
    {
        return _gameUIHandler;
    }
    public static BuildingHandler GetBuildingHandler()
    {
        return _buildHandler;
    }
    public static CameraManager GetCameraManager()
    {
        return _cameraManager;
    }

    public static void SetPaused(bool pPaused)
    {
        _isPaused = pPaused;
    }
    public static bool GetPaused()
    {
        return _isPaused;
    }
}
