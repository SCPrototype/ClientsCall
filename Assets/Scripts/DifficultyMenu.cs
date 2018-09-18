using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameInitializer.HardModeEnabled = true;
            FadeToBlack.DoFade(1, true, 1);
            //Application.LoadLevel(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameInitializer.HardModeEnabled = false;
            FadeToBlack.DoFade(1, true, 1);
            //Application.LoadLevel(1);
        }
	}
}
