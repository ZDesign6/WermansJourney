using UnityEngine;

public class GameManagerBehavior : MonoBehaviour
{
    //the container for the first instance of this script
    public static GameManagerBehavior singleton;

    // -- DIALOGUE --

    //tracks if we are inDialogue
    public bool inDialogue = false;
    //tracks the active DialogueEngager
    public DialogueEngager activeDialogueEngager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // -- SINGLETON ASSIGNMENT --

        //on start, if there is not already a singleton
        if (singleton == null)
        {
            //store this instance of the Game Manger script as the singleton
            singleton = this;
        }
        //else, if there is already a singleton AND it is NOT this instance of the script
        else if (singleton != this)
        {
            //Then this object is not the singleton. destroy this game object.
            Destroy(this.gameObject);
        }
        //finally, if we have made it here without being destroyed, this object is our singleton. Mark it as Don't Destroy.
        DontDestroyOnLoad(this.gameObject);

        // -- MISC --

        //attempt to run at 60 fps
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if there is an activeDialogueEngager...
        if (activeDialogueEngager != null) 
        {
            //reduce its fadeTimer by one as long as it is not 0
            if (activeDialogueEngager.fadeTimer > 0)
            {
                activeDialogueEngager.fadeTimer = activeDialogueEngager.fadeTimer - 1;
            }

            //if its BGFadingIn is true...
            if (activeDialogueEngager.BGFadingIn == true)
            {
                //fade in the BG until it reaches alpha 255
                activeDialogueEngager.fadeInBG();
            }
            else if (activeDialogueEngager.TextFadingIn == true)
            {
                //call the textFadeOutfct
                activeDialogueEngager.fadeInCurrentTextObj();
            }
            else if (activeDialogueEngager.FadingOut == true)
            {
                activeDialogueEngager.fadeOut();
            }
                
        }
    }
}
