using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CityManager : MonoBehaviour {

    public enum DirectionKey { LEFT, RIGHT, UP, DOWN };

    //This one belongs in the game handler.
    public enum CurrentMode { SELECTINGTILE, BUILDINGTILE, WAITINGFORTURN };
    public static CurrentMode currentMode;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public abstract void HandleTurn(City pCity);
}
