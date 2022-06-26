using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowTool : MonoBehaviour
{


  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<BoxCollider2D>());
            GetComponent<AudioSource>().Play();
            FindObjectOfType<L4Vent>().TakeScrewDriver();
            Destroy(gameObject, 2f);
        }
    }
}
