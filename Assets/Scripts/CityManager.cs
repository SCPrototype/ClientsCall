﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CityManager : MonoBehaviour {

    public enum DirectionKey { LEFT, RIGHT, UP, DOWN };

    //This one belongs in the game handler.
    public enum CurrentMode { SELECTINGTILE, BUILDINGTILE, WAITINGFORTURN, EXAMINEMODE, MISSILEAIM };
    public static CurrentMode currentMode;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public abstract void HandleTurn(City pCity);


    public float GetTileValue(CustomTile pTile) //UGLY PART: Code duplication with AICityManager. Left in because of time pressure.
    {
        Building pBuilding = pTile.GetBuildingOnTile();
        if (pBuilding == null)
        {
            return 0;
        }
        float value = -(pBuilding.GetCost() / 3); //Remove cost from the value of the move.
        City tileCity = pTile.GetCity();

        bool buildingIsProduction = false;
        float happinessValue = 0;
        float moneyValue = 0;
        float collectionValue = 1;
        if (pBuilding is ProductionBuilding)
        {
            buildingIsProduction = true;
            ProductionBuilding prodBuilding = pBuilding as ProductionBuilding;
            happinessValue += prodBuilding.GetHappinessGain();
            moneyValue += prodBuilding.GetMoneyGain();
            collectionValue = 0;
        }

        Building[] buildingsInRange = tileCity.GetBuildingsAroundTile(1, pTile);
        foreach (Building b in buildingsInRange)
        {
            if (b is CollectionBuilding && buildingIsProduction)
            {
                //If this move places a production building next to a collection building, add value to the move.
                collectionValue += 1;
            }
            else if (b is ProductionBuilding)
            {
                if (!buildingIsProduction)
                {
                    //If this move places a collection building next to a production building, add value to the move.
                    ProductionBuilding prodBuilding = b as ProductionBuilding;
                    happinessValue += prodBuilding.GetHappinessGain();
                    moneyValue += prodBuilding.GetMoneyGain();
                }
                else if (pBuilding.GetType() == b.GetType())
                {
                    ProductionBuilding prodBuilding = pBuilding as ProductionBuilding;
                    happinessValue += prodBuilding.GetHappinessGain() * Glob.FactoryProductionMultiplier;
                    moneyValue += prodBuilding.GetMoneyGain() * Glob.FactoryProductionMultiplier; //If a production building is placed next to a production building of the same type, add value to the move.
                }
                else
                {
                    //If a production building is placed next to a production building of a different type, subtract value from the move.
                    ProductionBuilding prodBuilding = pBuilding as ProductionBuilding;
                    happinessValue -= prodBuilding.GetHappinessGain() * Glob.FactoryProductionMultiplier;
                    moneyValue -= prodBuilding.GetMoneyGain() * Glob.FactoryProductionMultiplier;
                }
            }
        }
        value += (happinessValue + moneyValue) * collectionValue;
        return value;
    }

    public CurrentMode GetCurrentMode()
    {
        return currentMode;
    }
    public void SetCurrentMode(CurrentMode pMode)
    {
        currentMode = pMode;
        Debug.Log("Set current mode to: " + pMode);
    }
}
