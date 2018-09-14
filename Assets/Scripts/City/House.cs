using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : CollectionBuilding {

    private const int _cost = 20;

    public House()
    {
        
    }

    // Use this for initialization
    void Awake() {
        base.Initialize(_cost);
    }

    public House Initialize()
    {
        base.Initialize(_cost);
        return this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
