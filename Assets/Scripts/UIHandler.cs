using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    public static Text NotificationText;
    public static GameObject NotificationPanel;
    public Image[] BuildingImages;
    private int currentIndex = 0;
    //Replace this with a constant probably.
    public RectTransform ImagesContent;
    public ScrollRect ScrollRectangle;

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                NotificationPanel.gameObject.SetActive(false);
            }
            if (InputHandler.currentMode == InputHandler.CurrentMode.BUILDINGTILE)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    currentIndex--;
                    currentIndex = Mathf.Clamp(currentIndex, 0, BuildingImages.Length);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    currentIndex++;
                    currentIndex = Mathf.Clamp(currentIndex, 0, BuildingImages.Length - 1);
                }
                SetIconActive(currentIndex);

                float number = 1.00f / (BuildingImages.Length -1);
                float anotherNumber = number * currentIndex;
               
                Debug.Log(anotherNumber);
                ScrollRectangle.verticalNormalizedPosition = 1 - anotherNumber;
            }
        }
    }

    private void Initialize()
    {
        NotificationPanel = GameObject.FindGameObjectWithTag("NotificationPanel");
        NotificationPanel.SetActive(false);
        NotificationText = NotificationPanel.GetComponentInChildren<Text>();

        SetIconActive(0);
    }

    public static void ShowNotification(string message)
    {
        NotificationPanel.SetActive(true);
        NotificationText.text = message;
    }

    private void SetIconActive(int pIndexNumber)
    {
        foreach (Image pImage in BuildingImages)
        {
            if (pImage == BuildingImages[pIndexNumber])
            {
                pImage.color = new Color(1, 1, 1, 1);
            }
            else
            {
                pImage.color = new Color(1, 1, 1, 0.5f);
            }
        }
    }

}
