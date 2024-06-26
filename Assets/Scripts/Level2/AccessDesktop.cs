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
    private GameObject minimapIcon;

    private void Start()
    {
        character = FindObjectOfType<L2Player>().transform;
        minimapIcon = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (Vector2.Distance(character.position, transform.position) < 1.5f)
        {
            Text text = transform.GetChild(1).GetChild(0).GetComponent<Text>();
            string msg = "'E' to interact";

            if (isUSB && Input.GetKeyDown(KeyCode.E))
            {
                doors.DOLocalMoveY(0, 0.1f);
                miniGame.gameObject.SetActive(true);
                Destroy(GetComponentInChildren<Canvas>().gameObject);
                Destroy(minimapIcon);
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
                MissionUI.ClearText(1);
                isUSB = true;
                GetComponent<AudioSource>().Play();
                minimapIcon.SetActive(true);
                Destroy(USB.gameObject);
            }
        }
        else if(USB)
        {
            USB.GetComponentInChildren<Canvas>(true).gameObject.SetActive(false);
        }
    }
}
