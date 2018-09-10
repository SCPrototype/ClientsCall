using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Museum : FunctionBuilding {

    public Museum(int cost, int pActionCost , City pCity) : base(cost, pActionCost, pCity)
    {

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
