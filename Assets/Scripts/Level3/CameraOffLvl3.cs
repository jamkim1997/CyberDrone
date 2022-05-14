using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraOffLvl3 : MonoBehaviour
{

    public GameObject terminal;
    public GameObject camera;
    public UnityEvent interactAction;
    bool isActive;
    public bool isInRange;
    public AudioSource cameraOffSound;
    
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
                cameraOffSound.Play();
                interactAction.Invoke();
                isActive = true;
                Debug.Log("Terminal Activated");
                camera.GetComponent<SurveillanceCamera>().changeViewDistance(0);
                
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
        /*
        if (!isOpened)
        {
            isOpened = true;
            door.transform.position = new Vector3 (16.5f, 12.5f, 0);
        }*/
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