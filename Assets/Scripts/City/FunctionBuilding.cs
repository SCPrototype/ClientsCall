using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FunctionBuilding : Building {

    private int actionCost;
    private string _description;

    public FunctionBuilding()
    {

    }

    public FunctionBuilding Initialize(int pCost, int pActionCost, string pDescription)
    {
        actionCost = pActionCost;
        _description = pDescription;
        base.Initialize(pCost);
        return this;
    }

    //Gives a description of the building to put into the info bar.
    public string GetDescription()
    {
        string text = "";
        return text;
    }

    //TODO: Dont forget to subtract money
    public abstract void DoAction();
}
