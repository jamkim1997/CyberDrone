using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraViewChange : MonoBehaviour
{
    public void Test()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        Player playerScript = FindObjectOfType<Player>();
        SurveillanceCamera[] cameras = FindObjectsOfType<SurveillanceCamera>();
        Guard guard = FindObjectOfType<Guard>();

        playerScript.enabled = false;
        foreach(SurveillanceCamera camera in cameras)
        {
            camera.enabled = false;
        }
        guard.enabled = false;

        Camera.main.transform.DOMove(new Vector3(16.71f, -3.72f, -10f), 2f);
        Camera.main.DOOrthoSize(3, 2f);

        yield return new WaitForSeconds(2f);
        // Open the door code

        yield return new WaitForSeconds(2f);
        Camera.main.transform.DOLocalMove(new Vector3(0,0, -10f), 2f);
        Camera.main.DOOrthoSize(4.5f, 2f);

        yield return new WaitForSeconds(2f);
        playerScript.enabled = true;
        foreach (SurveillanceCamera camera in cameras)
        {
            camera.enabled = true;
        }
        guard.enabled = true;
    }
}
