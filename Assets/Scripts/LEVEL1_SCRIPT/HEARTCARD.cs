using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HEARTCARD : MonoBehaviour
{
    [SerializeField] 
    private GameObject card_Back;

    public void Test()
    {
        if (card_Back.activeSelf)
        {
            card_Back.SetActive(false);
        }
    }

    
}
