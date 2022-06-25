using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxConrollerLevel4 : MonoBehaviour
{
    public bool hasTriggered;
    public Animator animator;
    public GameObject SDCard;


    public void OpenSafe()
    {
        if (!hasTriggered)
        {
           
            hasTriggered = true;
            animator.SetBool("Event_Animation", hasTriggered);
            SDCard.SetActive(true);

        }
    }
}
