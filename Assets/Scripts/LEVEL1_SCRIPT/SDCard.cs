using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDCard : MonoBehaviour
{
    public Animator secoondGreenDoor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Character")
        {
            MissionUI.ClearText(1);
            secoondGreenDoor.enabled = true;
            Destroy(gameObject);
        }
    }
}
