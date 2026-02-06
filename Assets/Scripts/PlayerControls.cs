using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // -- CONTROL-RELATED --

    //tracks if the player is currently holding W
    bool holdingW = false;
    //tracks if the player is curerntly holding A
    bool holdingA = false;
    //tracks if the player is curerntly holding D
    bool holdingD = false;

    // -- MOVEMENT-RELATED --

    //the degree of force applied every during forward movement
    public float moveForce = 3f;
    //the degree of rotational force applied during rotations
    public float rotationalForce = 3f;
    //maximum local linear velocity
    public float maxLinearVelocity = 1f;
    //maximum angular velocity
    public float maxAngularVelocity = 1f;
    //maximum possible rotation (positive or negative)
    float maxRotation = 90;
    public float friction = .01f;

    // -- Sound-Related --
    public int chooseSound;
    public AudioSource GravelSnd1;
    public AudioSource GravelSnd2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // -- INPUT --

        //every frame, if holding W, we want positive movement along the LOCAL Y axis
        if (holdingW == true)
        {
            //add positive force of moveForce on the local Y
            this.gameObject.GetComponent<Rigidbody2D>().AddRelativeForceY(moveForce);
        }
        //every frame, if holding A, we want to rotate positively around the Z
        if (holdingA == true) 
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddTorque(rotationalForce);
        }
        //every frame, if holding D, we want to rotate negatively around the Z
        if (holdingD == true)
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddTorque(rotationalForce * -1);
        }

        // -- ABSTRACTION -- (abstract properties out of the rigidbody so we can work with them more easily. They will then be re-assigned as a final step.)

        //abstract current linearVelocity out
        Vector2 workingLinearVelocity = this.gameObject.GetComponent<Rigidbody2D>().linearVelocity;
        //abstract current angular velocity out
        float workingAngularVelocity = this.gameObject.GetComponent<Rigidbody2D>().angularVelocity;
        //abstract current rotation out
        float workingRotation = this.gameObject.GetComponent<Rigidbody2D>().rotation;

        // -- PASSIVE CHANGES --

        //every frame, apply friction to linear velocity. any over-application will be corrected later.
        //friction must be applied differently depending on if velocities are positive or negative, to bring them towards 0. 

        //if x vel is pos, subtract friction to bring towards 0
        if (workingLinearVelocity.x >= 0f)
        {
            workingLinearVelocity = new Vector2(workingLinearVelocity.x - friction, workingLinearVelocity.y);
        }
        //else if x vel is neg, add friction to bring it towards 0
        else if (workingLinearVelocity.x < 0)
        {
            workingLinearVelocity = new Vector2(workingLinearVelocity.x + friction, workingLinearVelocity.y);
        }
        //if y vel is pos, subtract friction to bring it towards 0
        if (workingLinearVelocity.y >= 0f)
        {
            workingLinearVelocity = new Vector2(workingLinearVelocity.x, workingLinearVelocity.y - friction);
        }
        //else if y vel is negative, add friction to bring it towards 0
        else if(workingLinearVelocity.y < 0)
        {
            workingLinearVelocity = new Vector2(workingLinearVelocity.x, workingLinearVelocity.y + friction);
        }

        // -- CORRECTIONS -- (ensure working values conform to mins and maxes)

        //every frame, clamp x velocity between neg and pos max, and clamp y velocity between 0 and max
        workingLinearVelocity = new Vector2(Mathf.Clamp(workingLinearVelocity.x, maxLinearVelocity * -1, maxLinearVelocity), Mathf.Clamp(workingLinearVelocity.y, 0, maxLinearVelocity));
        //every frame, clamp angular velocity between min (negative max) and max
        workingAngularVelocity = Mathf.Clamp(workingAngularVelocity, maxAngularVelocity * -1, maxAngularVelocity);
        //every frame, clamp rotation between min (negative max) and max
        workingRotation = Mathf.Clamp(workingRotation, maxRotation * -1, maxRotation);
        //Debug.Log("current linear velocity is " + this.gameObject.GetComponent<Rigidbody2D>().linearVelocity);

        // -- RE-ASSIGNMENT -- (assign all working values back into the rigidbody)

        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = workingLinearVelocity;
        this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = workingAngularVelocity;
        this.gameObject.GetComponent<Rigidbody2D>().rotation = workingRotation;
    }
    //this fct runs on press or release W
    void OnPressW()
    {
        //toggle holdingW
        holdingW = !holdingW;
        //Debug.Log("Pressed W");
        
        // -- Sound-Picker--
        chooseSound = Random.Range(1,3);
        if (chooseSound == 1)
        {
            GravelSnd1.Play(0);
            Debug.Log("one");
        }
        if (chooseSound == 2)
        {
            GravelSnd2.Play(0);
            Debug.Log("2");
        }
    }
    //this fct runs on press or release A
    void OnPressA()
    {
        //toggle holdingA
        holdingA = !holdingA;
        //Debug.Log("Pressed A");
    }
    //this fct runs on press or release D
    void OnPressD()
    {
        //toggle holdingA
        holdingD = !holdingD;
        //Debug.Log("Pressed D");
    }
}
