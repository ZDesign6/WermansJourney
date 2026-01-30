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
    void Update()
    {
        
    }
    //on press enter, advance the dialogue in the active dialogue engager, if there is one
    void OnAdvanceDialogue()
    {
        //check if not null
        if (activeDialogueEngager != null)
        {
            //then trigger the advance dialogue method of it
            activeDialogueEngager.advanceDialogue();
        }
    }
}
