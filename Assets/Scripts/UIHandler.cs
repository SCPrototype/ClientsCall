using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    private GameObject buildPanel;
    private GameObject _examinePanel;
    private GameObject bottomBar;
    private static GameObject notificationPanel;
    private static Text notificationText;
    private GameObject eventMenu;
    private GameObject valuesPanel;
    private GameObject _buildInfoPanel;
    public Building[] _buildings;

    private Image[] buildingImages;
    private int currentBuildingSelection;
    private Text _buildInfoText;
    private ScrollRect scrollView;

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
        buildingImages = new Image[Glob.buildingCount];
        GameObject layoutGroup = buildPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject;
        for (int i = 0; i < Glob.buildingCount; i++)
        {
            buildingImages[i] = GameObject.Instantiate((Resources.Load(Glob.buildingImagePrefab) as GameObject), layoutGroup.transform).GetComponent<Image>();
            buildingImages[i].sprite = Glob.GetBuildingIcons()[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
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
        }
        else
        {
            currentBuildingSelection = index;

        }
        for (int i = 0; i < buildingImages.Length; i++)
        {
            if (i != currentBuildingSelection)
            {
                buildingImages[i].color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                buildingImages[i].color = new Color(1, 1, 1, 1);
                SetBuildingInfoText(_buildings[i]);
            }
        }
        float scrollAmount = (1.00f / (buildingImages.Length - 1) * currentBuildingSelection);
        scrollView.verticalNormalizedPosition = 1 - scrollAmount;

    }

    public void SetBuildingInfoText(Building pBuilding)
    {
        if (pBuilding is ProductionBuilding)
        {
            ProductionBuilding prodBuilding = Instantiate(pBuilding) as ProductionBuilding;
            int[] values = prodBuilding.GetMoneyHappinessRange();
            _buildInfoText.text = "<b>" +prodBuilding.GetType() + "</b>\nBuilding cost:" + prodBuilding.GetCost() + "\nProvides " + values[0] + " income each turn \nProvides " + values[1] + " happiness each turn\nAnd has a range of " + values[2];
            Destroy(prodBuilding.gameObject);
        }
        if (pBuilding is CollectionBuilding)
        {
            CollectionBuilding colBuilding = Instantiate(pBuilding) as CollectionBuilding;
            _buildInfoText.text = "<b>"+ colBuilding.GetType() + "</b>\nBuilding cost: " + colBuilding.GetCost() + "\nThis building collects resources from nearby buildings.";
            Destroy(colBuilding.gameObject);
        }
        if (pBuilding is FunctionBuilding)
        {
            FunctionBuilding funBuilding = pBuilding as FunctionBuilding;
            _buildInfoText.text = funBuilding.GetDescription();
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
        if(pBuilding is CollectionBuilding)
        {
            CollectionBuilding colBuilding = pBuilding as CollectionBuilding;
            text += "It collects resources from productionbuildings in range\n";
        }
        _examineText.text = text;
    }

    //Do not open /!\ Hazardous /!\
    private void Initialize()
    {
        _buildInfoPanel = GameObject.FindGameObjectWithTag("BuildingInfoPanel");
        _buildInfoText = _buildInfoPanel.GetComponentInChildren<Text>();
        _buildInfoPanel.SetActive(false);
        buildPanel = GameObject.FindGameObjectWithTag("BuildPanel");
        buildPanel.SetActive(false);
        scrollView = buildPanel.GetComponentInChildren<ScrollRect>();
        bottomBar = GameObject.FindGameObjectWithTag("BottomBar");
        notificationPanel = GameObject.FindGameObjectWithTag("NotificationPanel");
        notificationPanel.SetActive(false);
        notificationText = notificationPanel.GetComponentInChildren<Text>();
        eventMenu = GameObject.FindGameObjectWithTag("EventMenu");
        valuesPanel = GameObject.FindGameObjectWithTag("Values");
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
