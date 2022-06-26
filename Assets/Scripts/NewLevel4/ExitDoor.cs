using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class ExitDoor : MonoBehaviour
{
    public UnityEvent exitEvent;

    Animator animator;
    bool isInRange;
    bool isWorking;
    Text text;

    public Transform canvas;
    public int order;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        text = canvas.GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (isInRange && !isWorking)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isWorking = true;
                if (text)
                {
                    Destroy(canvas.gameObject);
                }
                animator.SetTrigger($"{order}Door");
                exitEvent.Invoke();
                ExitChoice.SetCard(false);
                StartCoroutine(DestroyCard());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            text.text = "Access card needed";
            if (ExitChoice.GetCard())
            {
                isInRange = true;
                text.text = "'E' to Open";
            }

            if (text)
            {
                canvas.gameObject.SetActive(true);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            if (text)
            {
                canvas.gameObject.SetActive(false);
            }

        }
    }

   IEnumerator DestroyCard()
    {
        Transform accessCard = FindObjectOfType<Player>().transform.GetChild(2);
        accessCard.gameObject.SetActive(true);
        accessCard.DOLocalMoveY(1.4f, 2f);
        yield return new WaitForSeconds(2f);
        Destroy(accessCard.gameObject);
        Destroy(this);
    }
}
