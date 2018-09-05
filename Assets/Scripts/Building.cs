using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public enum BuildingPhase { PLACEMENT, INPROGRESS, DONE };
    private BuildingPhase _currentBuildingPhase;

    private bool phaseChanged = false;

    private Renderer _renderer;
    private Material _materialClone;
    private Material _material;

    // Use this for initialization
    void Start()
    {
        //Gets the material and makes a copy of it for editing.
        _renderer = gameObject.GetComponent<Renderer>();
        _currentBuildingPhase = BuildingPhase.PLACEMENT;
        _material = _renderer.material;
        _materialClone = new Material(_material);
    }

    // Update is called once per frame
    void Update()
    {
        //Prevents it from updating all the time, TODO: Make it an event.
        if (phaseChanged == true)
        {
            phaseChanged = false;
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
    }

    public BuildingPhase GetBuildingPhase()
    {
        return _currentBuildingPhase;
    }

    public void SetBuildingPhase(BuildingPhase pBuildingPhase)
    {
        _currentBuildingPhase = pBuildingPhase;
        phaseChanged = true;
    }
}
