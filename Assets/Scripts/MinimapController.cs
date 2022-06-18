using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    private Transform player;

    private void Awake()
    {
        if (FindObjectOfType<Player>())
        {
            player = FindObjectOfType<Player>().transform;
        }
        else if(FindObjectOfType<L2Player>())
        {
            player = FindObjectOfType<L2Player>().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}
