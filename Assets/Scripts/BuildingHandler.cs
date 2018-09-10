using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour {

    private Building[] buildings;
    private int currentBuildingSelection;
    private Building placementBuilding;
    private City currentCity;
    private bool readyToBuild = true;
    private float prevTurn;

    // Use this for initialization
    void Start () {
        buildings = Glob.GetBuildingPrefabs();
    }
	
	// Update is called once per frame
	void Update () {
        if (!readyToBuild)
        {
            if (Time.time - prevTurn >= Glob.TurnDelay)
            {
                readyToBuild = true;
            }
        }
	}

    public void SetCurrentCity(City pCity)
    {
        if (currentCity != null)
        {
            currentCity.SetCurrentMode(CityManager.CurrentMode.WAITINGFORTURN);//TODO: Fix the flashing tiles
        }
        currentCity = pCity;
        currentCity.SetCurrentMode(CityManager.CurrentMode.SELECTINGTILE);
        prevTurn = Time.time;
        readyToBuild = false;
    }
    public City GetCurrentCity()
    {
        return currentCity;
    }

    public void StartBuilding()
    {
        placementBuilding.SetBuildingPhase(Building.BuildingPhase.INPROGRESS);
        currentCity.GetSelectedTile().SetBuilding(placementBuilding);
        currentCity.BudgetChange(placementBuilding.GetCost());
        DestroyPlacementBuilding();
        // currentCity.BudgetChange(buildingToPlace.GetCost());
    }

    public Building PlaceBuilding(CustomTile pCustomTile)
    {
        Building buildingToPlace = Instantiate(buildings[currentBuildingSelection]);
        //Makes a building which is into placement mode.
        buildingToPlace.SetBuildingTile(pCustomTile);
        buildingToPlace.SetBuildingPhase(Building.BuildingPhase.PLACEMENT);

        Debug.Log(buildingToPlace.GetCost());
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
        placementBuilding = PlaceBuilding(currentCity.GetSelectedTile());
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

    public bool IsReadyToBuild()
    {
        return readyToBuild;
    }
}
