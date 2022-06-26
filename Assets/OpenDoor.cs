using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Character")
        {
            GetComponent<Animator>().enabled = true;
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void OpentheGate()
    {
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach(BoxCollider2D collider in colliders)
        {
           
            Destroy(collider);
        }
        audioSource.Play();
        Destroy(this);
    }
}
