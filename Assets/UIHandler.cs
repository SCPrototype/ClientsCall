using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    public static Text NotificationText;
    public static GameObject NotificationPanel;
	// Use this for initialization
	void Start () {
        NotificationPanel = GameObject.FindGameObjectWithTag("NotificationPanel");
        NotificationPanel.SetActive(false);
        NotificationText = NotificationPanel.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.G))
        {
            NotificationPanel.gameObject.SetActive(false);
        }
	}

    public static void ShowNotification(string message)
    {
        NotificationPanel.SetActive(true);
        NotificationText.text = message;
    }
}
