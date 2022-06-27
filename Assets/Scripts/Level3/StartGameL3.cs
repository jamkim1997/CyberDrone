using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartGameL3 : MonoBehaviour
{
    private MissionUI missionUI;
    private Player player;
    public RectTransform canvas;
    private AudioSource[] audioSources;
    private void Awake()
    {
        missionUI = FindObjectOfType<MissionUI>();
        List<string> missionList = new List<string> { "- Disable the cameras", "- Exit safely" };

        missionUI.SetMission(missionList);
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
