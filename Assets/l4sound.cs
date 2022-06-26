using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class l4sound : MonoBehaviour
{
    public UnityEvent sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sound.Invoke();
    }
}
