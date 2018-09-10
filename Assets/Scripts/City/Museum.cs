using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Museum : FunctionBuilding {

    public Museum()
    {

    }

    new public Museum Initialize(int pCost, int pActionCost)
    {
        base.Initialize(pCost, pActionCost);
        return this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void DoAction()
    {
        //TODO: Mine for a relic
    }
}
