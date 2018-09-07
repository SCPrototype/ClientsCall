using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {

    private City playerCity;
    private City AICity;
    private BuildingHandler buildHandler;
    private InputHandler playerInputHandler;
    private UIHandler gameUIHandler;

	// Use this for initialization
	void Start () {
        gameUIHandler = Instantiate((Resources.Load(Glob.uiPrefab) as GameObject).GetComponent<UIHandler>());
        playerCity = new GameObject("PlayerCity").AddComponent<City>().Initialize(7, 7, 0.2f, new Vector3(0, 0, 0));
        AICity = new GameObject("AICity").AddComponent<City>().Initialize(7, 7, 0.2f, new Vector3(50, 0, 0));
        buildHandler = new GameObject("BuildingHandler").AddComponent<BuildingHandler>();
        buildHandler.SetCurrentCity(playerCity);
        playerInputHandler = new GameObject("InputHandler").AddComponent<InputHandler>().Initialize(playerCity, buildHandler, gameUIHandler);
    }
	
	// Update is called once per frame
	void Update () {

	}
}
