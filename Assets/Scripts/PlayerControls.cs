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

        // -- PASSIVE CHANGES --

        //abstract current linearVelocity for easy ref
        Vector2 currentLinearVelocity = this.gameObject.GetComponent<Rigidbody2D>().linearVelocity;
        //abstract current angular velocity for easy ref
        float currentangularVelocity = this.gameObject.GetComponent <Rigidbody2D>().angularVelocity;
        //abstract current rotation for easy ref
        float currentRotation = this.gameObject.GetComponent<Rigidbody2D>().rotation;
        //every frame, clamp linear velocity to max
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Mathf.Clamp(currentLinearVelocity.x, maxLinearVelocity * -1, maxLinearVelocity), Mathf.Clamp(currentLinearVelocity.y, 0, maxLinearVelocity));
        //every frame, clamp angular velocity between min (negative max) and max
        this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = Mathf.Clamp(currentangularVelocity, maxAngularVelocity * -1, maxAngularVelocity);
        //every frame, clamp rotation between min (negative max) and max
        this.gameObject.GetComponent<Rigidbody2D>().rotation = Mathf.Clamp(currentRotation, maxRotation * -1, maxRotation);
    }
    //this fct runs on press or release W
    void OnPressW()
    {
        //toggle holdingW
        holdingW = !holdingW;
        Debug.Log("Pressed W");
    }
    //this fct runs on press or release A
    void OnPressA()
    {
        //toggle holdingA
        holdingA = !holdingA;
        Debug.Log("Pressed A");
    }
    //this fct runs on press or release D
    void OnPressD()
    {
        //toggle holdingA
        holdingD = !holdingD;
        Debug.Log("Pressed D");
    }
}
