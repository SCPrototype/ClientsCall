using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public enum BuildingPhase { PLACEMENT, DONE };
    private BuildingPhase _currentBuildingPhase;

    private Renderer _renderer;
    private Material _materialClone;
    private Material _material;

    private int _buildingCost;
    private City _myCity;
    private CustomTile _builtTitle;


    public Building()
    {

    }

    public Building Initialize(int pCost)
    {
        _buildingCost = pCost;
        return this;
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

    public CustomTile GetBuildingTile()
    {
        return _builtTitle;
    }

    public void SetBuildingTile(CustomTile pCustomTile)
    {
        Vector3 positionBuilding = pCustomTile.transform.position;

        positionBuilding.y = 0.5f;
        this.transform.position = positionBuilding;
        this.transform.parent = pCustomTile.transform;
        _myCity = pCustomTile.GetComponentInParent<City>();
        _builtTitle = pCustomTile;
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
        if (_currentBuildingPhase == BuildingPhase.DONE)
        {
            if (_materialClone != null)
            {
                _materialClone.color = _material.color;
            }
        }
        _renderer.material = _materialClone;
    }

    public int GetCost()
    {
        return _buildingCost;
    }

    public City GetCity()
    {
        return _myCity;
    }
    public void SetCity(City pCity)
    {
        _myCity = pCity;
    }
}
