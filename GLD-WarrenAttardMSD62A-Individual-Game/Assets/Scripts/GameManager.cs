using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Unity Setup")]
    public GameObject CityGate;
    public int PassageCost = 15000;

    [Header("UI")]
    public GameObject Objective;

    private void Start()
    {
        GameData.PassageToCityCost = PassageCost;

        StartCoroutine(WaitForSeconds(20, UpdateObjectiveText));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.AccessToCity == true)
        {
            Debug.Log("Access Granted!");
            Destroy(CityGate);
        }

        if(GameData.NPCFound == true)
        {
            UpdateObjectiveText();
        }
    }

    private void UpdateObjectiveText()
    {
        Text ObjectiveText = Objective.GetComponent<Text>();

        Objective.SetActive(true);

        if(GameData.NPCFound == false)
        {
            ObjectiveText.text = "Find and talk to the Gate Keeper.";
        }
        else
        {
            ObjectiveText.text = "Open the City Gate. ($" + GameData.PassageToCityCost + ")";
        }
    }

    private IEnumerator WaitForSeconds(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
}
