using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class L4Start : MonoBehaviour
{
    public RectTransform canvas;
    private Player player;

    private void Awake()
    {

        player = FindObjectOfType<Player>();
    }

    void Start()
    {
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
