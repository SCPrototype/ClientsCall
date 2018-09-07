using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    private GameObject buildPanel;
    private GameObject bottomBar;
    private static GameObject notificationPanel;
    private static Text notificationText;
    private GameObject eventMenu;
    private GameObject valuesPanel;

    private Image[] buildingImages;
    private int currentBuildingSelection;

	// Use this for initialization
	void Start () {
        buildPanel = GameObject.FindGameObjectWithTag("BuildPanel");
        buildPanel.SetActive(false);
        bottomBar = GameObject.FindGameObjectWithTag("BottomBar");
        notificationPanel = GameObject.FindGameObjectWithTag("NotificationPanel");
        notificationPanel.SetActive(false);
        notificationText = notificationPanel.GetComponentInChildren<Text>();
        eventMenu = GameObject.FindGameObjectWithTag("EventMenu");
        valuesPanel = GameObject.FindGameObjectWithTag("Values");

        InitializeBuildPanel();
    }

    private void InitializeBuildPanel()
    {
        buildingImages = new Image[Glob.buildingCount];
        GameObject layoutGroup = buildPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject;
        for (int i = 0; i < Glob.buildingCount; i++)
        {
            buildingImages[i] = GameObject.Instantiate((Resources.Load(Glob.buildingImagePrefab) as GameObject), layoutGroup.transform).GetComponent<Image>();
            buildingImages[i].sprite = Glob.GetBuildingIcons()[i];
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.G))
        {
            notificationPanel.gameObject.SetActive(false);
        }
	}

    public static void ShowNotification(string message)
    {
        notificationPanel.SetActive(true);
        notificationText.text = message;
    }

    public void ToggleBuildPanel(bool toggle)
    {
        buildPanel.SetActive(toggle);
    }

    public void SetActiveBuildingImage(int index, bool addToCurrent = true)
    {
        if (addToCurrent)
        {
            for (int i = 0; i != index; i += (index / Mathf.Abs(index)))
            {
                currentBuildingSelection += (index / Mathf.Abs(index));
                if (currentBuildingSelection >= Glob.buildingCount)
                {
                    currentBuildingSelection = 0;
                }
                else if (currentBuildingSelection < 0)
                {
                    currentBuildingSelection = Glob.buildingCount - 1;
                }
            }
        } else
        {
            currentBuildingSelection = index;
        }
        for (int i = 0; i < buildingImages.Length; i++)
        {
            if (i != currentBuildingSelection)
            {
                buildingImages[i].color = new Color(1, 1, 1, 0.5f);
            } else
            {
                buildingImages[i].color = new Color(1, 1, 1, 1);
            }
        }
    }
}
