using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FunctionBuilding : Building {

    private int actionCost;

    public FunctionBuilding(int cost, int pActionCost, City pCity) : base(cost, pCity)
    {
        actionCost = pActionCost;
    }

    //TODO: Dont forget to subtract money
    public abstract void DoAction();
}
