using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Missile : MonoBehaviour
{
    private SoundHandler _soundHandler;
    private Vector3 _position;

    private bool _activateAnimation;
    private float _waitTime;
    private float _moveStartTime;
    private float _moveEndTime;
    public Animator _animator;



    public Missile(CustomTile pCustomTile)
    {
        this.transform.position = pCustomTile.transform.position;
    } 

    public Missile Initialize()
    {
        _animator = transform.parent.GetComponent<Animator>();
        _soundHandler = GameInitializer.GetSoundHandler();
        _soundHandler.PlaySound(SoundHandler.Sounds.MISSILEHIT);
        return this;
    }

    public void Update()
    {
        if (Time.time >= _moveStartTime && _activateAnimation == true)
        {
            Debug.Log("Play Missile");
            _animator.SetBool("isAnimationReady", true);
            _activateAnimation = false;
        }
    }

    public void SetMissileTile(CustomTile pCustomTile)
    {
        this.transform.position = pCustomTile.transform.position;
    }

    public void WaitWithAnimation(int pSeconds)
    {
        _activateAnimation = true;
        _waitTime = pSeconds;
        _moveStartTime = Time.time + pSeconds;
    }
}
