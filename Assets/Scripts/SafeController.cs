using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SafeController : MonoBehaviour
{
    public bool hasTriggered;
    private Animator safeAnimator;
    public Animator exitAnimator;
    public Transform tutExitText;
    public Transform tutCompleteText;
    private Exit exit;

    private void Awake()
    {
        exit = FindObjectOfType<Exit>();
        safeAnimator = GetComponent<Animator>();
    }

    public void OpenSafe()
    {
        if(name == "SafeTut")
        {
            MissionUI.ClearText(1);
            Destroy(transform.GetChild(0).gameObject);
            Destroy(tutCompleteText.gameObject);
            tutExitText.gameObject.SetActive(true);
            StartCoroutine(MoveCamera());
        }

        if(!hasTriggered)
        {
            hasTriggered = true;
            safeAnimator.SetBool("Event_Animation", hasTriggered);
            exit.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private IEnumerator MoveCamera()
    {
        Player characterScript = FindObjectOfType<Player>();
        characterScript.enabled = false;
        Camera.main.DOOrthoSize(1, 2);
        Camera.main.transform.DOMove(new Vector3(16, 12.39f, -10f), 2);

        yield return new WaitForSeconds(2);
        exitAnimator.SetBool("Open", true);
        FindObjectOfType<Audio_Manager>().Play("DoorOpening");
        yield return new WaitForSeconds(1f);
        Camera.main.DOOrthoSize(9.8f, 0.5f);
        Camera.main.transform.DOMove(new Vector3(1.9f, 3.61f, -10f), 0.5f);

        yield return new WaitForSeconds(0.5f);
        characterScript.enabled = true;
        Destroy(this);
    }
}
