﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartBuilding(Building pBuilding)
    {
        pBuilding.SetBuildingPhase(Building.BuildingPhase.INPROGRESS);
    }

    public Building PlaceBuilding(CustomTile pCustomTile, Building pBuilding)
    {
        Building buildingToPlace = Instantiate(pBuilding);
        Vector3 positionBuilding = pCustomTile.transform.position;
        //Puts it on the right height.
        positionBuilding.y = buildingToPlace.transform.localScale.y / 2;
        buildingToPlace.transform.position = positionBuilding;
        buildingToPlace.transform.parent = pCustomTile.transform;

        //Makes a building which is into placement mode.
        buildingToPlace.SetBuildingPhase(Building.BuildingPhase.PLACEMENT);
        return buildingToPlace;
    }
}
