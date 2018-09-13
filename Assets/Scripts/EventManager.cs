using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    private AllEvents _allEvents;
    private RandomEvent _currentEvent;

    public GameObject EventMenu;

    public Text eventText;

    public Text choiceText1;
    public Text choiceText2;
    public Text choiceText3;

    public GameObject effectBox;
    public Text effectText;

    private int _budget = 75;
    private int _happiness = 50;
    public Slider BudgetSlider;
    public Slider HappinessSlider;
    public Text BudgetText;
    public Text HappinessText;

    // Use this for initialization
    void Start()
    {
        TMX_Parser parser = new TMX_Parser();
        parser.Parse("Assets/XML/RandomEvents.txt", out _allEvents);
        Debug.Log(_allEvents);

        EventMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableRandomEvent()
    {
        _currentEvent = _allEvents.events[Random.Range(0, _allEvents.events.Length)];
        eventText.text = _currentEvent.description;
        choiceText1.text = _currentEvent.choices[0].description + "\nCost: " + _currentEvent.choices[0].cost;
        choiceText1.text += string.Format("\n\nHappiness: {0}", _currentEvent.choices[0].happinessValue);
        choiceText2.text = _currentEvent.choices[1].description + "\nCost: " + _currentEvent.choices[1].cost;
        choiceText2.text += string.Format("\n\nHappiness: {0}", _currentEvent.choices[1].happinessValue);
        choiceText3.text = _currentEvent.choices[2].description + "\nCost: " + _currentEvent.choices[2].cost;
        choiceText3.text += string.Format("\n\nHappiness: {0}", _currentEvent.choices[2].happinessValue);
        effectBox.SetActive(false);

        EventMenu.SetActive(true);
        choiceText1.GetComponentInParent<Button>().Select();

        GameInitializer.SetPaused(true);
    }

    public void HandleChoice(int choice)
    {
        Choice chc = _currentEvent.choices[choice];
        effectBox.SetActive(true);
        Repercussion currentRepercussion = chc.repercussions[Random.Range(0, chc.repercussions.Length)];
        effectText.text = currentRepercussion.description + "\nCost: " + currentRepercussion.cost;
        effectText.text += string.Format("\n\nHappiness: {0}", currentRepercussion.happinessValue);

        GameInitializer.GetBuildingHandler().GetCurrentCity().ReceiveCollection(-(chc.cost + currentRepercussion.cost), chc.GetHappiness() + currentRepercussion.GetHappiness());

    }

    public void EndEvent()
    {
        GameInitializer.SetPaused(false);
    }
}
