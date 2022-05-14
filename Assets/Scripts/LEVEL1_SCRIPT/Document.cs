using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Document : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            FindObjectOfType<DocController>().GainDocument();

            Destroy(gameObject);
        }
    }
}
