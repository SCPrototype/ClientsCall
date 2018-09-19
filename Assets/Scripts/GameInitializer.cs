using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {

    private static Glob.PlayerTypes _playerType = Glob.PlayerTypes.Achiever;
    private static int _achieverScore = 0;
    private static int _explorerScore = 0;
    private static int _killerScore = 0;
    private static int _socializerScore = 0;

    private City _playerCity;
    private static City[] _allCities;
    private static int _currentCity = 0;
    private static BuildingHandler _buildHandler;
    private static UIHandler _gameUIHandler;
    private static CameraManager _cameraManager;
    private static SoundHandler _soundHandler;

    public static bool HardModeEnabled = false;

    private static bool _isPaused = false;

    private float _lastAction = 0;
    private float _resetButtonPressTime = 0;

    public static void ResetGame()
    {
        FadeToBlack.DoFade(1, true, 0);
    }

	// Use this for initialization
	void Start () {
        _lastAction = Time.time;

        _achieverScore = 0;
        _explorerScore = 0;
        _killerScore = 0;
        _socializerScore = 0;

        _cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
        _buildHandler = new GameObject("BuildingHandler").AddComponent<BuildingHandler>();
        _gameUIHandler = Instantiate((Resources.Load(Glob.UIPrefab) as GameObject).GetComponent<UIHandler>());
        _soundHandler = new GameObject("SoundHandler").AddComponent<SoundHandler>();
        _allCities = new City[Glob.AmountOfAICities + 1];
        _playerCity = new GameObject("PlayerCity").AddComponent<City>().Initialize(new PlayerCityManager(), Glob.CityWidth, Glob.CityLength, Glob.TileSpacing, new Vector3(0, 0, 0));
        _allCities[0] = _playerCity;
        for (int i = 1; i < Glob.AmountOfAICities+1; i++)
        {
            if (HardModeEnabled)
            {
                _allCities[i] = new GameObject("AICity" + i).AddComponent<City>().Initialize(new AICityManager(Glob.HardAIDifficulty), Glob.CityWidth, Glob.CityLength, Glob.TileSpacing, new Vector3(Glob.CitySpacing * i, 0, 0));
            }
            else
            {
                _allCities[i] = new GameObject("AICity" + i).AddComponent<City>().Initialize(new AICityManager(Glob.EasyAIDifficulty), Glob.CityWidth, Glob.CityLength, Glob.TileSpacing, new Vector3(Glob.CitySpacing * i, 0, 0));
            }
        }
        _buildHandler.SetCurrentCity(_allCities[0]);

        Tutorial _tut = new GameObject("Tutorial").AddComponent<Tutorial>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(Glob.ExamineButton))
        {
            _resetButtonPressTime = Time.time;
        }
        if (Time.time - _lastAction >= Glob.GameTimeOut || (Time.time - _resetButtonPressTime >= Glob.ResetButtonTime && Input.GetKey(Glob.ExamineButton)))
        {
            _resetButtonPressTime = Time.time;
            UpdateActionTime();
            ResetGame();
        }
        else if (Input.anyKeyDown)
        {
            UpdateActionTime();
        }
    }

    private void UpdateActionTime()
    {
        _lastAction = Time.time;
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
        _buildHandler.DestroyPlacementBuilding();
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
        _gameUIHandler.EnableResolutionScreen(_playerType);
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

    public static void AddAchieverScore(int pScore)
    {
        _achieverScore += pScore;
        calculatePlayerType();
    }
    public static void AddExplorerScore(int pScore)
    {
        _explorerScore += pScore;
        calculatePlayerType();
    }
    public static void AddKillerScore(int pScore)
    {
        _killerScore += pScore;
        calculatePlayerType();
    }
    public static void AddSocializerScore(int pScore)
    {
        _socializerScore += pScore;
        calculatePlayerType();
    }
    public static int GetAchieverScore()
    {
        return _achieverScore;
    }
    public static int GetExplorerScore()
    {
        return _explorerScore;
    }
    public static int GetKillerScore()
    {
        return _killerScore;
    }
    public static int GetSocializerScore()
    {
        return _socializerScore;
    }

    private static void calculatePlayerType()
    {
        Debug.Log(_achieverScore + " _ " + _explorerScore + " _ " + _killerScore + " _ " + _socializerScore);
        if (_achieverScore > _explorerScore && _achieverScore > _killerScore && _achieverScore > _socializerScore)
        {
            _playerType = Glob.PlayerTypes.Achiever;
        }
        else if (_explorerScore > _achieverScore && _explorerScore > _killerScore && _explorerScore > _socializerScore)
        {
            _playerType = Glob.PlayerTypes.Explorer;
        }
        else if (_killerScore > _achieverScore && _killerScore > _explorerScore && _killerScore > _socializerScore)
        {
            _playerType = Glob.PlayerTypes.Killer;
        }
        else if (_socializerScore > _achieverScore && _socializerScore > _explorerScore && _socializerScore > _killerScore)
        {
            _playerType = Glob.PlayerTypes.Socializer;
        }
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
