using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeController : MonoBehaviour
{
    public bool hasTriggered;
    private Animator safeAnimator;
    public Animator exitAnimator;
    private Exit exit;

    private void Awake()
    {
        exit = FindObjectOfType<Exit>();
        safeAnimator = GetComponent<Animator>();
    }

    public void OpenSafe()
    {
        if(name == "SafeTut")
        {
            MissionUI.ClearText(1);
            exitAnimator.SetBool("Open", true);
        }

        if(!hasTriggered)
        {
            hasTriggered = true;
            safeAnimator.SetBool("Event_Animation", hasTriggered);
            exit.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
