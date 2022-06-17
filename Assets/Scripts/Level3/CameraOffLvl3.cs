using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraOffLvl3 : MonoBehaviour
{
    public Transform text;
    private SurveillanceCamera[] cctvs;
    private bool isInRange;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        cctvs = FindObjectsOfType<SurveillanceCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                audioSource.Play();
                foreach (SurveillanceCamera cctv in cctvs)
                {
                    cctv.changeViewDistance(0.1f);
                }
                MissionUI.ClearText(1);
                Destroy(transform.parent.GetChild(0).gameObject);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            text.gameObject.SetActive(false);
        }
    }
}