using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : CollectionBuilding {

    private const int _cost = 25;

    public House()
    {
        
    }

    // Use this for initialization
    void Awake() {
        base.Initialize(_cost);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
