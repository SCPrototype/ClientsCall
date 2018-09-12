using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : ProductionBuilding {

    private const int _cost = 10;
    private const int _happinessGain = 4;
    private const int _moneyGain = -6;
    private const int _range = 1;

    public Park()
    {

    }

    void Awake()
    {
        base.Initialize(_cost, _happinessGain, _moneyGain, _range);
    }

    public Park Initialize()
    {
        base.Initialize(_cost, _happinessGain, _moneyGain, _range);
        return this;
    }
}
