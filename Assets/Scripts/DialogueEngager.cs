using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEngager : MonoBehaviour
{
    /* THIS SCRIPT IS RESPONSIBLE FOR ENGAGING WITH THE SPECIFIC DIAOGUES RELEVENT TO THE OBSTACLE IT IS ATTACHED TO.
     * IT HOOKS ITSELF UP TO THE REST OF THE GAME THROUGH THE GAME MANAGER AFTER A COLLISION */

    // -- REFERENCES --

    //ref to game manager
    GameManagerBehavior gameManager;

    // -- DIALOGUE --

    //list of all UI DialogueBGs that should be triggered. assigned in-editor.
    public List<GameObject> dialogueBGObjs = new List<GameObject>();
    //list of all UI DialogueTextObjs that should be triggered. assigned in-editor.
    public List<GameObject> dialogueTextObjs = new List<GameObject>();

    //acts as a public Index for iterating over the DialogueObjs List. Starts at 0, reset by advanceDialogue.
    int currentDialogueObjIndex = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to game manager
        gameManager = GameManagerBehavior.singleton;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("current gameManager is " +  gameManager.name);
    }

    //collision function
    private void OnTriggerEnter2D (Collider2D collider)
    {
        //check if the collision was with the player
        if (collider.gameObject.name == "Player")
        {
            //flip inDialogue to true
            gameManager.inDialogue = true;
            //store self as the active DialogueEngager
            gameManager.activeDialogueEngager = this;

            //show the first dialogue and text
            showDialogue(currentDialogueObjIndex);
        }
    }
    //hides the current dialogueBGObj and dialogueTextObj, increments the Index, then shows the next. Also wraps up dialogue if we are on the last Index.
    public void advanceDialogue()
    {
        
        //if the currentIndex is the final Index (Count - 1)...
        if (currentDialogueObjIndex == dialogueBGObjs.Count - 1)
        {
            //hide the dialogueBox
            hideDialogue(currentDialogueObjIndex);
        }
        //else, increment and show the next dialogue
        else
        {
            //hide text, frame, and BG for current Dialogue
            hideDialogue(currentDialogueObjIndex);
            //increment the dialogueObj Index
            currentDialogueObjIndex = currentDialogueObjIndex + 1;
            //show the text and BG for "next" dialogue
            showDialogue(currentDialogueObjIndex);
        }

    }
    //"Shows" the dialogueBGObj and dialogueTextObj at the given index from their Lists. Right now this means activating their Renderer Components.
    void showDialogue(int dialogueIndex)
    {
        //show the current dialogue BG 
        dialogueBGObjs[currentDialogueObjIndex].GetComponent<Image>().enabled = true;
        //show the current dialogue Text
        dialogueTextObjs[currentDialogueObjIndex].GetComponent<TextMeshProUGUI>().enabled = true;
    }
    //"Hides" the dialogueBGObj and DialogueTextObj at the given index from their Lists. Right now this means de-activating their Renderer Components.
    void hideDialogue(int dialogueIndex)
    {
        //hide the current dialogue BG
        dialogueBGObjs[currentDialogueObjIndex].GetComponent<Image>().enabled = false;
        //hidethe current dialogue Text
        dialogueTextObjs[currentDialogueObjIndex].GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
