using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextController : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        text.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Character")
        {
            text.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Character")
        {
            text.enabled = false;
        }
    }
}
