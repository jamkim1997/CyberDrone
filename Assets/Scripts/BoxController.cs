using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public bool hasTriggered;
    public Animator animator;
    public GameObject SDCard;
    

    public void OpenSafe()
    {
        if(!hasTriggered)
        {
            hasTriggered = true;
            Debug.Log("Safe is now open...");
            animator.SetBool("Event_Animation", hasTriggered);
            SDCard.SetActive(true);
        }
    }
}
