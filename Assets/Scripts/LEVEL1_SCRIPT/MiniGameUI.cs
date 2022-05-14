using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameUI : MonoBehaviour

{
    private Player character;

    public GameObject miniGameUI;
   
    public GameObject imagePanel;

    public void miniGameOn()
    {
        character = FindObjectOfType<Player>();
        character.enabled = false;
        miniGameUI.SetActive(true);
        imagePanel.SetActive(true);
        MissionUI.ClearText(2);
    }


    
}
