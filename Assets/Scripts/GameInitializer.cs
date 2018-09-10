using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {

    private City playerCity;
    private static City[] allCities;
    private static int currentCity = 0;
    private static BuildingHandler buildHandler;
    private static UIHandler gameUIHandler;

	// Use this for initialization
	void Start () {
        gameUIHandler = Instantiate((Resources.Load(Glob.uiPrefab) as GameObject).GetComponent<UIHandler>());
        allCities = new City[Glob.AmountOfAICities + 1];
        playerCity = new GameObject("PlayerCity").AddComponent<City>().Initialize(new PlayerCityManager(), Glob.CityWidth, Glob.CityLength, Glob.TileSpacing, new Vector3(0, 0, 0));
        allCities[0] = playerCity;
        for (int i = 1; i < Glob.AmountOfAICities+1; i++)
        {
            allCities[i] = new GameObject("AICity").AddComponent<City>().Initialize(new AICityManager(), Glob.CityWidth, Glob.CityLength, Glob.TileSpacing, new Vector3(Glob.CitySpacing * i, 0, 0));
        }

        buildHandler = new GameObject("BuildingHandler").AddComponent<BuildingHandler>();
        buildHandler.SetCurrentCity(allCities[0]);
        //playerInputHandler = new GameObject("InputHandler").AddComponent<InputHandler>().Initialize(playerCity, buildHandler, gameUIHandler);
        //AIInputHandler = new GameObject("AIInputHandler").AddComponent<AIInputHandler>().Initialize(buildHandler);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public static City GetCurrentCity()
    {
        return allCities[currentCity];
    }

    public static void EndTurn()
    {
        currentCity++;
        if (currentCity >= allCities.Length)
        {
            currentCity = 0;
        }
        buildHandler.SetCurrentCity(allCities[currentCity]);
    }

    public static UIHandler GetUIHandler()
    {
        return gameUIHandler;
    }
    public static BuildingHandler GetBuildingHandler()
    {
        return buildHandler;
    }
}
