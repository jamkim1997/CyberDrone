using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocController : MonoBehaviour
{
    private bool hasTriggered;
    private int gainedDocs;

    public Animator greenDoor;

    public void OpenDocBox()
    {
        if (!hasTriggered)
        {
            MissionUI.ClearText(1);
            hasTriggered = true;

            transform.GetChild(0).gameObject.SetActive(true);
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
            MissionUI.ClearText(1);
            greenDoor.enabled = true;
            Destroy(this);
        }
    }
}
