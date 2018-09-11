using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProductionBuilding : Building
{

    private int _happinessGain;
    private int _moneyGain;
    private int _tileAffectRange;


    public ProductionBuilding()
    {

    }

    public ProductionBuilding Initialize(int pCost, int pHappinessGain, int pMoneyGain, int pRange)
    {
        base.Initialize(pCost);
        _happinessGain = pHappinessGain;
        _moneyGain = pMoneyGain;
        _tileAffectRange = pRange;
        return this;
    }

    public void Produce()
    {
        //TODO: Tell all CollectionBuildings within range to collect.
        Building[] buildingsInRange = _myCity.GetBuildingsAroundTile(_tileAffectRange, this.GetBuildingTile());
        foreach (Building pBuilding in buildingsInRange)
        {
            if (pBuilding is CollectionBuilding)
            {
                CollectionBuilding prodBuilding = pBuilding as CollectionBuilding;
                prodBuilding.Collect(_moneyGain, _happinessGain);
            }
        }
    }

    public int GetHappinessGain()
    {
        return _happinessGain;
    }
    public int GetMoneyGain()
    {
        return _moneyGain;
    }
    public int GetRange()
    {
        return _tileAffectRange;
    }
}
