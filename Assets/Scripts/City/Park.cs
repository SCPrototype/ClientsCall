using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : ProductionBuilding {

    private const int _cost = 10;
    private const int _happinessGain = 4;
    private const int _moneyGain = -2;
    private const int _range = 1;
    //private ParticleSystem[] _particles = Glob.GetParticleEffects();
    public ParticleSystem _particle;

    public Park()
    {

    }

    void Awake()
    {
        //_particle = _particles[1];
        base.Initialize(_cost, _happinessGain, _moneyGain, _range, _particle);
    }

    public Park Initialize()
    {
       // _particle = _particles[1];
        base.Initialize(_cost, _happinessGain, _moneyGain, _range, _particle);
        return this;
    }
}
