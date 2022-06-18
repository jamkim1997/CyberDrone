using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CameraAnim : MonoBehaviour
{
    public float[] values;
    public UnityEvent[] events;
    private bool isWhileWorking;

    public void UpdateSprite(float x)
    {
        if(name == "SpeicalCCTV") {
            if(!isWhileWorking)
            {
                isWhileWorking = true;
                StartCoroutine(Rotate());
            }
            return;
        }

        for(int i = 0; i < values.Length; i++)
        {
            if(x < values[i])
            {
                events[i].Invoke();
                break;
            }
        }
    }

    private IEnumerator Rotate()
    {
        while(true)
        {
            transform.DOLocalRotate(new Vector3(transform.localRotation.x, transform.localRotation.y, -136), 3f);
            yield return new WaitForSeconds(3f);
            transform.DOLocalRotate(new Vector3(transform.localRotation.x, transform.localRotation.y, -150), 3f);
            yield return new WaitForSeconds(3f);
        }
    }

}
