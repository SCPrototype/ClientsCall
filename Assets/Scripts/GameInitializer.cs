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

    private static bool _isPaused = false;

	// Use this for initialization
	void Start () {
        _cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
        _buildHandler = new GameObject("BuildingHandler").AddComponent<BuildingHandler>();
        _gameUIHandler = Instantiate((Resources.Load(Glob.uiPrefab) as GameObject).GetComponent<UIHandler>());
        _allCities = new City[Glob.AmountOfAICities + 1];
        _playerCity = new GameObject("PlayerCity").AddComponent<City>().Initialize(new PlayerCityManager(), Glob.CityWidth, Glob.CityLength, Glob.TileSpacing, new Vector3(0, 0, 0));
        _allCities[0] = _playerCity;
        for (int i = 1; i < Glob.AmountOfAICities+1; i++)
        {
            _allCities[i] = new GameObject("AICity" + i).AddComponent<City>().Initialize(new AICityManager(Random.Range(0, 100)), Glob.CityWidth, Glob.CityLength, Glob.TileSpacing, new Vector3(Glob.CitySpacing * i, 0, 0));
        }
        _buildHandler.SetCurrentCity(_allCities[0]);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public static City GetCurrentCity()
    {
        return _allCities[_currentCity];
    }

    public static void EndTurn()
    {
        _currentCity++;
        if (_currentCity >= _allCities.Length)
        {
            _currentCity = 0;
            
        }
        _buildHandler.SetCurrentCity(_allCities[_currentCity]);
    }

    public static void EndGame(City pWinner = null)
    {
        _isPaused = true;
        if (pWinner == null)
        {
            pWinner = calculateWinner();
        }

        UIHandler.ShowNotification("The winner is: " + pWinner.gameObject.name + ", with a score of " + pWinner.GetScore() + "!");
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
