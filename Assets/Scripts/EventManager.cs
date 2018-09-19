using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    private AllEvents _allEvents;
    private RandomEvent _currentEvent;
    private int _eventIndex = 0;

    public GameObject EventMenu;

    public Text eventText;

    public Text choiceText1;
    public Text choiceText2;
    public Text choiceText3;

    public GameObject effectBox;
    public Text effectText;

    public Slider BudgetSlider;
    public Slider HappinessSlider;
    public Text BudgetText;
    public Text HappinessText;
    private SoundHandler _soundHandler;

    // Use this for initialization
    void Start()
    {
        TMX_Parser parser = new TMX_Parser();
        parser.Parse("Assets/XML/RandomEvents.txt", out _allEvents);
        Debug.Log(_allEvents);
        _soundHandler = GameInitializer.GetSoundHandler();
        EventMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayConfirmSound()
    {
        _soundHandler.PlaySound(SoundHandler.Sounds.CONFIRM);
    }

    public void EnableRandomEvent()
    {
        if (_eventIndex >= _allEvents.events.Length)
        {
            return;
        }
        _currentEvent = _allEvents.events[_eventIndex];
        _eventIndex++;
        eventText.text = _currentEvent.description;
        choiceText1.text = _currentEvent.choices[0].description + "\nCost: " + _currentEvent.choices[0].cost;
        choiceText2.text = _currentEvent.choices[1].description + "\nCost: " + _currentEvent.choices[1].cost;
        choiceText3.text = _currentEvent.choices[2].description + "\nCost: " + _currentEvent.choices[2].cost;
        effectBox.SetActive(false);

        EventMenu.SetActive(true);
        choiceText1.GetComponentInParent<Button>().Select();

        GameInitializer.SetPaused(true);
    }

    public void HandleChoice(int choice)
    {
        Choice chc = _currentEvent.choices[choice];
        effectBox.SetActive(true);
        string repercussionCostText = chc.repercussion.cost.ToString();
        if (chc.repercussion.cost < 0)
        {
            repercussionCostText = "+" + (-chc.repercussion.cost);
        }
        effectText.text = chc.repercussion.description + "\nCost: " + repercussionCostText;

        GameInitializer.GetBuildingHandler().GetCurrentCity().ReceiveCollection(-(chc.cost + chc.repercussion.cost));

        GameInitializer.AddAchieverScore(chc.achieverValue);
        GameInitializer.AddExplorerScore(chc.explorerValue);
        GameInitializer.AddKillerScore(chc.killerValue);
        GameInitializer.AddSocializerScore(chc.socializerValue);

    }

    public void EndEvent()
    {
        GameInitializer.SetPaused(false);
    }
}
