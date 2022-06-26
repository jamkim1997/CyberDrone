using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalForCode : MonoBehaviour
{
    private bool isInRange;
    [SerializeField] GameObject minimap;

    public Transform text;
    public Transform codeUI;

    Player player;
    private AudioSource audioSource;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        audioSource = GetComponent<AudioSource>();
    }
    public void OnDestroy()
    {
        Destroy(text.gameObject);
        Destroy(minimap);
        player.enabled = true;
    }

    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                audioSource.Play();
                codeUI.gameObject.SetActive(true);
                player.enabled = false;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            if (text)
            {
                text.gameObject.SetActive(true);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            if (text)
            {
                text.gameObject.SetActive(false);
            }
            
        }
    }
}
