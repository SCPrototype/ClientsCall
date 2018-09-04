using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour {

    public Building HousePrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaceBuilding(CustomTile pCustomTile, Building pBuilding)
    {
        Building buildingToPlace = Instantiate(HousePrefab);
        Vector3 positionBuilding = pCustomTile.transform.position;
        //Puts it on the right height.
        positionBuilding.y = buildingToPlace.transform.localScale.y / 2;
        buildingToPlace.transform.position = positionBuilding;
    }
}
