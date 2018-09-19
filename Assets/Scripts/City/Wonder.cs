using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wonder : FunctionBuilding {

    private const int _cost = 350; //And 17 happy houses
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
        GameInitializer.GetCameraManager().MoveCameraTo(transform.position + Glob.CameraBuildingOffset, Glob.CameraBuildingZoomTime);
        if (GetCity().GetManager() is PlayerCityManager)
        {
            UIHandler.ShowNotification("Your wonder is finished! This massive structure is a symbol of wealth and prosperity, and a huge part of the inhabitants of AIton's city have moved to your city because of it. AIton has thrown in the towel.");
        } else
        {
            UIHandler.ShowNotification("AIton's wonder is finished! This massive structure is a symbol of wealth and prosperity, and a huge part of your inhabitants have moved to AIton's city because of it. You have no choice but to surrender.");
        }
        
        GameInitializer.EndGame(false, GetCity());
    }
}
