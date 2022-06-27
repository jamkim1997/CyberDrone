using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class L4Start : MonoBehaviour
{
    public RectTransform canvas;
    private Player player;
    private AudioSource[] audioSources;

    private void Awake()
    {
        audioSources = FindObjectsOfType<AudioSource>();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume *= ((float)GameManager.GetSound() / 10);
        }
        player.enabled = false;
        StartCoroutine(DeleteStartUI());
    }

    IEnumerator DeleteStartUI()
    {
        yield return new WaitForSeconds(1f);

        canvas.DOLocalMoveY(Screen.height, 0.5f);
        yield return new WaitForSeconds(1.5f);

        player.enabled = true;
        Destroy(canvas.parent.gameObject);

    }
}
