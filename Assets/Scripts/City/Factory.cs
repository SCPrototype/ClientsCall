using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : ProductionBuilding {

    private const int _cost = 18;
    private const int _happinessGain = -5;
    private const int _moneyGain = 11;
    private const int _range = 1;
    
    public Factory()
    {
        
    }

    // Use this for initialization
    void Awake () {
        base.Initialize(_cost, _happinessGain, _moneyGain, _range);
    }

    public Factory Initialize()
    {
        base.Initialize(_cost, _happinessGain, _moneyGain, _range);
        return this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
