using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBoost : MonoBehaviour
{
    public GameObject player;
    //public GameObject guard;
    GameObject[] guards;
    //public GameObject camera;
    bool isBoost;
    public Text boostText;

    // Start is called before the first frame update
    void Start()
    {
        isBoost = false;
        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            guards = GameObject.FindGameObjectsWithTag("Guard");
            //guards = FindObjectsOfType<Guard>();
            if (isBoost)
            {// Disable Boost if true
                //guard.GetComponent<Guard>().changeViewDistance(8);
                foreach (GameObject g in guards)
                {
                    g.GetComponent<Guard>().changeViewDistance(8);
                }
                //FindObjectOfType<Guard>().changeViewDistance(8);
                player.GetComponent<Player>().changeSpeed();
                isBoost = false;
                SetText();
                Debug.Log("Boost Disabled");
            }
            else
            {// Enable Boost if false
                //guard.GetComponent<Guard>().changeViewDistance(10);
                foreach (GameObject g in guards)
                {
                    g.GetComponent<Guard>().changeViewDistance(9);
                }
                //FindObjectOfType<Guard>().changeViewDistance(9);
                player.GetComponent<Player>().changeSpeed();
                isBoost = true;
                SetText();
                Debug.Log("Boost Enabled");
            }
            //camera.GetComponent<SurveillanceCamera>().changeViewDistance(0);
        }
        
    }

    void SetText()
    {
        if (isBoost)
        {
            boostText.text = "On";
        }
        else
        {
            boostText.text = "Off";
        }
    }
}
