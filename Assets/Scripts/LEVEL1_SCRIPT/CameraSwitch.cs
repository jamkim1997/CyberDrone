using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    private bool hasTriggered;
    public GameObject StopCamera;

    public void offCamera()
    {
        if (!hasTriggered)
        {
            hasTriggered = true;

            StopCamera.GetComponentInChildren<FieldOfView>().gameObject.SetActive(false);
            StopCamera.GetComponent<Animator>().enabled = false;
            StopCamera.GetComponent<SurveillanceCamera>().enabled = false;
            MissionUI.ClearText(2);
        }
    }

    

}
