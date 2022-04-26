using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public UnityEvent doorOpenEvent;
    public GameObject door;

    bool isOpened = false;


    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
                Debug.Log("Key was pressed------!!!");
                doorOpenEvent.Invoke();
                FindObjectOfType<Audio_Manager>().Play("DoorOpening");
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player in range");
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
        if(collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player is not in range");
        }
    }
}
