using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Unity Setup")]
    public NPC npc;

    private bool isTalking = false;

    private float distance;
    public float curResponseTracker = 0;

    public GameObject player;
    public GameObject dialogueUI;

    [Header("UI")]
    public Text npcName;
    public Text npcDialogueBox;
    public Text playerResponse;

    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false);
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);

        if(distance <= 2.5f)
        {
            GameData.NPCFound = true;

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                curResponseTracker++;
                if(curResponseTracker >= npc.dialogue.Length - 1)
                {
                    curResponseTracker = npc.dialogue.Length - 1;
                }
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                curResponseTracker--;
                if (curResponseTracker < 0)
                {
                    curResponseTracker = 0;
                }
            }

            //trigger dialogue
            if (Input.GetKeyDown(KeyCode.E) && isTalking == false)
            {
                StartConversation();
            }
            else if (Input.GetKeyDown(KeyCode.E) && isTalking == true)
            {
                EndDialogue();
            }

            if(curResponseTracker == 0 && npc.playerDialogue.Length >= 0)
            {
                playerResponse.text = npc.playerDialogue[0];
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //How to make money dialogue
                    npcDialogueBox.text = npc.dialogue[1];
                }
            }
            else if(curResponseTracker == 1 && npc.playerDialogue.Length >= 1)
            {
                playerResponse.text = npc.playerDialogue[1];
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if(GameData.Money >= 15000f)
                    {
                        //Open gate dialogue
                        npcDialogueBox.text = npc.dialogue[2];

                        GameData.Money -= 15000;
                        GameData.AccessToCity = true;
                    }
                    else
                    {
                        npcDialogueBox.text = npc.dialogue[3];
                    }
                    
                }
            }
            else if (curResponseTracker == 2 && npc.playerDialogue.Length >= 2)
            {
                playerResponse.text = npc.playerDialogue[2];
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //Goodbye dialogue
                    npcDialogueBox.text = npc.dialogue[4];
                }
            }
        }
        else
        {
            EndDialogue();
        }
    }

    private void StartConversation()
    {
        isTalking = true;
        curResponseTracker = 0;
        dialogueUI.SetActive(true);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];
    }

    private void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
    }
}
