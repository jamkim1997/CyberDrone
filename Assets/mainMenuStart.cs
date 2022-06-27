using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuStart : MonoBehaviour
{
    private AudioSource[] audioSources;

    private void Awake()
    {
        audioSources = FindObjectsOfType<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.volume *= ((float)GameManager.GetSound() / 10);
        }
    }
}
