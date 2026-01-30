using UnityEngine;
using UnityEngine.SceneManagement;

public class RouteAEndingBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //this fct fires on entering the collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //load RouteAEnding
        SceneManager.LoadScene("RouteAEnding");
    }
}
