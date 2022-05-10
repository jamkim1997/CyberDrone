using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BugGame : MonoBehaviour
{
    private L2Player character;
    private int target;

    private int currentLevel = 1;

    private int maxLevel = 10;

    private bool isPlaying;

    public Button[] bugs;

    public GameObject[] fires;

    private Vent ventScript;

    [SerializeField]
    private Transform AnimGuards;
    [SerializeField]
    private Transform mainRoomGuards;

    [SerializeField]
    private Sprite[] sprites;

    public Image targetRenderer;

    private AudioSource audio;
    public AudioClip[] sounds;

    public Animator[] labs;

    private void Awake()
    {
        ventScript = FindObjectOfType<Vent>();
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        character = FindObjectOfType<L2Player>();
        Play();
    }

    private void GetTargetNum()
    {
        target = Random.Range(0, 4);
    }

    private void Play()
    {
        isPlaying = true;
        GetTargetNum();
        targetRenderer.sprite = sprites[target];
    }

    IEnumerator MoveAnim()
    {
        bugs[target].transform.DOLocalMoveY(50, 1f);
        SoundEffect("Bug");
        yield return new WaitForSeconds(1f);

        bugs[target].transform.DOLocalMoveY(-183, 0f);
        yield return new WaitForSeconds(0.5f);

        EventSystem.current.SetSelectedGameObject(null);
        if (currentLevel == 4)
        {
            SoundEffect("Glitch");
            fires[0].SetActive(true);
            fires[1].SetActive(true);
        }
        if (currentLevel == 8)
        {
            SoundEffect("Glitch");
            fires[2].SetActive(true);
            fires[3].SetActive(true);
        }

        if (currentLevel == maxLevel)
        {
            currentLevel = 0;
            StartCoroutine(Clear());
        }
        Play();
    }

    IEnumerator Clear()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(mainRoomGuards.gameObject);
        character.enabled = false;
        SoundEffect("Lab");
        foreach (Animator lab in labs)
        {
            lab.enabled = true;
            lab.transform.GetComponentInChildren<ParticleSystem>(true).gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1.5f);

        Camera camera = Camera.main;
        camera.transform.DOLocalMove(new Vector3(12.17f, -7.63f, -10), 2);
        yield return new WaitForSecondsRealtime(2f);

        AnimGuards.gameObject.SetActive(true);
        AnimGuards.DOLocalMoveX(-5.8f, 1.5f);
        SoundEffect("Guard");
        yield return new WaitForSeconds(1f);

        camera.transform.DOLocalMove(new Vector3(-0.7f, -23.5f, -10), 1f);
        yield return new WaitForSeconds(1.2f);

        ventScript.OpenVent();
        camera.transform.DOShakePosition(0.5f, 1.5f);
        SoundEffect("Vent");
        yield return new WaitForSeconds(2f);

        camera.transform.DOLocalMove(new Vector3(0, 0, -10), 0.5f);
        yield return new WaitForSeconds(0.8f);

        character.enabled = true;
        Destroy(this);
    }

    IEnumerator WrongAnswer()
    {
        yield return new WaitForSeconds(2f);
        Play();
    }

    public void KillTarget(int index)
    {
        if (isPlaying && index == target)
        {
            isPlaying = false;
            targetRenderer.sprite = sprites[5];
            currentLevel++;
            StartCoroutine(MoveAnim());
        }
        else if (isPlaying)
        {
            isPlaying = false;
            targetRenderer.sprite = sprites[4];
            StartCoroutine(WrongAnswer());
        }
    }

    private void SoundEffect(string name)
    {
        switch (name)
        {
            case "Bug":
                audio.clip = sounds[3];
                break;
            case "Vent":
                audio.clip = sounds[1];
                break;
            case "Glitch":
                audio.clip = sounds[2];
                break;
            case "Lab":
                audio.clip = sounds[0];
                break;
            case "Guard":
                audio.clip = sounds[4];
                break;
        }
        audio.Play();
    }
}
