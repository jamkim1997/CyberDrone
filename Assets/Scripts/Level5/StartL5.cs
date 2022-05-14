using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartL5 : MonoBehaviour
{
    private MissionUI missionUI;
    private Player player;
    public RectTransform canvas;

    private void Awake()
    {
        missionUI = FindObjectOfType<MissionUI>();
        List<string> missionList = new List<string> { "- Open all safes", "", "- Escape from any exits" };

        missionUI.SetMission(missionList);

        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        player.enabled = false;
        StartCoroutine(DeleteStartUI());
    }

    IEnumerator DeleteStartUI()
    {
        yield return new WaitForSeconds(1.5f);

        canvas.DOLocalMoveY(Screen.height, 0.5f);
        yield return new WaitForSeconds(1.5f);

        player.enabled = true;
        Destroy(canvas.parent.gameObject);

    }
}
