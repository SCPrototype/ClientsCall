using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digsite : FunctionBuilding
{

    private const int _cost = 9;
    public ParticleSystem _particle;
    private const string _description = "This is a dig-site, use it to dig up relics.";
    
    public Digsite()
    {

    }

    void Awake()
    {
        base.Initialize(_cost, _description);
    }

    public Digsite Initialize()
    {
        //_particle = _particles[0];
        base.Initialize(_cost, _description);
        return this;
    }

    public override void DoAction()
    {
        //TODO: Mine for a relic
        //Change to a different number for more chance.
        int outcome = UnityEngine.Random.Range(0, 2);
        switch(outcome)
        {
            case 1:
                this.GetCity().AddRelic(1);
                _particle.Play();
                ParticleSystem.EmissionModule em = _particle.emission;
                em.enabled = true;
                break;
            default:
                //Nothing happened.
                break;
        }
    }
}
