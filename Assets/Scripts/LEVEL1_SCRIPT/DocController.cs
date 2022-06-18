using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocController : MonoBehaviour
{
    private bool hasTriggered;
    private CameraMovement cameraMovement;
    private int gainedDocs;
    public Transform minimapIcon;
    
    private void Awake()
    {
        cameraMovement = FindObjectOfType<CameraMovement>();
    }
    public void OpenDocBox()
    {
        if (!hasTriggered)
        {
            MissionUI.ClearText(1);
            hasTriggered = true;

            transform.GetChild(4).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void GainDocument()
    {
        gainedDocs++;

        if (gainedDocs == 4)
        {
            cameraMovement.CameraWait();
            MissionUI.ClearText(1);
            minimapIcon.gameObject.SetActive(true);
            Destroy(this);
        }
    }
}
