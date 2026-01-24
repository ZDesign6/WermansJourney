using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // -- REFERENCES --

    //ref to Player
    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to player
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //abstract playerPos for easy ref
        Vector3 playerPos = player.GetComponent<Transform>().position;
        //every frame, update x and y pos to match player
        this.gameObject.GetComponent<Transform>().position = new Vector3(playerPos.x, playerPos.y, this.gameObject.GetComponent<Transform>().position.z);
    }
}
