using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    public Animator animator;
    
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

        Camera.main.transform.DOMove(new Vector3(16.5f, -3.0f, -10f), 2f); // ??????
        Camera.main.DOOrthoSize(4, 2f);

        yield return new WaitForSeconds(2f);

        animator.enabled = true;
        animator.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        Camera.main.transform.DOLocalMove(new Vector3(0, 0, -10f), 2f); // ??????
        Camera.main.DOOrthoSize(5.3f, 2f);

        yield return new WaitForSeconds(2f);

        playerscript.enabled = true;
        guard.enabled = true;

        foreach (SurveillanceCamera camera in cameras)
        {
            camera.enabled = true;
        }

    }
}
