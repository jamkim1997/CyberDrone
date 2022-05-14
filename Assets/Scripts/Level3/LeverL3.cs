using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverL3 : MonoBehaviour
{
    private Transform Character;

    private bool isActivated;
    private Canvas canvas;
    private LeverControl leverControl;
    private AudioSource audioSource;

    [SerializeField]
    private Sprite onSprite;

    private void Start()
    {
        Character = FindObjectOfType<Player>(true).transform;
        canvas = GetComponentInChildren<Canvas>(true);
        leverControl = FindObjectOfType<LeverControl>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isActivated && Vector2.Distance(Character.position, transform.position) < 1.5f)
        {
            canvas.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                isActivated = true;
                audioSource.Play();
                if(name == "leverL3")
                {
                    FindObjectOfType<Exit>().IsLoaded = false;
                }
                else
                {
                    FindObjectOfType<Exit>().IsLoaded = true;
                }
                GetComponent<SpriteRenderer>().sprite = onSprite;
                Destroy(canvas.gameObject);
            }
        }
    }
}
