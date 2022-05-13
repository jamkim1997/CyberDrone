using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameUI : MonoBehaviour

{

    public GameObject miniGameUI;
   
    public GameObject imagePanel;

    //public float time = 30;
    
    // Start is called before the first frame update
   

    // Update is called once per frame
    public void miniGameOn()//Update()
    {


            miniGameUI.SetActive(true);
            imagePanel.SetActive(true);

           




            Debug.Log("working");
        
    }


    /*IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        Destroy(miniGameUI);
        Destroy(imagePanel);


    }*/


    
}
