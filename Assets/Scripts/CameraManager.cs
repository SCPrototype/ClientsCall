﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour {

    private Camera _myCamera;

    private float _moveStartTime;
    private float _moveEndTime;
    private Vector3 _startPos;
    private Vector3 _targetPos;

	// Use this for initialization
	void Start () {
        _myCamera = GetComponent<Camera>();
        //MoveCameraTo(new Vector3(155, 31, -10), 5);
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time <= _moveEndTime)
        {
            //transform.position += ((_targetPos - _startPos) * Time.deltaTime) / (_moveEndTime - _moveStartTime);
            transform.position = _startPos + ((_targetPos - _startPos) * ((Time.time - _moveStartTime) / (_moveEndTime - _moveStartTime)));
        }
	}

    public void MoveCameraTo(Vector3 pTargetPos, float pTime)
    {
        _moveStartTime = Time.time;
        _startPos = transform.position;
        _targetPos = pTargetPos;
        _moveEndTime = Time.time + pTime;
    }
}
