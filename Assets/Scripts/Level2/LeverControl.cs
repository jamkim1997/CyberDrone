using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;

public class LeverControl : MonoBehaviour
{
    public Renderer[] targetRenderer;
    public Material darkMaterial;
    private int step = 0;
    public Tilemap[] tileMaps;
    private BoxCollider2D doorCollider;
    private L2CCTV cctv;
    public Transform guard;
    public Transform endGuards;
    public Transform exit;
    private AudioSource audioSource;
    public AudioClip[] sounds;
    private L2Player player;
    public Material lightOnMaterial;
    public GameObject minimapIcon;
    public FieldOfView[] FieldOfView;

    private void Awake()
    {
        doorCollider = FindObjectOfType<CardKey>().GetComponentInChildren<BoxCollider2D>();
        cctv = FindObjectOfType<L2CCTV>();
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<L2Player>(true);
    }

    private void ActivateLever() {
        switch (step)
        {
            case 1:
                Debug.Log("1");
                break;
            case 2:
                StartCoroutine(Emergency());
                StartCoroutine(Siren());
                Debug.Log("2");
                break;
            case 3:
                StopAllCoroutines();
                LightOff();
                Debug.Log("3");
                break;
            default:
                Debug.Log("Wrong num");
                break;

        }
    }

    public void LeverDown()
    {
        SoundEffect("Lever");
        step++;
        ActivateLever();
    }

    IEnumerator Emergency()
    {
        player.enabled = false;
        doorCollider.enabled = true;
        SoundEffect("Siren");
        tileMaps[0].color = new Color32(0X8E, 0X62, 0X62, 0xFF);

        Camera camera = Camera.main;
        camera.transform.DOMove(new Vector3(-10.41f, 5.32f, -10), 1);
        yield return new WaitForSeconds(1f);

        endGuards.DOLocalMoveY(9.5f, 3f);

        yield return new WaitForSeconds(1f);
        SoundEffect("Siren");

        yield return new WaitForSeconds(2f);
        exit.GetComponent<Animator>().SetBool("Open", true);
        SoundEffect("Door");
        yield return new WaitForSeconds(1.5f);

        
        camera.transform.DOLocalMove(new Vector3(0, 0f, -10), 1);

        yield return new WaitForSeconds(1f);
        player.enabled = true;
        Destroy(endGuards.gameObject);
    }

    IEnumerator Siren()
    {
        while (true)
        {
            tileMaps[1].color = new Color32(0xC3, 0x61, 0x61, 0xFF);
            yield return new WaitForSeconds(0.1f);
            tileMaps[1].color = new Color32(0xC8, 0x38, 0x38, 0xFF);
            yield return new WaitForSeconds(0.1f);
            tileMaps[1].color = new Color32(0xFF,0x04,0x04, 0xFF);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void LightOff()
    {
        MissionUI.ClearText(2);
        SoundEffect("Power");
        foreach (Renderer target in targetRenderer)
        {
            target.material = darkMaterial;
        }

        cctv.enabled = false;
        guard.GetComponent<L2Guard>().enabled = false;
        guard.GetComponent<Animator>().enabled = false;


        FieldOfView = FindObjectsOfType<FieldOfView>();
        //delete the field of view
        foreach (FieldOfView fieldOfView in FieldOfView)
        {
            Destroy(fieldOfView.gameObject);
        }
        minimapIcon.SetActive(true);
    }

    public void LightOn()
    {
        foreach (Renderer target in targetRenderer)
        {
            target.material = lightOnMaterial;
        }
        tileMaps[1].color = Color.white;
        cctv.enabled = true;
        cctv.GetComponent<Animator>().enabled = true;
        guard.GetComponent<L2Guard>().enabled = true;
        guard.GetComponent<Animator>().enabled = true;
    }

    private void SoundEffect(string name) {
        switch (name)
        {
            case "Lever":
                audioSource.clip = sounds[0];
                break;
            case "Siren":
                audioSource.clip = sounds[1];
                break;
            case "Power":
                audioSource.clip = sounds[2];
                break;
            case "Door":
                audioSource.clip = sounds[3];
                break;
        }
        audioSource.Play();
    }
}
