using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameUI : MonoBehaviour
{
    private Animator animator;
    private Slider slider;
    public Text text;
    public Animator anim;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SoundChanged()
    {
        if(!slider)
        {
            slider = GetComponent<Slider>();
        }

        if(slider.value == 0)
        {
            text.text = "This game needs sound!";
        }
        else
        {
            text.text = $"Sound: {slider.value}";
        }
        GameManager.SetSound((int)slider.value);
        FindObjectOfType<AudioSource>().volume = ((float)slider.value / 10) * 0.3f;
    }

    public void SetUpSlider()
    {
        if (!slider)
        {
            slider = GetComponent<Slider>();
        }

        slider.value = GameManager.GetSound();

        if(slider.value == 0)
        {
            text.text = "This game needs sound!";
        }
        else
        {
            text.text = $"Sound: {slider.value}";
        }
    }

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    public void Skip()
    {
        anim.Play("Anim_MainMenu", 0, 1);

    }
    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }
}
