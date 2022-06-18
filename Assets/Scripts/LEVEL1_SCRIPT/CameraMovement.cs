using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    
    public void CameraWait()
    {
        StartCoroutine(WaitCamera());


    }
    private IEnumerator WaitCamera()
    {
        Player playerscript = FindObjectOfType<Player>();
        Guard guard = FindObjectOfType<Guard>();
        SurveillanceCamera[] cameras = FindObjectsOfType<SurveillanceCamera>();


        playerscript.enabled = false;
        guard.enabled = false;

        foreach (SurveillanceCamera camera in cameras)
        {
            camera.enabled = false;
        }

        Camera.main.transform.DOMove(new Vector3(16.5f, -1.0f, -10f), 2f); // Àý´ñ°ª
        Camera.main.DOOrthoSize(4, 2f);

        yield return new WaitForSeconds(2f);

        
       

        yield return new WaitForSeconds(2f);
        Camera.main.transform.DOLocalMove(new Vector3(0, 0, -10f), 2f); // »ó´ñ°ª
        Camera.main.DOOrthoSize(4, 2f);

        yield return new WaitForSeconds(2f);

        playerscript.enabled = true;
        guard.enabled = true;

        foreach (SurveillanceCamera camera in cameras)
        {
            camera.enabled = true;


        }

    }
}
