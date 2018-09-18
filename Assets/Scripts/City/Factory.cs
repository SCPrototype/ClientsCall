using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : ProductionBuilding {

    private const int _cost = 50;
    private const int _happinessGain = -2;
    private const int _moneyGain = 10;
    private const int _range = 1;
    public int _currentBoost = 0;
    //private ParticleSystem[] _particles = Glob.GetParticleEffects();
    private ParticleSystem _particle;
    
    public Factory()
    {
        
    }

    // Use this for initialization
    void Awake () {
        //_particle = _particles[0];
        _particle = this.transform.GetComponent<ParticleSystem>();
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

    public void AddBoost(int pBoost)
    {
        _currentBoost += pBoost;
    }

    public int GetBoost()
    {
        return _currentBoost;
    }
}
