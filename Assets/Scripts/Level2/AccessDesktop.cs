using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AccessDesktop : MonoBehaviour
{
    private bool isUSB;
    private Transform character;
    [SerializeField]
    private Transform doors;

    [SerializeField]
    private Transform USB;
    [SerializeField]
    private Canvas miniGame;
    private AudioSource audio;

    private void Awake()
    {
        character = FindObjectOfType<L2Player>().transform;
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Vector2.Distance(character.position, transform.position) < 1.5f)
        {
            Text text = transform.GetChild(0).GetChild(0).GetComponent<Text>();
            string msg = "'E' to interact";

            if (isUSB && Input.GetKeyDown(KeyCode.E))
            {
                doors.DOLocalMoveY(0, 0);
                miniGame.gameObject.SetActive(true);
                Destroy(GetComponentInChildren<Canvas>().gameObject);
                Destroy(this);
            }

            if (!isUSB)
            {
                msg = "Take Bug USB first";
            }

            text.text = msg;
            transform.GetComponentInChildren<Canvas>(true).gameObject.SetActive(true);
        }
        else
        {
            transform.GetComponentInChildren<Canvas>(true).gameObject.SetActive(false);
        }

        if(USB && Vector2.Distance(character.position, USB.position) < 1.5f)
        {
            USB.GetComponentInChildren<Canvas>(true).gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                isUSB = true;
                audio.Play();
                Destroy(USB.gameObject);
            }
        }
        else if(USB)
        {
            USB.GetComponentInChildren<Canvas>(true).gameObject.SetActive(false);
        }
    }
}
