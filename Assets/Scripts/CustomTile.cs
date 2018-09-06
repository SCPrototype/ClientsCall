using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTile : MonoBehaviour
{
    private Renderer _meshRenderer;

    private Renderer _renderer;
    private Material _materialClone;
    private Material _material;

    private Building _building;

    public enum TileState { HASBUILDING, DONTHAVEBUILDING };
    private TileState _tileState;

    // Use this for initialization
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        _materialClone = new Material(_material);
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
            _materialClone.color = new Color(_material.color.r, _material.color.g, _material.color.b, 1);
        }
        else
        {
            _materialClone.color = new Color(_material.color.r, _material.color.g, _material.color.b, 0.3f);
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
    }

    public Building GetBuildingOnTile()
    {
        return _building;
    }
}
