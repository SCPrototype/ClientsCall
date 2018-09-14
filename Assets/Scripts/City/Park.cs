using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : FunctionBuilding {

    private const int _cost = 10;
    private const string _description = "Will make the surrounding area happy. Perhaps the enemy will become more friendly if he sees you treat you people well.";

    public Park()
    {

    }

    void Awake()
    {
        base.Initialize(_cost, _description);
    }

    public Park Initialize()
    {
        base.Initialize(_cost, _description);
        return this;
    }

    public override void DoAction()
    {
        
    }
}
