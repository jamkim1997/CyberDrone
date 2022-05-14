using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretLevel : MonoBehaviour
{
    public void MoveScene()
    {
        bool isloaded = FindObjectOfType<Exit>().IsLoaded;

        if(isloaded)
        {
            FindObjectOfType<Exit>().IsLoaded = false;
        }
        else
        {
            FindObjectOfType<Exit>().IsLoaded = true;
        }
    }
}
