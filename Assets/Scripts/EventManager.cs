using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    private AllEvents allEvents;
    private RandomEvent currentEvent;

    public GameObject EventMenu;

    public Text eventText;

    public Text choiceText1;
    public Text choiceText2;
    public Text choiceText3;

    public GameObject effectBox;
    public Text effectText;

    private int budget = 75;
    private int happiness = 50;
    public Slider BudgetSlider;
    public Slider HappinessSlider;
    public Text BudgetText;
    public Text HappinessText;

    // Use this for initialization
    void Start()
    {
        TMX_Parser parser = new TMX_Parser();
        parser.Parse("Assets/XML/RandomEvents.txt", out allEvents);
        Debug.Log(allEvents);

        EventMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnableRandomEvent();
        }
    }

    public void EnableRandomEvent()
    {
        currentEvent = allEvents.events[Random.Range(0, allEvents.events.Length)];
        eventText.text = currentEvent.description;
        choiceText1.text = currentEvent.choices[0].description + "\nCost: " + currentEvent.choices[0].cost;
        choiceText1.text += string.Format("\n\nHappiness: {0}", currentEvent.choices[0].happinessValue);
        choiceText2.text = currentEvent.choices[1].description + "\nCost: " + currentEvent.choices[1].cost;
        choiceText2.text += string.Format("\n\nHappiness: {0}", currentEvent.choices[1].happinessValue);
        choiceText3.text = currentEvent.choices[2].description + "\nCost: " + currentEvent.choices[2].cost;
        choiceText3.text += string.Format("\n\nHappiness: {0}", currentEvent.choices[2].happinessValue);
        effectBox.SetActive(false);

        EventMenu.SetActive(true);
    }

    public void HandleChoice(int choice)
    {
        Choice chc = currentEvent.choices[choice];
        effectBox.SetActive(true);
        Repercussion currentRepercussion = chc.repercussions[Random.Range(0, chc.repercussions.Length)];
        effectText.text = currentRepercussion.description + "\nCost: " + currentRepercussion.cost;
        effectText.text += string.Format("\n\nHappiness: {0}", currentRepercussion.happinessValue);
        UpdateBudget(chc.cost + currentRepercussion.cost);
        UpdateHappiness(chc.GetHappiness() + currentRepercussion.GetHappiness());
    }

    public void UpdateBudget(int pBudget)
    {
        budget = pBudget;
        budget = Mathf.Clamp(budget, 0, 100);
        // BudgetText.text = "Budget: " + budget + "/100";
        // BudgetSlider.value = budget / 100f;
    }
    public void UpdateHappiness(int pHappiness)
    {
        happiness = pHappiness;
        happiness = Mathf.Clamp(happiness, 0, 100);
        // HappinessText.text = "Happiness: " + happiness + "/100";
        // HappinessSlider.value = happiness / 100f;
    }
}
