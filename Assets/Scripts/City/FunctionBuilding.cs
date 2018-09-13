using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FunctionBuilding : Building
{

    private string _description;

    public FunctionBuilding()
    {

    }

    public FunctionBuilding Initialize(int pCost, string pDescription)
    {
        _description = pDescription;
        base.Initialize(pCost);
        return this;
    }

    //Gives a description of the building to put into the info bar.
    public string GetDescription()
    {
        return _description;
    }

    //TODO: Dont forget to subtract money
    public abstract void DoAction();
}
