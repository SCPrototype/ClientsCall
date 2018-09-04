using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

    private AllEvents allEvents;
    private RandomEvent currentEvent;

    public Text eventText;

    public Text choiceText1;
    public Text choiceText2;
    public Text choiceText3;

    public GameObject effectBox;
    public Text effectText;

	// Use this for initialization
	void Start () {
        TMX_Parser parser = new TMX_Parser();
        parser.Parse("Assets/XML/RandomEvents.txt", out allEvents);
        Debug.Log(allEvents);
        EnableRandomEvent();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnableRandomEvent();
        }
    }

    public void EnableRandomEvent()
    {
        currentEvent = allEvents.events[Random.Range(0, allEvents.events.Length)];
        eventText.text = currentEvent.description;
        choiceText1.text = currentEvent.choices[0].description + "\tCost: " + currentEvent.choices[0].cost;
        choiceText1.text += string.Format("\n\nPollution: {0}, Entertainment: {1}, Health: {2}, Population: {3}, Education: {4}", currentEvent.choices[0].pollutionValue, currentEvent.choices[0].entertainmentValue, currentEvent.choices[0].healthValue, currentEvent.choices[0].populationValue, currentEvent.choices[0].educationValue);
        choiceText2.text = currentEvent.choices[1].description + "\tCost: " + currentEvent.choices[1].cost;
        choiceText2.text += string.Format("\n\nPollution: {0}, Entertainment: {1}, Health: {2}, Population: {3}, Education: {4}", currentEvent.choices[1].pollutionValue, currentEvent.choices[1].entertainmentValue, currentEvent.choices[1].healthValue, currentEvent.choices[1].populationValue, currentEvent.choices[1].educationValue);
        choiceText3.text = currentEvent.choices[2].description + "\tCost: " + currentEvent.choices[2].cost;
        choiceText3.text += string.Format("\n\nPollution: {0}, Entertainment: {1}, Health: {2}, Population: {3}, Education: {4}", currentEvent.choices[2].pollutionValue, currentEvent.choices[2].entertainmentValue, currentEvent.choices[2].healthValue, currentEvent.choices[2].populationValue, currentEvent.choices[2].educationValue);
        effectBox.SetActive(false);
    }

    public void HandleChoice(int choice)
    {
        Choice chc = currentEvent.choices[choice];
        effectBox.SetActive(true);
        Repercussion currentRepercussion = chc.repercussions[Random.Range(0, chc.repercussions.Length)];
        effectText.text = currentRepercussion.description;
        effectText.text += string.Format("\n\nPollution: {0}, Entertainment: {1}, Health: {2}, Population: {3}, Education: {4}", currentRepercussion.pollutionValue, currentRepercussion.entertainmentValue, currentRepercussion.healthValue, currentRepercussion.populationValue, currentRepercussion.educationValue);
    }
}
