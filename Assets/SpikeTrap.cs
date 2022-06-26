using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    Player player;
    bool isActivated;
    public AudioSource spike;
   

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    public void Activate()
    {
        if(spike)
        {
            spike.Play();
        }
        isActivated = true;
    }

    public void Unactivate()
    {
        isActivated = false;
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Character" && isActivated)
        {
            player.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
            Destroy(this);
        }
    }
}
