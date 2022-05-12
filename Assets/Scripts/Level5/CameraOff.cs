using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraOff : MonoBehaviour
{

    public GameObject terminal;
    public GameObject camera;
    public UnityEvent interactAction;
    GameObject[] doorScripts;
    GameObject[] doors;
    //bool isActive;
    public bool isInRange;
    public Sprite openedDoor;
    public AudioSource clickSound;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                clickSound.Play(); 
                doorScripts = GameObject.FindGameObjectsWithTag("DoorScript");
                doors = GameObject.FindGameObjectsWithTag("Door");
                interactAction.Invoke();
                //isActive = true;
                Debug.Log("Terminal Activated");
                camera.GetComponent<SurveillanceCamera>().changeViewDistance(0);
                // a foreach loop to enable all doors
                foreach (GameObject s in doorScripts)
                {
                    s.GetComponent<DoorController>().openDoor();
                }
                foreach (GameObject d in doors)
                {
                    d.GetComponent<SpriteRenderer>().sprite = openedDoor;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player near terminal");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player not near terminal");
        }
    }
}
