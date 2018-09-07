using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProductionBuilding : Building {

    private int happinessGain;
    private int moneyGain;
    private int tileAffectRange;

    public ProductionBuilding(int cost, int pHappinessGain, int pMoneyGain, int range) : base(cost)
    {
        happinessGain = pHappinessGain;
        moneyGain = pMoneyGain;
        tileAffectRange = range;
    }

    public void Produce()
    {
        //TODO: Tell all CollectionBuildings within range to collect.
    }
}
