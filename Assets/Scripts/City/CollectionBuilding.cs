using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectionBuilding : Building
{
    

    public CollectionBuilding(int cost, City pCity) : base(cost, pCity)
    {
   
    }

    public void Collect(int pIncome, int pHappiness)
    {
        //TODO: Tell collection buildings to collect from nearby production buildings   
        this.GetCity().ReceiveCollection(pIncome, pHappiness);
    }
}
