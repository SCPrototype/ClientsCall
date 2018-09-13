using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : ProductionBuilding {

    private const int _cost = 18;
    private const int _happinessGain = -2;
    private const int _moneyGain = 4;
    private const int _range = 1;
    //private ParticleSystem[] _particles = Glob.GetParticleEffects();
    public ParticleSystem _particle;
    
    public Factory()
    {
        
    }

    // Use this for initialization
    void Awake () {
        //_particle = _particles[0];
        base.Initialize(_cost, _happinessGain, _moneyGain, _range, _particle);
    }

    public Factory Initialize()
    {
        //_particle = _particles[0];
        base.Initialize(_cost, _happinessGain, _moneyGain, _range, _particle);
        return this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
