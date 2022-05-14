using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    private BoxCollider2D boxColldier2D;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Player player;

    void Start()
    {
        boxColldier2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            boxColldier2D.enabled = false;
            audioSource.Play();
            Color32 color = spriteRenderer.color;
            spriteRenderer.color = new Color32(color.r, color.g, color.b, 80);
            player.enabled = false;

        }
        if(Input.GetKeyUp(KeyCode.Space)) 
        {
            boxColldier2D.enabled = true;
            audioSource.Pause();
            Color32 color = spriteRenderer.color;
            spriteRenderer.color = new Color32(color.r, color.g, color.b, 255);
            player.enabled = true;
        }
    }
}
