using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    public GameObject player;
    //bool invisibility_active; 
    //Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        //invisibility_active = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //invisibility_active = true; 
            //pos = GameObject.FindGameObjectWithTag("Player").transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            player.GetComponent<Player>().BecomeInvisible();
             
        }
        if(Input.GetKeyUp(KeyCode.Space)) 
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<Player>().BecomeVisible();
        }
    }
}
