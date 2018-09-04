using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTile : MonoBehaviour {

    private Renderer _meshRenderer;
	// Use this for initialization
	void Start () {
        _meshRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset()
    {
        if(_meshRenderer.enabled == false)
        {
            _meshRenderer.enabled = true;
        }
    }

    public void InvertColor()
    {
        if(_meshRenderer.enabled == true)
        { _meshRenderer.enabled = false; } else
        {
            _meshRenderer.enabled = true;
        }
        
    }
}
