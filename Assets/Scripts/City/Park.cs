using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : ProductionBuilding {

    public Park()
    {

    }

    new public Park Initialize(int pCost, int pHappinessGain, int pMoneyGain, int pRange)
    {
        base.Initialize(pCost, pHappinessGain, pMoneyGain, pRange);
        return this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
