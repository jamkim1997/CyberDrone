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

    private AudioSource audioSource;
    public AudioClip[] sounds;

    public Animator[] labs;
    public Transform labGuard;
    public Transform fov;

    public Sprite hackedImage;

    private void Awake()
    {
        ventScript = FindObjectOfType<Vent>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        character = FindObjectOfType<L2Player>();
        character.enabled = false;
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

        SoundEffect("bomb");
        bugs[target].transform.DOShakeScale(0.5f, 2);
        yield return new WaitForSeconds(0.5f);

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
            targetRenderer.sprite = sprites[4];
            StartCoroutine(Clear());
        }
        else
        {
            Play();
        }
        
    }

    IEnumerator Clear()
    {
        targetRenderer.transform.parent.GetChild(0).GetComponent<Image>().sprite = hackedImage;
        foreach(GameObject fire in fires)
        {
            Destroy(fire);
        }
        SoundEffect("Computer");
        yield return new WaitForSeconds(1.5f);

        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(mainRoomGuards.gameObject);
        Destroy(fov.gameObject);
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

        

        L2Guard labGuardScript = labGuard.GetComponent<L2Guard>();
        labGuardScript.enabled = false;
        labGuardScript.IsAIOn = true;
        camera.transform.DOMove(new Vector3(labGuard.position.x, labGuard.position.y, -10), 1f);
        yield return new WaitForSeconds(1f);
        labGuard.GetChild(1).gameObject.SetActive(true);
        FindObjectOfType<Audio_Manager>().Play("Run");

        yield return new WaitForSeconds(1f);

        camera.transform.DOLocalMove(new Vector3(0, 0, -10), 0.5f);
        yield return new WaitForSeconds(0.8f);
        character.enabled = true;
        MissionUI.ClearText(1);
        MissionUI.ClearText(2);
        labGuardScript.enabled = true;
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
                audioSource.clip = sounds[3];
                break;
            case "Vent":
                audioSource.clip = sounds[1];
                break;
            case "Glitch":
                audioSource.clip = sounds[2];
                break;
            case "Lab":
                audioSource.clip = sounds[0];
                break;
            case "Guard":
                audioSource.clip = sounds[4];
                break;
            case "Computer":
                audioSource.clip = sounds[6];
                break;
            case "bomb":
                audioSource.clip = sounds[5];
                break;
        }
        audioSource.Play();
    }
}
