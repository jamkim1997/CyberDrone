using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessCard : MonoBehaviour
{
    public Animator secoondGreenDoor;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            ExitChoice.SetCard(true);
           
            Destroy(gameObject);
        }
    }
}
