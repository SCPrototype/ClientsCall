using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTile : MonoBehaviour
{
    private City _myCity;
    
    private Renderer _meshRenderer;

    private Renderer _renderer;
    private Material _materialClone;
    private Material _newBaseMaterial;
    private Material _baseMaterial;

    private Building _building;

    public enum TileState { HASBUILDING, DONTHAVEBUILDING };
    private TileState _tileState;

    private bool _isHappy = false;

    // Use this for initialization
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _baseMaterial = _renderer.material;
        _newBaseMaterial = new Material(_baseMaterial);
        _materialClone = new Material(_newBaseMaterial);
    }

    public void SetCity(City pCity)
    {
        _myCity = pCity;
    }
    public City GetCity()
    {
        return _myCity;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Resets the tile, so it doesn't get stuck on the blink.
    public void Reset()
    {
        _materialClone.color = new Color(_materialClone.color.r, _materialClone.color.g, _materialClone.color.b, 1);
        _renderer.material = _materialClone;
    }

    //TODO: Change colors instead of enable/disabling it.
    public void InvertColor()
    {
        if (_materialClone.color.a < 1)
        {
            _materialClone.color = new Color(_newBaseMaterial.color.r, _newBaseMaterial.color.g, _newBaseMaterial.color.b, 1);
        }
        else
        {
            _materialClone.color = new Color(_newBaseMaterial.color.r, _newBaseMaterial.color.g, _newBaseMaterial.color.b, 0.3f);
        }
        _renderer.material = _materialClone;

    }

    public void SetState(TileState pTileState)
    {
        _tileState = pTileState;
    }

    public TileState GetState()
    {
        return _tileState;
    }

    public void SetBuilding(Building pBuilding)
    {
        _building = pBuilding;
        bool shouldSetHappy = false;
        if (pBuilding is Park)
        {
            shouldSetHappy = true;
        }
        else if (pBuilding is Factory)
        {
            shouldSetHappy = false;
        } else
        {
            return;
        }
        _myCity.HandleHappiness(this, shouldSetHappy);
    }

    public Building GetBuildingOnTile()
    {
        return _building;
    }

    public void SetIsHappy(bool pHappy)
    {
        if (_renderer == null)
        {
            _renderer = GetComponent<Renderer>();
            _baseMaterial = _renderer.material;
            _newBaseMaterial = new Material(_baseMaterial);
            _materialClone = new Material(_newBaseMaterial);
        }
        _isHappy = pHappy;
        if (_isHappy)
        {
            _newBaseMaterial.color = new Color(Glob.happyColor.r, Glob.happyColor.g, Glob.happyColor.b, 1);
        } else
        {
            _newBaseMaterial.color = new Color(Glob.unhappyColor.r, Glob.unhappyColor.g, Glob.unhappyColor.b, 1);
        }
        _materialClone = new Material(_newBaseMaterial);
        _renderer.material = _materialClone;
    }
    public bool GetIsHappy()
    {
        return _isHappy;
    }
}
