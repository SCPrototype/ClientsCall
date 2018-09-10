using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICityManager : CityManager {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void HandleTurn(City pCity)
    {
        //TODO: Add calculations for optimal move here
        //Move 1 tile to the right
        pCity.ChangeSelectedTile(DirectionKey.RIGHT);

        //Select the tile
        pCity.GetSelectedTile().Reset();

        GameInitializer.GetBuildingHandler().ChangeBuildingSelection(1);

        GameInitializer.GetBuildingHandler().StartBuilding();

        GameInitializer.EndTurn();
    }
}
