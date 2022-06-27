using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private AudioSource[] audioSources;

    private void Awake()
    {
        audioSources = FindObjectsOfType<AudioSource>();
    }

    private void Start()
    {
        foreach (AudioSource audio in audioSources)
        {
            audio.volume *= ((float)GameManager.GetSound() / 10);
        }
    }
    public void Replay()
    {
        GameManager.RePlay();
    }

    public void Menu()
    {
        GameManager.LoadScene("0");
    }
}

