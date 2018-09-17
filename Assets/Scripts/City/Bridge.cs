using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : FunctionBuilding {

    private const int _cost = 50;
    private const string _description = "These workers will build a bridge across the river, but the enemy won't allow us to build on their side. Try to convince the enemy by increasing the happiness of your city.";

    public Bridge()
    {

    }

    void Awake()
    {
        base.Initialize(_cost, _description);
    }

    public Bridge Initialize()
    {
        base.Initialize(_cost, _description);
        return this;
    }

    public override void DoAction()
    {
        //TODO: Build a bridge.
    }
}
