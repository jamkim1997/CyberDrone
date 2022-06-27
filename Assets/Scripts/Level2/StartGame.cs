using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private L2Player player;
    private MissionUI missionUI;
    public RectTransform doorUI;
    public RectTransform canvas;
    public Text text;
    private AudioSource[] audios;

    private void Awake()
    {
        audios = FindObjectsOfType<AudioSource>();
        player = FindObjectOfType<L2Player>(true);
        missionUI = FindObjectOfType<MissionUI>();
        if (GameManager.GetIsHidden())
        {
            player.transform.position = new Vector3(-16.92f, -35.58f, 0);
            doorUI.position = new Vector3(-2.14f, -20.48f, 0);
            List<string> missionList = new List<string> { "- Take a bug USB", "- Get into the office", "- Access the server", "- Do hacking the server", "- Escape through the vent", "- Take Key Card", "- Enter electricity room", "- Turn Off lights", "- Escape carefully !!!" };
            text.text = "Location : Lab";
            missionUI.SetMission(missionList);
        }
        else
        {
            player.transform.position = new Vector3(18.8f, -35.58f, 0);
            List<string> missionList = new List<string> { "- Take a bug USB", "- Get into the lab", "- Access the server", "- Do hacking the server", "- Escape through the vent", "- Take Key Card", "- Enter electricity room", "- Turn Off lights", "- Escape carefully !!!" };
            text.text = "Location : Office";
            missionUI.SetMission(missionList);
        }
        player.gameObject.SetActive(true);
    }

    void Start()
    {
        foreach (AudioSource audioSource in audios)
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
