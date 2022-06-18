using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vent : MonoBehaviour
{
    public Sprite[] sprites;
    public BoxCollider2D[] ventEntryColliders;
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer ventExitRenderer;
    public Transform ventTileMap;
    public Transform animGuards;
    public Transform keyCard;
    private CardKey cardKeyScript;
    private L2Player playerScripe;
    public L2Guard labGuard;

    private bool isWorking;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ventEntryColliders = GetComponentsInChildren<BoxCollider2D>();
        cardKeyScript = FindObjectOfType<CardKey>();
        playerScripe = FindObjectOfType<L2Player>();
    }

    public void OpenVent()
    {
        isWorking = true;
        ventTileMap.gameObject.SetActive(true);
        spriteRenderer.sprite = sprites[0];
        ventExitRenderer.sprite = sprites[0];
        ventEntryColliders[0].enabled = false;
        ventEntryColliders[3].enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isWorking)
        {
            if (ventEntryColliders[1])
            {
                labGuard.IsAIOn = false;
                Destroy(labGuard.GetComponent<NavMeshAgent>());
                FindObjectOfType<Audio_Manager>().Play("IntroMusic");
                FindObjectOfType<Audio_Manager>().Pause("Run");
                playerScripe.GetIntoTheVent();
                spriteRenderer.sprite = sprites[1];
                ventEntryColliders[0].enabled = true;
                Destroy(ventEntryColliders[1]);
            }
            else
            {
                playerScripe.GetOutTheVent();
                ventExitRenderer.sprite = sprites[1];
                ventEntryColliders[3].enabled = true;
                Destroy(ventEntryColliders[2]);
                Destroy(animGuards.gameObject);
                keyCard.gameObject.SetActive(true);
                cardKeyScript.ActivateKeyCard();
                MissionUI.ClearText(1);
                Destroy(this);
            }
        }
    }
}
