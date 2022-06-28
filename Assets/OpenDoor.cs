using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private AudioSource audioSource;
    public bool AnimationSkip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Character")
        {
            if (audioSource)
            {
                audioSource.Play();
            }
            GetComponent<Animator>().enabled = true;
        }
    }

    public void OpentheGate()
    {
        if(AnimationSkip)
        {
            return;
        }
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach(BoxCollider2D collider in colliders)
        {
           
            Destroy(collider);
        }
        
        Destroy(this);
    }
}
