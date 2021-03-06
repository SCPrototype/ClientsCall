﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProductionBuilding : Building
{

    private int _happinessGain;
    private int _moneyGain;
    private int _tileAffectRange;
    private ParticleSystem _particle;
    private SoundHandler _soundHandler;

    void Start()
    {
        _soundHandler = GameInitializer.GetSoundHandler();
    }

    public ProductionBuilding()
    {

    }

    public ProductionBuilding Initialize(int pCost, int pHappinessGain, int pMoneyGain, int pRange, ParticleSystem pParticle)
    {
        
        base.Initialize(pCost);
        _happinessGain = pHappinessGain;
        _moneyGain = pMoneyGain;
        _tileAffectRange = pRange;
        _particle = pParticle;
        return this;
    }

    public float Produce()
    {
        float value = 0;
        Building[] buildingsInRange = GetCity().GetBuildingsAroundTile(_tileAffectRange, this.GetBuildingTile());
        foreach (Building pBuilding in buildingsInRange)
        {
            if (pBuilding is CollectionBuilding)
            {
                CollectionBuilding prodBuilding = pBuilding as CollectionBuilding;
                StartCoroutine(PlayParticle());
                //prodBuilding.Collect(_moneyGain, _happinessGain);
                value += GetIncomeWithMultiplier();
            }
        }

        return value;
    }

    public IEnumerator PlayParticle()
    {
        _soundHandler.PlaySound(SoundHandler.Sounds.MONEY);
        _particle.Play();
        ParticleSystem.EmissionModule em = _particle.emission;
        em.enabled = true;
        yield return Glob.AnimationCollection;
    }

    public int[] GetMoneyHappinessRange()
    {
        int[] valueArray = new int[3];
        valueArray[0] = _moneyGain;
        valueArray[1] = _happinessGain;
        valueArray[2] = _tileAffectRange;
        return valueArray;
    }

    public void PlayAnimation()
    {
        //nothing yet.
    }

    public int GetHappinessGain()
    {
        return _happinessGain;
    }
    public int GetMoneyGain()
    {
        return _moneyGain;
    }
    public int GetRange()
    {
        return _tileAffectRange;
    }

    public float GetIncomeWithMultiplier()
    {
        float value = 0;
        if (this is Factory)
        {
            Factory factory = this as Factory;
           
            value = _moneyGain + (factory.GetBoost() * Glob.FactoryProductionMultiplier);
            
        } else
        {

        }
        return value;
    }
}
