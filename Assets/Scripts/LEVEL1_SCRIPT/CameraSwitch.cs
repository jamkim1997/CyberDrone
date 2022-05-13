using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public bool hasTriggered;
    public GameObject StopCamera;
   
   


    public void offCamera()
    {
        if (!hasTriggered)
        {
            hasTriggered = true;
            Debug.Log("camea is now off");


            StopCamera.GetComponentInChildren<FieldOfView>().gameObject.SetActive(false);
            StopCamera.GetComponent<Animator>().enabled = false;
            StopCamera.GetComponent<SurveillanceCamera>().enabled = false;

           
            
        }
    }

    

}
