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
    public GameObject BGObj;
    //list of all UI DialogueTextObjs that should be triggered. assigned in-editor.
    public List<GameObject> dialogueTextObjs = new List<GameObject>();
    //the max possible alpha value. Used to clamp alpha before assignment and used to check if we are done lerping
    public float maxAlpha = .9f;
    //how long it should take in frames for bg to fade in
    public float BGFadeInTime = 180;
    //how long it should take for each text box to fade in
    public float TextFadeInTime = 240;
    //how long it takes for everything to fade out
    public float FadeOutTime = 180;
    //tracks fading progress
    public float fadeTimer = 0;
    //tracks our fading states
    public bool BGFadingIn = false;
    public bool TextFadingIn = false;
    public bool FadingOut = false;
   
    //acts as a public Index for iterating over the DialogueObjs List. Starts at 0, reset by functions.
    int currentTextObjIndex = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to game manager
        gameManager = GameManagerBehavior.singleton;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    //collision function
    private void OnTriggerEnter2D (Collider2D collider)
    {
        //if the thing entering is the PLayer
        if (collider.gameObject.name == "Player")
        {
            //set BG fade in to true
            BGFadingIn = true;
            //and set fadeTimer equal to BG fadeTime
            fadeTimer = BGFadeInTime;
            //and set self to activeDialogueManager
            gameManager.activeDialogueEngager = this;
        }

    }
    //this fct runs as soon as a collider that was overlapping leaves 
    private void OnTriggerExit2D (Collider2D collider)
    {
        //set all things alpha to 0
        BGObj.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        for (int textObjIndex = 0; textObjIndex < dialogueTextObjs.Count; textObjIndex++)
        {
            //set each text alpha to 0
            dialogueTextObjs[textObjIndex].GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0);
        }
        //and unassign self as activeDialogueEngager
        gameManager.activeDialogueEngager = null;
    }
    //this fct assigns the BG obj's alpha equal to (BGFadeInTime - fadeTimer) / BGFadeInTime. Once BG alpha reaches max, switches boolean states
    public void fadeInBG()
    {
        //abstract percentage of time elapsed
        float alphaToAssign = (BGFadeInTime - fadeTimer) / BGFadeInTime;
        //clamp alpha to assign between 0 and maxAlpha
        alphaToAssign = Mathf.Clamp(alphaToAssign, 0, maxAlpha);
        //abstract current color for easy ref
        Color currentColor = BGObj.GetComponent<Image>().color;
        //assign new alpha
        BGObj.GetComponent<Image>().color = new Color(currentColor.r, currentColor.g, currentColor.b, alphaToAssign);

        //finally, if BGObj's alpha has reached 255...
        if (BGObj.GetComponent<Image>().color.a == maxAlpha)
        {
            //toggle off BGFadingIn
            BGFadingIn = false;
            //and toggle on textFadingIn
            TextFadingIn = true;
            //and set fadeTimer equal to TextFadeInTime 
            fadeTimer = TextFadeInTime;
        }
    }
    /*this fct gradually increases the alpha of the TextObj of the given index. Once that Obj's alpha reaches 255, it increases the index. 
     * Once the final TextObj's alpha reaches 255, it flips off TextFadingIn and flips on FadingOut */
    public void fadeInCurrentTextObj()
    {
        //abstract percentage of time elapse. Starts at 0, ends at 1.
        float  alphaToAssign = (TextFadeInTime - fadeTimer) / TextFadeInTime;
        //clamp alpha to assign between 0 and maxAlpha
        alphaToAssign = Mathf.Clamp(alphaToAssign, 0, maxAlpha);
        //abstract current color for easy ref
        Color currentColor = dialogueTextObjs[currentTextObjIndex].GetComponent<TextMeshProUGUI>().color;
        //assign new alpha
        dialogueTextObjs[currentTextObjIndex].GetComponent<TextMeshProUGUI>().color = new Color(currentColor.r, currentColor.g, currentColor.b, alphaToAssign);

        //finally, if the current TextObj has reached max alpha...
        if (dialogueTextObjs[currentTextObjIndex].GetComponent<TextMeshProUGUI>().color.a == maxAlpha)
        {
            //and the current TextObj is not the last text obj
            if (currentTextObjIndex != dialogueTextObjs.Count - 1)
            {
                //then increase the currentTextObjIndex to move on to the next Text Obj
                currentTextObjIndex = currentTextObjIndex + 1;
                fadeTimer = TextFadeInTime;
            }
            //else if this was the last text obj...
            else
            {
                //then flip off textFadingIn
                TextFadingIn = false;
                //flip on FadingOut
                FadingOut = true;
                //and set the fadeTimer equal to FadeOutTime to prep for fade out
                fadeTimer = FadeOutTime;
            }
        }
    }
    //this fct is responsible for uniformly fading out all the BG and all text OBJs. Once all have reached alpha 0, it then unassigns the activeDialogueEngager from the singleton
    public void fadeOut()
    {
        //abstract BG color for easy ref
        Color currentBGColor = BGObj.GetComponent<Image>().color;
        //abstract Text colors for easy ref
        Color currentTextColors = dialogueTextObjs[currentTextObjIndex].GetComponent<TextMeshProUGUI>().color;
        //abstract percentage of time elapsed
        float alphaToAssign = (FadeOutTime - fadeTimer) / FadeOutTime;
        //clamp alpha to assign between 0 and max
        alphaToAssign = Mathf.Clamp(alphaToAssign, 0, maxAlpha);

        //assign new aplhas 
        BGObj.GetComponent<Image>().color = new Color(currentBGColor.r,currentBGColor.g,currentBGColor.g, .75f - alphaToAssign);
        //iterate over all Text Objs to assign their alphas
        for (int textObjIndex = 0; textObjIndex < dialogueTextObjs.Count; textObjIndex = textObjIndex + 1)
        {
            //assign new alha to the currentTextObj in the array
            dialogueTextObjs[textObjIndex].GetComponent<TextMeshProUGUI>().color = new Color(currentTextColors.r, currentTextColors.g, currentTextColors.b, .75f - alphaToAssign);
        }

        //finally, once either alpha reaches 0 (they should be synced), unassign the activeDialogueManager in trhe gameManager
        if (BGObj.GetComponent<Image>().color.a == 0)
        {
            gameManager.activeDialogueEngager = null;
        }
    }

}
