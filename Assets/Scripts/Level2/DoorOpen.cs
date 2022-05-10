using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoorOpen : MonoBehaviour
{
    private float currentPercent, maxPercent;
    private Transform character;
    private bool isWorking;

    private Canvas progressCanvas;
    [SerializeField]
    private Text changableText;
    [SerializeField]
    private Text percentText;
    [SerializeField]
    private Image slider;
    private AudioSource audio;
    public AudioClip openDoor;

    private void Awake()
    {
        character = FindObjectOfType<L2Player>().transform;
        progressCanvas = transform.parent.GetComponentInChildren<Canvas>(true);
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        currentPercent = 0;
        maxPercent = 100;
        isWorking = true;
    }

    void Update()
    {
        if(isWorking && Vector2.Distance(transform.position, character.position) < 1.5f)
        {
            progressCanvas.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                if (currentPercent <= 100)
                {
                    currentPercent += 0.5f;
                }

                if (audio.isPlaying)
                {
                    audio.UnPause();
                }
                else
                {
                    audio.Play();
                }

                changableText.text = "In Progress";
                slider.fillAmount = currentPercent / maxPercent;
                percentText.text = "" + (int)currentPercent +"%";
                
            }
            else
            {
                changableText.text = "'E' to open";
            }
        }
        else if(isWorking)
        { 
            audio.Pause();
            progressCanvas.gameObject.SetActive(false);
        }

        if(currentPercent >= maxPercent)
        {
            audio.Stop();
            isWorking = false;
            progressCanvas.gameObject.SetActive(false);
            StartCoroutine(OpenTheGate());
        }
    }

    IEnumerator OpenTheGate()
    {
        transform.parent.DOLocalMoveY(2, 2f);
        yield return new WaitForSeconds(2.5f);
        Destroy(this);
    }
}
