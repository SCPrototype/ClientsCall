using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : ProductionBuilding {

    private const int _cost = 5;
    private const int _happinessGain = -3;
    private const int _moneyGain = 4;
    private const int _range = 1;
    
    public Factory()
    {
        
    }

    // Use this for initialization
    void Awake () {
        base.Initialize(_cost, _happinessGain, _moneyGain, _range);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
