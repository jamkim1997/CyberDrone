using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardKey : MonoBehaviour
{
    private L2Player character;
    private bool isKeyCard;
    public BoxCollider2D childCollider;

    private Transform keycard;
    private Canvas[] canvas;
    private AudioSource audioSource;

    public AudioClip openDoor;

    private void Start()
    {
        character = FindObjectOfType<L2Player>();
        canvas = GetComponentsInChildren<Canvas>(true);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (canvas[0])
        {
            if (Vector2.Distance(character.transform.position, transform.position) < 1.5f)
            {
                Text text = GetComponentsInChildren<Text>(true)[0];
                string msg = "'E' to interact";

                if (isKeyCard && Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(canvas[0].gameObject);
                    childCollider.enabled = false;
                    audioSource.clip = openDoor;
                    audioSource.Play();

                    GetComponent<Animator>().SetBool("Open", true);
                }

                if (!isKeyCard)
                {
                    msg = "Authorised Zone \n Key Card Required";
                }

                text.text = msg;
                canvas[0].gameObject.SetActive(true);
            }
            else
            {
                canvas[0].gameObject.SetActive(false);
            }
        }

        if (keycard && !isKeyCard && Vector2.Distance(character.transform.position, keycard.position) < 1.5f)
        {
            canvas[1].gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                isKeyCard = true;
                audioSource.Play();
                MissionUI.ClearText(2);
                Destroy(keycard.gameObject);
            }
        }
        else if (keycard)
        {
            canvas[1].gameObject.SetActive(false);
        }
    }

    public void ActivateKeyCard()
    {
        keycard = transform.GetChild(1).transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Animator>().SetTrigger("Close");
        MissionUI.ClearText(1);
        Destroy(this);
    }
}
