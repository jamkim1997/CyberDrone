using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessCard : MonoBehaviour
{
   

   






    private void OnTriggerEnter2D(Collider2D collision)
    {

       
        if (collision.gameObject.name == "Character")
        {
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(transform.GetChild(0).gameObject);
            ExitChoice.SetCard(true);

            GetComponent<AudioSource>().Play();

            Destroy(gameObject,2f);
        }
    }
}


