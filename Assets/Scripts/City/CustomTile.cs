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
    private ParticleSystem _particle;

    // Use this for initialization
    void Start()
    {
        _particle = this.transform.GetComponent<ParticleSystem>();
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
        _materialClone.color = _newBaseMaterial.color;
        _renderer.material = _materialClone;
    }

    //TODO: Change colors instead of enable/disabling it.
    public void InvertColor(Color targetColor)
    {
        if (_materialClone.color != _newBaseMaterial.color)
        {
            _materialClone.color = _newBaseMaterial.color;
        }
        else if (targetColor == new Color(0, 0, 0, 0))
        {
            _materialClone.color = new Color(_newBaseMaterial.color.r, _newBaseMaterial.color.g, _newBaseMaterial.color.b, 0.3f);
        }
        else
        {
            _materialClone.color = targetColor;
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
        }
        else
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
            _newBaseMaterial.color = new Color(Glob.HappyColor.r, Glob.HappyColor.g, Glob.HappyColor.b, 1);
        }
        else
        {
            _newBaseMaterial.color = new Color(Glob.UnhappyColor.r, Glob.UnhappyColor.g, Glob.UnhappyColor.b, 1);
        }
        _materialClone = new Material(_newBaseMaterial);
        _renderer.material = _materialClone;
    }
    public bool GetIsHappy()
    {
        return _isHappy;
    }

    public void SetInfluenceColor(Building pInfluenceBuilding, Building pBuildingUnderInfluence)
    {
        //Handle all different interaction between buildings.
        _newBaseMaterial.color = new Color(Glob.HappyColor.r, Glob.HappyColor.g, Glob.HappyColor.b, 1);
        _materialClone = new Material(_newBaseMaterial);
        _renderer.material = _materialClone;
        //Debug.Log("Influence color set.");
    }

    public void ReSetColor()
    {
        //Default should be 93AC58FF.
        //if (_colorHasChanged)

        if (_isHappy)
        {
            _newBaseMaterial.color = new Color(Glob.HappyColor.r, Glob.HappyColor.g, Glob.HappyColor.b, 1);
        }
        else
        {
            _newBaseMaterial.color = new Color(Glob.UnhappyColor.r, Glob.UnhappyColor.g, Glob.UnhappyColor.b, 1);
        }
        // }
        //else
        //{
        //    _newBaseMaterial.color = new Color(Glob.NeutralColor.r, Glob.NeutralColor.g, Glob.NeutralColor.b, 1);
        //}
        _materialClone = new Material(_newBaseMaterial);
        _renderer.material = _materialClone;
    }

    public void PlayParticle()
    {
        ParticleSystem childParticle = null;
        if (transform.childCount > 0)
        {
            childParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        }
        _particle.Play();
        if (childParticle != null)
        {
            childParticle.Stop();
        }
        ParticleSystem.EmissionModule em = _particle.emission;
        em.enabled = true;
    }

    public void StopParticle()
    {
        ParticleSystem.EmissionModule em = _particle.emission;
        em.enabled = false;
    }
}
