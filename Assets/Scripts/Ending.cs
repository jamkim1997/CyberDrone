using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ending : MonoBehaviour
{
    public RectTransform drone;

    private void Start()
    {
        StartCoroutine(Animation());   
    }

    IEnumerator Animation()
    {
        while (true)
        {
            drone.DOLocalMoveY(213, 0.5f);
            yield return new WaitForSeconds(0.5f);
            drone.DOLocalMoveY(-196, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void GobackToMainMenu()
    {
        DOTween.KillAll();
        GameManager.LoadScene($"{0}");
    }
}
