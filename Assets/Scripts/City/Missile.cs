using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Missile : MonoBehaviour
{
    private SoundHandler _soundHandler;
    private Vector3 _position;


    public Missile(CustomTile pCustomTile)
    {
        this.transform.position = pCustomTile.transform.position;
    } 

    public Missile Initialize()
    {
        _soundHandler = GameInitializer.GetSoundHandler();
        _soundHandler.PlaySound(SoundHandler.Sounds.MISSILEHIT);
        return this;
    }

    public void SetMissileTile(CustomTile pCustomTile)
    {
        this.transform.position = pCustomTile.transform.position;
    }
}
