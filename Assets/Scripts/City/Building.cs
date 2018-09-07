using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public enum BuildingPhase { PLACEMENT, INPROGRESS, DONE };
    private BuildingPhase _currentBuildingPhase;

    private Renderer _renderer;
    private Material _materialClone;
    private Material _material;

    private int BuildingCost;

    public Building(int cost)
    {
        BuildingCost = cost;
        getRenderer();
    }

    private void getRenderer()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _currentBuildingPhase = BuildingPhase.PLACEMENT;
        _material = _renderer.material;
        _materialClone = new Material(_material);
    }

    public BuildingPhase GetBuildingPhase()
    {
        return _currentBuildingPhase;
    }

    public void SetBuildingPhase(BuildingPhase pBuildingPhase)
    {
        if (_renderer == null)
        {
            getRenderer();
        }

        _currentBuildingPhase = pBuildingPhase;
        if (_currentBuildingPhase == BuildingPhase.PLACEMENT)
        {
            _materialClone.color = new Color(_materialClone.color.r, _materialClone.color.g, _materialClone.color.b, 0.5f);
        }
        if (_currentBuildingPhase == BuildingPhase.INPROGRESS)
        {
            _materialClone.color = _material.color;
        }
        _renderer.material = _materialClone;
    }

    public int GetCost()
    {
        return BuildingCost;
    }
}
