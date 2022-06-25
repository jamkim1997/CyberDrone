using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public bool IsHidden;
    public bool IsLoaded;
    public int sceneNum;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.name == "Character")
        {
            if (IsLoaded)
            {
                GameManager.LoadScene($"{sceneNum}");
                return;
            }

            if (IsHidden)
            {
                GameManager.SetIsHidden();
            }

            GameManager.NextScene();
        }
    }

    public void ChangeIsHidden(bool hidden=true)
    {
        IsHidden = hidden;
    }

    public void SetLoaded(bool value)
    {
        IsLoaded = value;
    }
}
