using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{

    public bool isOpen;
    //public GameObject door;
    //public Sprite openedDoor;

    void Start()
    {
        isOpen = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            //Loading level with build index
            SceneManager.LoadScene("Game_Over");
        }
    }

    public void openDoor()
    {
        isOpen = true;
        //door.gameObject.GetComponent<SpriteRenderer>().sprite = openedDoor;
        //this.gameObject.GetComponent<SpriteRenderer>().sprite = openedDoor;
    }
}
