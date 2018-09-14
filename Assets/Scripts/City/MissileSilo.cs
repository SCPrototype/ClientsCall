using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSilo : FunctionBuilding {

    private const int _cost = 200;
    private const string _description = "If you build this you can launch a missile at the enemy's city! \nI'm sure they will surrender after being bombarded a few times.";

    public MissileSilo()
    {

    }

    void Awake()
    {
        base.Initialize(_cost, _description);
    }

    public MissileSilo Initialize()
    {
        base.Initialize(_cost, _description);
        return this;
    }


    public override void DoAction()
    {
        //TODO: Launch a missile
        Debug.Log("Did action.");
        GetCity().SetCurrentMode(CityManager.CurrentMode.MISSILEAIM);
    }
}
