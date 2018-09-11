using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : ProductionBuilding {

    private const int _cost = 5;
    private const int _happinessGain = 4;
    private const int _moneyGain = -2;
    private const int _range = 1;

    public Park()
    {

    }

    void Awake()
    {
        base.Initialize(_cost, _happinessGain, _moneyGain, _range);
    }
}
