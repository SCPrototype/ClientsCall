using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FunctionBuilding : Building {

    private int actionCost;

    public FunctionBuilding()
    {

    }

    public FunctionBuilding Initialize(int pCost, int pActionCost)
    {
        actionCost = pActionCost;
        base.Initialize(pCost);
        return this;
    }

    //TODO: Dont forget to subtract money
    public abstract void DoAction();
}
