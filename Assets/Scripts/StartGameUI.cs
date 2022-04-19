using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartGameUI : MonoBehaviour
{
    [SerializeField]
    private Button Tutorial;
    [SerializeField]
    private Button Level1;
    [SerializeField]
    private Button Level2;
    [SerializeField]
    private Button Level3;
    [SerializeField]
    private Button Level4;
    [SerializeField]
    private Button Level5;

    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    private void OnEnable()
    {
        switch (PlayerSettings.controlType) 
        {
            case EControlType.Tutorial:
                Tutorial.image.color = Color.green;
                Level1.image.color = Color.white;
                Level2.image.color = Color.white;
                Level3.image.color = Color.white;
                Level4.image.color = Color.white;
                Level5.image.color = Color.white;
                break;


            case EControlType.Level1:
                Tutorial.image.color = Color.white;
                Level1.image.color = Color.green;
                Level2.image.color = Color.white;
                Level3.image.color = Color.white;
                Level4.image.color = Color.white;
                Level5.image.color = Color.white;
                break;

            case EControlType.Level2:
                Tutorial.image.color = Color.white;
                Level1.image.color = Color.white;
                Level2.image.color = Color.green;
                Level3.image.color = Color.white;
                Level4.image.color = Color.white;
                Level5.image.color = Color.white;
                break;


            case EControlType.Level3:
                Tutorial.image.color = Color.white;
                Level1.image.color = Color.white;
                Level2.image.color = Color.white;
                Level3.image.color = Color.green;
                Level4.image.color = Color.white;
                Level5.image.color = Color.white;
                break;


            case EControlType.Level4:
                Tutorial.image.color = Color.white;
                Level1.image.color = Color.white;
                Level2.image.color = Color.white;
                Level3.image.color = Color.white;
                Level4.image.color = Color.green;
                Level5.image.color = Color.white;
                break;

            case EControlType.Level5:
                Tutorial.image.color = Color.white;
                Level1.image.color = Color.white;
                Level2.image.color = Color.white;
                Level3.image.color = Color.white;
                Level4.image.color = Color.white;
                Level5.image.color = Color.green;
                break;




        }
    }

    public void SetcontrolMode(int controltype)
    {
        PlayerSettings.controlType = (EControlType)controltype;
        switch (PlayerSettings.controlType)
        {
            case EControlType.Tutorial:
                Tutorial.image.color = Color.green;
                Level1.image.color = Color.white;
                Level2.image.color = Color.white;
                Level3.image.color = Color.white;
                Level4.image.color = Color.white;
                Level5.image.color = Color.white;
                break;


            case EControlType.Level1:
                Tutorial.image.color = Color.white;
                Level1.image.color = Color.green;
                Level2.image.color = Color.white;
                Level3.image.color = Color.white;
                Level4.image.color = Color.white;
                Level5.image.color = Color.white;
                break;

            case EControlType.Level2:
                Tutorial.image.color = Color.white;
                Level1.image.color = Color.white;
                Level2.image.color = Color.green;
                Level3.image.color = Color.white;
                Level4.image.color = Color.white;
                Level5.image.color = Color.white;
                break;


            case EControlType.Level3:
                Tutorial.image.color = Color.white;
                Level1.image.color = Color.white;
                Level2.image.color = Color.white;
                Level3.image.color = Color.green;
                Level4.image.color = Color.white;
                Level5.image.color = Color.white;
                break;


            case EControlType.Level4:
                Tutorial.image.color = Color.white;
                Level1.image.color = Color.white;
                Level2.image.color = Color.white;
                Level3.image.color = Color.white;
                Level4.image.color = Color.green;
                Level5.image.color = Color.white;
                break;

            case EControlType.Level5:
                Tutorial.image.color = Color.white;
                Level1.image.color = Color.white;
                Level2.image.color = Color.white;
                Level3.image.color = Color.white;
                Level4.image.color = Color.white;
                Level5.image.color = Color.green;
                break;




        }

    }
    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }
}
