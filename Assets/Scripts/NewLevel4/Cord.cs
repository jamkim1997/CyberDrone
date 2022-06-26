using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Cord : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] BoxCollider2D exitCollider;

    Player player;
    Exit exitScript;
    Button[] buttons;
    string input = "";
    string answer = "2413";
    int wrongTime = 0;
    private AudioSource audioSource;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        player = FindObjectOfType<Player>();
        exitScript = FindObjectOfType<Exit>();
        audioSource = GetComponent<AudioSource>();
    }

    private void ResetInput()
    {
        foreach(Button button in buttons)
        {
            button.interactable = true;
        }
        input = "";
        wrongTime++;

        if(wrongTime > 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            player.enabled = true;
            Vector3 targetPosition = new Vector3(13.79f, -0.63f, -10);
            if(exitScript.IsLoaded)
            {
                targetPosition = new Vector3(17.4f, 12f, -10);
            }

            StartCoroutine(MoveCamera(targetPosition));
        }
    }

    private IEnumerator MoveCamera(Vector3 targetPosition)
    {
        Camera.main.transform.DOMove(targetPosition, 2f);
        Camera.main.DOOrthoSize(2, 2f);
        yield return new WaitForSeconds(3f);

        Camera.main.transform.DOLocalMove(new Vector3(0,0,-10), 0.5f);
        Camera.main.DOOrthoSize(5.3f, 0.5f);
        yield return new WaitForSeconds(0.5f);
    }


    private void UpdateUI(string input)
    {
        text.text = input;
    }

    public void AddInput(string input)
    {
        if(this.input.Length < 4)
        {
            audioSource.Play();
            this.input += input;
            UpdateUI(this.input);

            if (this.input.Length == 4)
            {
                foreach (Button button in buttons)
                {
                    button.interactable = false;
                }

                StartCoroutine(CheckTheAnswer());
            }
        }
    }

    IEnumerator CheckTheAnswer()
    {
        yield return new WaitForSeconds(0.5f);

        bool result = CompareTheAnswer();
        if (result)
        {
            exitCollider.enabled = true;
            text.color = Color.green;
            yield return new WaitForSeconds(0.5f);
            Destroy(FindObjectOfType<TerminalForCode>());
            Destroy(this.gameObject);
        }
        else
        {
            for(int i = 0; i < 5; i++)
            {
                UpdateUI(this.input);
                yield return new WaitForSeconds(0.1f);
                UpdateUI("");
                yield return new WaitForSeconds(0.1f);
            }
            ResetInput();
        }
    }

    private bool CompareTheAnswer()
    {
        int numInput = int.Parse(input);
        int numAnswer = int.Parse(answer);
        return numInput == numAnswer;
    }
}

