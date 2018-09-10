using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProductionBuilding : Building
{

    private int happinessGain;
    private int moneyGain;
    private int tileAffectRange;


    public ProductionBuilding()
    {

    }

    public ProductionBuilding Initialize(int pCost, int pHappinessGain, int pMoneyGain, int pRange)
    {
        base.Initialize(pCost);
        happinessGain = pHappinessGain;
        moneyGain = pMoneyGain;
        tileAffectRange = pRange;
        return this;
    }

    public void Produce()
    {
        tileAffectRange = 1;
        moneyGain = 3;
        happinessGain = -2;
        //TODO: Tell all CollectionBuildings within range to collect.
        Building[] buildingsInRange = _myCity.GetBuildingsAroundTile(tileAffectRange, this.GetBuildingTile());
        foreach (Building pBuilding in buildingsInRange)
        {
            if (pBuilding is CollectionBuilding)
            {
                CollectionBuilding prodBuilding = pBuilding as CollectionBuilding;
                prodBuilding.Collect(moneyGain, happinessGain);
            }
        }
    }
}
