using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowTool : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            FindObjectOfType<L4Vent>().TakeScrewDriver();
           Destroy(gameObject);
        }
    }
}
