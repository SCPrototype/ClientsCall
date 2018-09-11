using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandlerX : MonoBehaviour
{

    private Vector3 _defaultPosition;
    private Vector3 _currentPosition;
    private Vector3 _targetPosition;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToPosition(Vector3 pTarget)
    {
        _targetPosition = pTarget;
    }

    public void Reset()
    {
        if (_currentPosition == _targetPosition) return;
        MoveToPosition(_defaultPosition);
    }
}
