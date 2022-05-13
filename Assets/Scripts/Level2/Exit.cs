using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public bool isHidden;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.name == "Character")
        {
            if (isHidden)
            {
                GameManager.SetIsHidden();
            }

            GameManager.NextScene();
        }
    }
}
