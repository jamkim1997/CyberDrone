using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocController : MonoBehaviour
{
    public bool hasTriggered;
   
    public Animator animator;
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;
    public GameObject Doc;
    public GameObject Doc1;
    public GameObject Doc2;
    public GameObject Doc3;
   


    public void OpenDocBox()
    {
        if (!hasTriggered)
        {
            hasTriggered = true;
            Debug.Log("DocBox is now open...");
            animator.SetBool("Event_Animation", hasTriggered);
            animator1.SetBool("Event_Animation", hasTriggered);
            animator2.SetBool("Event_Animation", hasTriggered);
            animator3.SetBool("Event_Animation", hasTriggered);
            Doc.SetActive(true);
            Doc1.SetActive(true);
            Doc2.SetActive(true);
            Doc3.SetActive(true);
        }
    }
   


   
}
