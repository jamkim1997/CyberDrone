using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialStart : MonoBehaviour
{
    public RectTransform canvas;
    private Player player;

    void Start()
    {
        StartCoroutine(DeleteStartUI());
        player = FindObjectOfType<Player>();
        player.enabled = false;
    }

    IEnumerator DeleteStartUI()
    {
        yield return new WaitForSeconds(2f);

        canvas.DOLocalMoveY(Screen.height, 1.5f);
        yield return new WaitForSeconds(1.5f);

        player.enabled = true;
        Destroy(canvas.parent.gameObject);

    }

}
