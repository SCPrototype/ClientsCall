using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digsite : FunctionBuilding
{

    private const int _cost = 25;
    public ParticleSystem _particle;
    private const string _description = "Building one of these will give you a chance to find a relic at the start of your turn. \nFill up a museum with 10 relics, and display dominance through tourism!";
    
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
        int outcome = UnityEngine.Random.Range(0, 100);
        if(outcome < Glob.ChanceToMineRelic)
        {
            this.GetCity().AddRelic();
        }
    }
}
