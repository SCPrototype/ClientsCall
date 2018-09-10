using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{

    private Building[] _buildings;
    private int _currentBuildSelection;
    private Building _placementBuilding;
    private City _city;

    public BuildingHandler Initialize(City pCity)
    {
        _city = pCity;
        return this;
    }
    // Use this for initialization
    void Start()
    {
        _buildings = Glob.GetBuildingPrefabs();
    }

    public void StartBuilding()
    {
        _placementBuilding.SetBuildingPhase(Building.BuildingPhase.INPROGRESS);
        City.GetSelectedTile().SetBuilding(_placementBuilding);
        DestroyPlacementBuilding();
    }

   

    public Building PlaceBuilding(CustomTile pCustomTile)
    {
        Building buildingToPlace = Instantiate(_buildings[_currentBuildSelection]);
        //Makes a building which is into placement mode.
        buildingToPlace.SetBuildingTile(pCustomTile);
        buildingToPlace.SetBuildingPhase(Building.BuildingPhase.PLACEMENT);
        _city.BudgetChange(-10);
        // _city.BudgetChange(buildingToPlace.GetCost());
        return buildingToPlace;
    }

    public void ChangeBuildingSelection(int index, bool addToCurrent = true)
    {
        DestroyPlacementBuilding();
        if (addToCurrent)
        {
            for (int i = 0; i != index; i += (index / Mathf.Abs(index)))
            {
                _currentBuildSelection += (index / Mathf.Abs(index));
                if (_currentBuildSelection >= Glob.buildingCount)
                {
                    _currentBuildSelection = 0;
                }
                else if (_currentBuildSelection < 0)
                {
                    _currentBuildSelection = Glob.buildingCount - 1;
                }
            }
        }
        else
        {
            _currentBuildSelection = index;
        }
        _placementBuilding = PlaceBuilding(City.GetSelectedTile());
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
        _placementBuilding = null;
    }

    public bool PlacementBuildingActive()
    {
        return _placementBuilding != null;
    }
}
