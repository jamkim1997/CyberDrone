using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safes : MonoBehaviour
{
    private int safe;

    public void OpenSafe(int target)
    {
        safe++;

        transform.GetChild(target).GetComponent<Animator>().SetBool("Event_Animation", true);

        if(safe == 4)
        {
            Exit[] exits = FindObjectsOfType<Exit>();
            for(int i = 0; i < exits.Length; i++)
            {
                exits[i].GetComponent<Animator>().SetBool("Open", true);
                exits[i].GetComponent<BoxCollider2D>().enabled = true;
            }
            MissionUI.ClearText(1);
            MissionUI.ClearText(2);
            Destroy(this);
        } 
    }
}
