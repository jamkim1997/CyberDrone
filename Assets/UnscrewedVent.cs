using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscrewedVent : MonoBehaviour

   
{
    public Animator vent;
   

    private void openDoor()
    {
        if (Input.GetKey(KeyCode.E)) {
            vent.enabled = true;

        }   

    }
}
