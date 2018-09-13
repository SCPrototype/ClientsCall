using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : FunctionBuilding {

    private const int _cost = 10;

    public Park()
    {

    }

    void Awake()
    {
        base.Initialize(_cost, "Iz a parc.");
    }

    public Park Initialize()
    {
        base.Initialize(_cost, "Iz a parc.");
        return this;
    }

    public override void DoAction()
    {
        
    }
}
