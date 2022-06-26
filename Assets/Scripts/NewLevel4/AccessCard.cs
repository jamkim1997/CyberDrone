using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessCard : MonoBehaviour
{
    public AudioSource audioSource;
   

   






    private void OnTriggerEnter2D(Collider2D collision)
    {

       
        if (collision.gameObject.name == "Character")
        {
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<BoxCollider2D>());
            audioSource = GetComponent<AudioSource>();
            
            ExitChoice.SetCard(true);

            audioSource.Play();

            Destroy(gameObject,2f);
        }
    }
}


