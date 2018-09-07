using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{
    List<Building> placedBuildingList = new List<Building>();

    public void StartBuilding(Building pBuilding, CustomTile pCustomTile)
    {
        pBuilding.SetBuildingPhase(Building.BuildingPhase.INPROGRESS);
        pCustomTile.SetBuilding(pBuilding);
        placedBuildingList.Add(pBuilding);
    }

    public Building PlaceBuilding(CustomTile pCustomTile, Building pBuilding)
    {
        Building buildingToPlace = Instantiate(pBuilding);
        Vector3 positionBuilding = pCustomTile.transform.position;

        positionBuilding.y = buildingToPlace.transform.localScale.y / 2;
        buildingToPlace.transform.position = positionBuilding;
        buildingToPlace.transform.parent = pCustomTile.transform;

        //Makes a building which is into placement mode.
        buildingToPlace.SetBuildingPhase(Building.BuildingPhase.PLACEMENT);
        return buildingToPlace;
    }

    public void OnNewTurn()
    {
        int MoneyDifference = 0;
        int HappinessDifference = 0;

        foreach (Building pBuilding in placedBuildingList)
        {
            /*  if(pBuilding.type == type.collectionhouse)
             *  {
             *      MoneyDifference += pBuilding.Collect();
             *  }
             *  if(pBuilding.type == type.production)
             *  {
             *      int[] production = pBuilding.Production;
             *      MoneyDifference += production[0];
             *      HappinessDifference += production[1];
             *  }
             * 
             */
        }

        //Update City with new Money/Happiness.
    }
}
