using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartL5 : MonoBehaviour
{
    private MissionUI missionUI;
    private Player player;
    public RectTransform canvas;
    public Animator doorAnimator;
    public Transform guard;
    private AudioSource[] audioSources;

    private void Awake()
    {
        audioSources = FindObjectsOfType<AudioSource>();
        missionUI = FindObjectOfType<MissionUI>();
        List<string> missionList = new List<string> { "- Open all safes", "", "- Escape from any exits" };

        missionUI.SetMission(missionList);

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
        SpeedBoost speedBoost = FindObjectOfType<SpeedBoost>();
        speedBoost.enabled = false;
        yield return new WaitForSeconds(1.5f);

        canvas.DOLocalMoveY(Screen.height, 0.5f);
        yield return new WaitForSeconds(1.5f);

        
        Destroy(canvas.parent.gameObject);
        // Game open

        if(gameObject.name == "Startgame")
        {
            
            Vector3 initlaPosition = Camera.main.transform.position;
            float initlaiCameraSize = Camera.main.orthographicSize;
            Camera.main.transform.DOMove(new Vector3(-11.18f, -4.18f, -10f), 2f);
            Camera.main.DOOrthoSize(3f, 2f);
            yield return new WaitForSeconds(2.3f);

            guard.gameObject.SetActive(true);
            doorAnimator.SetBool("Open", true);
            GetComponent<AudioSource>().Play();
            doorAnimator.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            guard.DOMoveY(-4.08f, 2f);
            yield return new WaitForSeconds(2f);

            doorAnimator.SetTrigger("Close");
            GetComponent<AudioSource>().Play();
            SpriteRenderer guardRenderer = guard.GetComponent<SpriteRenderer>();
            guardRenderer.flipX = true;
            guard.DOMoveX(-12.89f, 1f);

            yield return new WaitForSeconds(1f);
            guardRenderer.flipX = false;
            guard.GetComponent<Guard>().enabled = true;

            yield return new WaitForSeconds(1.5f);

            Camera.main.transform.DOMove(initlaPosition, 0.5f);
            Camera.main.DOOrthoSize(initlaiCameraSize, 0.5f);
            yield return new WaitForSeconds(0.5f);
            speedBoost.enabled = true;
        }
        player.enabled = true;
        speedBoost.enabled = true;

    }
}
