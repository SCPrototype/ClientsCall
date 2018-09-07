using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectionBuilding : Building {

    public CollectionBuilding(int cost) : base(cost)
    {

    }

    public void Collect()
    {
        //TODO: Tell the city how much you collected.
    }
}
