using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskController : MonoBehaviour
{
    public bool hasTriggered;
    //public Animator animator;

    public void OpenComputer()
    {
        if(!hasTriggered)
        {
            hasTriggered = true;
            Debug.Log("Safe is now open...");
            //animator.SetBool("Event_Animation", hasTriggered);
            
        }
    }
}

