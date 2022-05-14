using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityMusic : MonoBehaviour
{
    public AudioSource invisibilityOffSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            invisibilityOffSound.Play();
        }
        

        if(Input.GetKeyUp(KeyCode.Space))
        {
            invisibilityOffSound.Pause();
        } 

    }
}
