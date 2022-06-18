using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    private Transform Character;

    private LeverControl leverControl;

    private bool isActivated;
    private Canvas canvas;

    [SerializeField]
    private Sprite onSprite;

    private void Start()
    {
        Character = FindObjectOfType<L2Player>().transform;
        leverControl = transform.parent.GetComponent<LeverControl>();
        canvas = GetComponentInChildren<Canvas>(true);
    }

    void Update()
    {
      if(!isActivated && Vector2.Distance(Character.position, transform.position) < 1.5f) {
            canvas.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                isActivated = true;
                leverControl.LeverDown();
                GetComponent<SpriteRenderer>().sprite = onSprite;
                Destroy(canvas.gameObject);
                Destroy(transform.GetChild(0).gameObject);
                Destroy(this);
            }
        }
    }
}
