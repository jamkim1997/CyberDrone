using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentOpen : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform character;
    private float currentPercent, maxPercent;
    private bool checkingBin;
    private Canvas ProgressUI;
    [SerializeField]
    private Image percentSlider;
    [SerializeField]
    private GameObject yellowTool;
    [SerializeField] 
    Transform text;
    [SerializeField]
    bool DoesContainTool;
    public Sprite sprite;

    void Start()
    {
        character = FindObjectOfType<Player>().transform;
        ProgressUI = transform.parent.GetComponentInChildren<Canvas>(true);
        currentPercent = 0;
        maxPercent = 100;
        checkingBin = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (checkingBin && Vector2.Distance(transform.position, character.position) < 1.5f)
        {
            ProgressUI.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                if(text)
                {
                    Destroy(text.gameObject);
                }
                currentPercent += 40 * Time.deltaTime;

                percentSlider.fillAmount = currentPercent / maxPercent;
            }
        }
        else if (checkingBin)
        {
            ProgressUI.gameObject.SetActive(false);
        }
        if (currentPercent >= maxPercent)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
            ProgressUI.gameObject.SetActive(false);
            currentPercent = 0;
            if(DoesContainTool)
            {
                yellowTool.SetActive(true);
            }
            Destroy(this);
        }
    }

}
