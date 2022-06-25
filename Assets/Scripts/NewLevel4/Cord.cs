using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cord : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    Button[] buttons;
    string input = "";
    string answer = "2413";
    int wrongTime = 0;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
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
            // Show the cpord
        }
    }
    private void UpdateUI(string input)
    {
        text.text = input;
    }

    public void AddInput(string input)
    {
        this.input += input;
        UpdateUI(this.input);

        if(this.input.Length == 4)
        {
            foreach(Button button in buttons)
            {
                button.interactable = false;
            }

            StartCoroutine(CheckTheAnswer());
        }
    }

    IEnumerator CheckTheAnswer()
    {
        yield return new WaitForSeconds(0.5f);

        bool result = CompareTheAnswer();
        if (result)
        {
            // Open the gate
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

