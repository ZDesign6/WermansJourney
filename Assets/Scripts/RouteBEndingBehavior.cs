using UnityEngine;
using UnityEngine.SceneManagement;

public class RouteBEndingBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //this fct fires when entering the collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("something entered Route B endzone");
        //load route B ending
        SceneManager.LoadScene("RouteBEnding");
    }
}
