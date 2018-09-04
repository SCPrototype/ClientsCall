using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public enum BuildingPhase { INPROGRESS, DONE };
    private BuildingPhase _currentBuildingPhase;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public BuildingPhase GetBuildingPhase()
    {
        return _currentBuildingPhase;
    }

    public void SetBuildingPhase(BuildingPhase pBuildingPhase)
    {
        _currentBuildingPhase = pBuildingPhase;
    }
}
