using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    private GameObject _buildPanel;
    private GameObject _examinePanel;
    private GameObject _bottomBar;
    private static GameObject _notificationPanel;
    private static Text _notificationText;
    private Text _turnCounter;
    private GameObject _eventMenu;
    private GameObject _valuesPanel;
    private GameObject _buildInfoPanel;
    private Building[] _buildings;

    private Image[] _buildingImages;
    private int _currentBuildingSelection;
    private Text _buildInfoText;
    private ScrollRect _scrollView;

    private Slider _budgetSlider;
    private Slider _happinessSlider;
    private Text _budgetText;
    private Text _happinessText;
    private Text _examineText;


    // Use this for initialization
    void Start()
    {
        Initialize();
        InitializeBuildPanel();
    }

    private void InitializeBuildPanel()
    {
        _buildingImages = new Image[Glob.buildingCount];
        GameObject layoutGroup = _buildPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject;
        for (int i = 0; i < Glob.buildingCount; i++)
        {
            _buildingImages[i] = GameObject.Instantiate((Resources.Load(Glob.buildingImagePrefab) as GameObject), layoutGroup.transform).GetComponent<Image>();
            _buildingImages[i].sprite = Glob.GetBuildingIcons()[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _notificationPanel.gameObject.SetActive(false);
        }
    }

    public static void ShowNotification(string message)
    {
        _notificationPanel.SetActive(true);
        _notificationText.text = message;
    }
    public static void ToggleNotificationPanel(bool toggle)
    {
        _notificationPanel.SetActive(toggle);
    }

    public void ToggleBuildPanel(bool toggle)
    {
        _buildPanel.SetActive(toggle);
    }

    public void ToggleExaminePanel(bool toggle)
    {
        _examinePanel.SetActive(toggle);
    }

    public void SetActiveBuildingImage(int index, bool addToCurrent = true)
    {
        _buildInfoPanel.SetActive(true);
        if (addToCurrent)
        {
            for (int i = 0; i != index; i += (index / Mathf.Abs(index)))
            {
                _currentBuildingSelection += (index / Mathf.Abs(index));
                if (_currentBuildingSelection >= Glob.buildingCount)
                {
                    _currentBuildingSelection = 0;
                }
                else if (_currentBuildingSelection < 0)
                {
                    _currentBuildingSelection = Glob.buildingCount - 1;
                }
            }
        }
        else
        {
            _currentBuildingSelection = index;

        }
        for (int i = 0; i < _buildingImages.Length; i++)
        {
            if (i != _currentBuildingSelection)
            {
                _buildingImages[i].color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                _buildingImages[i].color = new Color(1, 1, 1, 1);
                SetBuildingInfoText(_buildings[i]);
            }
        }
        float scrollAmount = (1.00f / (_buildingImages.Length - 1) * _currentBuildingSelection);
        _scrollView.verticalNormalizedPosition = 1 - scrollAmount;

    }

    public void SetBuildingInfoText(Building pBuilding)
    {
        if (pBuilding is ProductionBuilding)
        {
            ProductionBuilding prodBuilding = pBuilding as ProductionBuilding;
            int[] values = prodBuilding.GetMoneyHappinessRange();
            _buildInfoText.text = "<b>" +prodBuilding.GetType() + "</b>\nBuilding cost:" + prodBuilding.GetCost() + "\nProvides " + values[0] + " income each turn \nProvides " + values[1] + " happiness each turn\nAnd has a range of " + values[2];
        }
        if (pBuilding is CollectionBuilding)
        {
            CollectionBuilding colBuilding = pBuilding as CollectionBuilding;
            _buildInfoText.text = "<b>"+ colBuilding.GetType() + "</b>\nBuilding cost: " + colBuilding.GetCost() + "\nThis building collects resources from nearby production buildings.";
        }
        if (pBuilding is FunctionBuilding)
        {
            FunctionBuilding funBuilding = pBuilding as FunctionBuilding;
            _buildInfoText.text = "<b>" + funBuilding.GetType() + "</b>\nBuilding cost: " + funBuilding.GetCost() + "\n" + funBuilding.GetDescription();
        }
    }

    public void SetResourcesBars(int pBudget, int pHappiness)
    {
        pBudget = Mathf.Clamp(pBudget, 0, 100);
        _budgetText.text = "Budget: " + pBudget + "/100";
        _budgetSlider.value = pBudget / 100f;
        pHappiness = Mathf.Clamp(pHappiness, 0, 100);
        _happinessText.text = "Happiness: " + pHappiness + "/100";
        _happinessSlider.value = pHappiness / 100f;
    }
    public void SetExamineMode(Building pBuilding)
    {
        ToggleExaminePanel(true);
        string text = "This is a <b>" + pBuilding.GetType() + "</b>\n";
        if (pBuilding is ProductionBuilding)
        {
            ProductionBuilding prodBuilding = pBuilding as ProductionBuilding;
            text += "It produces <b>" + prodBuilding.GetMoneyHappinessRange()[0] + "</b> Income\n It produces <b>" + prodBuilding.GetMoneyHappinessRange()[1] + "</b> happiness\n";
            text += "And has a range of <b>" + prodBuilding.GetMoneyHappinessRange()[2] + "</b>";
        }
        else if(pBuilding is CollectionBuilding)
        {
            CollectionBuilding colBuilding = pBuilding as CollectionBuilding;
            text += "It collects resources from production buildings in range.\n";
        }
        else if (pBuilding is FunctionBuilding)
        {
            FunctionBuilding funBuilding = pBuilding as FunctionBuilding;
            text += funBuilding.GetDescription();
        }
        _examineText.text = text;
    }

    public void SetTurnText(int pTurn)
    {
        _turnCounter.text = "Turn: " + pTurn + "/" + Glob.TurnAmount;
    }

    //Do not open /!\ Hazardous /!\
    private void Initialize()
    {
        _buildInfoPanel = GameObject.FindGameObjectWithTag("BuildingInfoPanel");
        _buildInfoText = _buildInfoPanel.GetComponentInChildren<Text>();
        _buildInfoPanel.SetActive(false);
        _buildPanel = GameObject.FindGameObjectWithTag("BuildPanel");
        _buildPanel.SetActive(false);
        _scrollView = _buildPanel.GetComponentInChildren<ScrollRect>();
        _bottomBar = GameObject.FindGameObjectWithTag("BottomBar");
        _notificationPanel = GameObject.FindGameObjectWithTag("NotificationPanel");
        _notificationPanel.SetActive(false);
        _notificationText = _notificationPanel.GetComponentInChildren<Text>();
        _turnCounter = GameObject.FindGameObjectWithTag("TurnCounter").GetComponent<Text>();
        _eventMenu = GameObject.FindGameObjectWithTag("EventMenu");
        _valuesPanel = GameObject.FindGameObjectWithTag("Values");
        _budgetSlider = GameObject.FindGameObjectWithTag("BudgetSlider").GetComponent<Slider>();
        _happinessSlider = GameObject.FindGameObjectWithTag("HappinessSlider").GetComponent<Slider>();
        _budgetText = _budgetSlider.GetComponentInChildren<Text>();
        _happinessText = _happinessSlider.GetComponentInChildren<Text>();
        _examinePanel = GameObject.FindGameObjectWithTag("ExaminePanel");
        _examineText = _examinePanel.GetComponentInChildren<Text>();
        _examinePanel.SetActive(false);

        _buildings = Glob.GetBuildingPrefabs();

    }
}
