using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class Audio_Manager : MonoBehaviour
{
public Sound[] sounds;

    void Awake(){
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume * ((float)GameManager.GetSound() / 10);
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            return;
        }
        s.source.Play();
    }

    public void Pause (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            return;
        }
        s.source.Pause();
    }
       
    public void UnPause (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            return;
        }
        s.source.UnPause();
    }

    // Start is called before the first frame update
    void Start()
    {
        Play("IntroMusic");
    }
}
