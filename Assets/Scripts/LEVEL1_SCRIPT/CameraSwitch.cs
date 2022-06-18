using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    private bool hasTriggered;
    public GameObject StopCamera;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void offCamera()
    {
        if (!hasTriggered)
        {
            hasTriggered = true;
            spriteRenderer.flipY = true;
            StopCamera.GetComponentInChildren<FieldOfView>().gameObject.SetActive(false);
            StopCamera.GetComponent<SurveillanceCamera>().enabled = false;
            MissionUI.ClearText(2);
            Destroy(this);
        }
    }

    

}
