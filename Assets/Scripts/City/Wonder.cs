using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wonder : FunctionBuilding {

    private const int _cost = 350; //And 11 happy houses
    private const string _description = "No one will dare challenge you after laying eyes on this collossal structure. \nYou do need happy inhabitants if you want to spend their tax money on this though...";

    public Wonder()
    {

    }

    void Awake()
    {
        base.Initialize(_cost, _description);
    }

    public Wonder Initialize()
    {
        base.Initialize(_cost, _description);
        return this;
    }

    public override void DoAction()
    {
        GameInitializer.EndGame(false, GetCity());
    }
}
