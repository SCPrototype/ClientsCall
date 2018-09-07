using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour {

    private Building[] buildings;
    private int currentBuildingSelection;
    private Building placementBuilding;

    // Use this for initialization
    void Start () {
        buildings = Glob.GetBuildingPrefabs();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartBuilding()
    {
        placementBuilding.SetBuildingPhase(Building.BuildingPhase.INPROGRESS);
        City.GetSelectedTile().SetBuilding(placementBuilding);
        DestroyPlacementBuilding();
    }

    public Building PlaceBuilding(CustomTile pCustomTile)
    {
        Building buildingToPlace = Instantiate(buildings[currentBuildingSelection]);
        Vector3 positionBuilding = pCustomTile.transform.position;

        positionBuilding.y = buildingToPlace.transform.localScale.y / 2;
        buildingToPlace.transform.position = positionBuilding;
        buildingToPlace.transform.parent = pCustomTile.transform;

        //Makes a building which is into placement mode.
        buildingToPlace.SetBuildingPhase(Building.BuildingPhase.PLACEMENT);
        return buildingToPlace;
    }

    public void ChangeBuildingSelection(int index, bool addToCurrent = true)
    {
        DestroyPlacementBuilding();
        if (addToCurrent)
        {
            for (int i = 0; i != index; i += (index / Mathf.Abs(index)))
            {
                currentBuildingSelection += (index / Mathf.Abs(index));
                if (currentBuildingSelection >= Glob.buildingCount)
                {
                    currentBuildingSelection = 0;
                }
                else if (currentBuildingSelection < 0)
                {
                    currentBuildingSelection = Glob.buildingCount - 1;
                }
            }
        }
        else
        {
            currentBuildingSelection = index;
        }
        placementBuilding = PlaceBuilding(City.GetSelectedTile());
    }

    public void DestroyPlacementBuilding()
    {
        Building[] buildings = FindObjectsOfType<Building>();

        foreach (Building pBuilding in buildings)
        {
            if (pBuilding.GetBuildingPhase() == Building.BuildingPhase.PLACEMENT)
            {
                Destroy(pBuilding.gameObject);
            }
        }
        placementBuilding = null;
    }

    public bool PlacementBuildingActive()
    {
        return placementBuilding != null;
    }
}
