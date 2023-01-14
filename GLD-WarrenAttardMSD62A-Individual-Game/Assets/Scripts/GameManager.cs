using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class GameManager : MonoBehaviour
{
    [Header("Unity Setup")]
    public GameObject CityGate;
    public int PassageCost = 15000;
    public GameObject Player;

    private string baseURI = "http://WarrenAtt.pythonanywhere.com";

    [Header("UI")]
    public GameObject Objective;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

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

    [ContextMenu("Load Data")]
    public void LoadData()
    {
        print("Getting data from the server");


        RestClient.Get(baseURI + "/api/getMoney").Then(money =>
        {
            GameData.Money = Int32.Parse(money.Text);
        }).Catch(err =>
        {
            print(err.Message);
        });

        RestClient.GetArray<PlayerPosition>(baseURI + "/api/getLocation").Then(location =>
        {

            Player.GetComponent<Invector.vCharacterController.vThirdPersonInput>().RootMotion = false;
            foreach (PlayerPosition position in location)
            {
                Debug.Log("Position X: " + position.positionX + "Position Y: " + position.positionY + "Position Z: " + position.positionZ);
                Vector3 playerPos = new Vector3(position.positionX, position.positionY + 1, position.positionZ);
                GameData.PlayerPos = playerPos;
                Player.transform.position = playerPos;
                StartCoroutine(AciveInput());
            }
        }).Catch(err =>
        {
            print(err.Message);
        });

        RestClient.Get(baseURI + "/api/getInventory").Then(inventory =>
        {
            Debug.Log(JsonConvert.DeserializeObject(inventory.Text));

        }).Catch(err =>
        {
            print(err.Message);
        });
    }

    [ContextMenu("Save Data")]
    public void SaveData()
    {
        print("Saving data on server");

        int playerMoney = GameData.Money;

        List<PlayerPosition> playerPositions = new List<PlayerPosition>();

        List<string> playerInvetoryList = new List<string>();
        string[] playerInvetory = null;

        if(Player != null)
        {
            PlayerPosition position = new PlayerPosition();
            position.positionX = Player.transform.position.x;
            position.positionY = Player.transform.position.y;
            position.positionZ = Player.transform.position.z;

            playerPositions.Add(position);
        }

        if(GameData.PlayerInvetory != null)
        {
            foreach(Item item in GameData.PlayerInvetory)
            {
                playerInvetoryList.Add(JsonUtility.ToJson(item));
                playerInvetory = playerInvetoryList.ToArray();

            }
        }

        string jsonPlayerPos = JsonConvert.SerializeObject(playerPositions);
        string jsonPlayerMoney = JsonConvert.SerializeObject(playerMoney);
        string jsonPlayerInventory = JsonConvert.SerializeObject(playerInvetory);

        RestClient.Post(baseURI + "/api/saveMoney", jsonPlayerMoney).Catch(error =>
        {
            var err = error as RequestException;
            print(err.StatusCode);
            print(err.Response);
            print(err.Message);
        });

        RestClient.Post(baseURI + "/api/savePosition", jsonPlayerPos).Catch(error =>
        {
            var err = error as RequestException;
            print(err.StatusCode);
            print(err.Response);
            print(err.Message);
        });

        RestClient.Post(baseURI + "/api/saveInventory", jsonPlayerInventory).Catch(error =>
        {
            var err = error as RequestException;
            print(err.StatusCode);
            print(err.Response);
            print(err.Message);
        });
    }

    private IEnumerator AciveInput()
    {
        yield return new WaitForSeconds(2f);
        Player.GetComponent<Invector.vCharacterController.vThirdPersonInput>().RootMotion = true;
    }

    private IEnumerator WaitForSeconds(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
}
