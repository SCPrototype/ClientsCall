using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    public static Text NotificationText;
    public static GameObject NotificationPanel;
    private int _currentIndex = 0;

    private GameObject _buildingList;
    private Image[] _buildingImages;
    private ScrollRect _contentScroller;
    private RectTransform _contentImagesHolder;

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
                    _currentIndex--;
                    _currentIndex = Mathf.Clamp(_currentIndex, 0, _buildingImages.Length);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _currentIndex++;
                    _currentIndex = Mathf.Clamp(_currentIndex, 0, _buildingImages.Length - 1);
                }

                SetIconActive(_currentIndex);

                float scrollAmount = (1.00f / (_buildingImages.Length - 1) * _currentIndex);
                _contentScroller.verticalNormalizedPosition = 1 - scrollAmount;
            }
        }
    }

    private void Initialize()
    {
        NotificationPanel = GameObject.FindGameObjectWithTag("NotificationPanel");
        NotificationPanel.SetActive(false);
        NotificationText = NotificationPanel.GetComponentInChildren<Text>();
        _contentScroller = GameObject.FindGameObjectWithTag("BuildingScroller").GetComponent<ScrollRect>();
        _contentImagesHolder = GameObject.FindGameObjectWithTag("BuildingList").GetComponent<RectTransform>();
        _buildingImages = _contentImagesHolder.GetComponentsInChildren<Image>();


        SetIconActive(0);
    }

    public static void ShowNotification(string message)
    {
        NotificationPanel.SetActive(true);
        NotificationText.text = message;
    }

    private void SetIconActive(int pIndexNumber)
    {
        foreach (Image pImage in _buildingImages)
        {
            if (pImage == _buildingImages[pIndexNumber])
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
