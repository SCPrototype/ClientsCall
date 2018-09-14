using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : FunctionBuilding {

    private const int _cost = 70;
    private const string _description = "Will make the surrounding area happy. \nPerhaps the enemy will become more friendly if they see you treat your people well.";

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
