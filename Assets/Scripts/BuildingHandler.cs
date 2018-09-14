using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{

    private Building[] buildings;
    private int currentBuildingSelection;
    private Building placementBuilding;
    private City currentCity;
    private bool readyToBuild = true;
    private float prevTurn;

    // Use this for initialization
    void Start()
    {
        buildings = Glob.GetBuildingPrefabs();
    }

    // Update is called once per frame
    void Update()
    {
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
        GameInitializer.GetCameraManager().MoveCameraTo(pCity.transform.position + Glob.CameraOffset, Glob.CameraCitySwitchTime);
    }
    public City GetCurrentCity()
    {
        return currentCity;
    }

    public void QuickBuildBuilding(City pCity, CustomTile pCustomTile, int pBuildingIndex)
    {
        if (buildings == null)
        {
            buildings = Glob.GetBuildingPrefabs();
        }
        Building buildingToPlace = Instantiate(buildings[pBuildingIndex]);
        buildingToPlace.SetBuildingTile(pCustomTile);
        pCustomTile.SetBuilding(buildingToPlace);
        buildingToPlace.SetBuildingPhase(Building.BuildingPhase.DONE);
    }

    public bool StartBuilding()
    {
        //Can the building get placed.
        if (currentCity.CanBuild(placementBuilding.GetCost()))
        {
            if (placementBuilding is Wonder)
            {
                if (currentCity.GetHappyHouseAmount() < Glob.WonderHappyHouseReq)
                {
                    //TODO: Give player info about needing more happy houses.
                    UIHandler.ShowNotification("The inhabitants are preventing your workers from building this. They don't want you 'wasting' money on this, instead of making them happy. Try building some parks next to houses first.");
                    return false;
                }
            }
            //Place the building.
            //Check for building type if there is another building tile near it. If so, upgruade the building to the amount of buildings.
            //Give the other building an index of +1.
            placementBuilding = UpgruadeBuilding(placementBuilding);

            if(placementBuilding is Factory)  placementBuilding = Instantiate(placementBuilding);
            placementBuilding.SetBuildingPhase(Building.BuildingPhase.DONE);
            placementBuilding.SetBuildingTile(currentCity.GetSelectedTile());
            currentCity.GetSelectedTile().SetBuilding(placementBuilding);
            currentCity.BudgetChange(-placementBuilding.GetCost());
            if (placementBuilding is MissileSilo)//TODO: Hard coded spaghett
            {
                MissileSilo building = placementBuilding as MissileSilo;
                building.DoAction();
            }
            else if (placementBuilding is Wonder)
            {
                Wonder building = placementBuilding as Wonder;
                building.DoAction();
            }
            DestroyPlacementBuilding();
            return true;
        }
        else
        {
            UIHandler.ShowNotification("You don't have enough money to build that.");
            return false;
        }
    }

    public Building UpgruadeBuilding(Building pBuilding)
    {
        if (pBuilding is Factory)
        {
            Building[] buildingsInRange = currentCity.GetBuildingsAroundTile(1, pBuilding.GetBuildingTile());
            Factory[] factoriesPrefabs = Glob.GetFactoriesPrefabs();
            int amountOfFactories = 0;
            foreach (Building pBuildingFromList in buildingsInRange)
            {
                if (pBuildingFromList is Factory)
                {
                    amountOfFactories++;
                    ReplaceFactory(pBuildingFromList as Factory, 1);
                }
            }

            Factory factoryToPlace;
            if (amountOfFactories > factoriesPrefabs.Length)
            {
                amountOfFactories = factoriesPrefabs.Length;
            }
            factoryToPlace = factoriesPrefabs[amountOfFactories];
            factoryToPlace = Instantiate(factoryToPlace);
            factoryToPlace.AddBoost(amountOfFactories);
            return factoryToPlace;
        }
        else
        {
            return pBuilding;
        }
    }

    public void ReplaceFactory(Factory pCurrentFactory, int indexChange)
    {
        Factory[] factoriesPrefab = Glob.GetFactoriesPrefabs();
        int boost = pCurrentFactory.GetBoost();
        Factory newFactory = Instantiate(factoriesPrefab[boost + indexChange]);
        newFactory.AddBoost(boost + indexChange);
        newFactory.SetBuildingPhase(Building.BuildingPhase.DONE);
        newFactory.SetBuildingTile(pCurrentFactory.GetBuildingTile());
        pCurrentFactory.GetBuildingTile().SetBuilding(newFactory);
        Destroy(pCurrentFactory.gameObject);
    }

    public Building PlaceBuilding(CustomTile pCustomTile)
    {
        Building buildingToPlace = Instantiate(buildings[currentBuildingSelection]);
        //Makes a building which is into placement mode.
        buildingToPlace.SetBuildingTile(pCustomTile);
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
        placementBuilding = PlaceBuilding(currentCity.GetSelectedTile());
    }
    public void ChangeBuildingSelection(Building pBuilding)
    {
        DestroyPlacementBuilding();
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i] == pBuilding)
            {
                currentBuildingSelection = i;
                break;
            }
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
