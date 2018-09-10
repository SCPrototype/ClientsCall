using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectionBuilding : Building
{
    public CollectionBuilding()
    {
   
    }

    new public CollectionBuilding Initialize(int pCost)
    {
        base.Initialize(pCost);
        return this;
    }

    public void Collect(int pIncome, int pHappiness)
    {
        //TODO: Tell collection buildings to collect from nearby production buildings   
        this.GetCity().ReceiveCollection(pIncome, pHappiness);
    }
}
