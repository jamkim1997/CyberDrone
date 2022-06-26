using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class ChaseGuard : MonoBehaviour
{
    public AudioSource bgAudio;
    Player player;
    NavMeshAgent nav;
    Guard guardScript;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        guardScript = GetComponent<Guard>();
        if (GetComponent<NavMeshAgent>())
        {
            nav = GetComponent<NavMeshAgent>();
            nav.updateRotation = false;
            nav.updateUpAxis = false;
        }
    }

    public void StartChase()
    {
        StartCoroutine(Chase());
    }

    private IEnumerator Chase()
    {
        Camera.main.transform.DOMove(new Vector3(-10.22f, 13.12f, -10), 2f);
        yield return new WaitForSeconds(2f);
        transform.GetChild(1).gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        Camera.main.transform.DOLocalMove(new Vector3(0, 0, -10), 0.5f);

        yield return new WaitForSeconds(0.5f);

        Destroy(bgAudio.gameObject);
        GetComponentInChildren<AudioSource>().Play();
        guardScript.IsAIOn = true;
       
    }
}
