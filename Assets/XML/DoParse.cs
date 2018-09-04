using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoParse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TMX_Parser parser = new TMX_Parser();
            AllEvents allEvents;
            parser.Parse("Assets/XML/RandomEvents.txt", out allEvents);
        }
	}
}
