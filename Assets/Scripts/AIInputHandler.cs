using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInputHandler : MonoBehaviour {

    private BuildingHandler _buildingHandler;

    public AIInputHandler Initialize(BuildingHandler pBuildingHandler)
    {
        _buildingHandler = pBuildingHandler;

        return this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void makeMove(City pTarget)
    {
        //TODO: Add calculations for optimal move here
        //Move 1 tile to the right
        //pTarget.ChangeSelectedTile(InputHandler.DirectionKey.RIGHT);

        //Select the tile
        pTarget.GetSelectedTile().Reset();

        _buildingHandler.ChangeBuildingSelection(1);

        _buildingHandler.StartBuilding();

        GameInitializer.EndTurn();
    }
}
