using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmitGas : MonoBehaviour
{
    public ParticleSystem[] particles;
    public Transform guard;
    public Animator animator;

    public bool test;
    public bool test2;
    private void Update()
    {
     if(test)
        {
            test = false;
            StartShow();
        }   
     if(test2)
        {
            test2 = false;
            VentOn();
        }
    }
    public void StartShow()
    {
        StartCoroutine(Show());
    }

    public void VentOn()
    {
        StartCoroutine(OnVent());
    }

    IEnumerator OnVent()
    {
        Camera.main.transform.DOMove(new Vector3(43.7f, 19.7f, -10), 2f);

        Player player = FindObjectOfType<Player>();
        player.enabled = false;
        yield return new WaitForSeconds(2f);
        int emissionRate = 200;
        while(emissionRate > 10)
        {
            emissionRate -= 10;
            foreach(ParticleSystem effect in particles)
            {
                var emission = effect.emission;
                emission.rateOverTime = emissionRate;
            }

            yield return new WaitForSeconds(0.25f);
        }
        foreach (ParticleSystem effect in particles)
        {
            var main = effect.main;
            main.loop = false;
        }
        yield return new WaitForSeconds(1.5f);
        foreach (ParticleSystem effect in particles)
        {
            Destroy(effect.gameObject);
        }
        Camera.main.transform.DOLocalMove(new Vector3(0, 0, -10), 0.5f);
        yield return new WaitForSeconds(0.5f);
        player.enabled = true;
    }

    IEnumerator Show()
    {
        Player player = FindObjectOfType<Player>();
        player.enabled = false;
        Camera.main.transform.DOMove(new Vector3(43.7f, 19.7f, -10), 2f);
        yield return new WaitForSeconds(2f);
        
        foreach(ParticleSystem effect in particles)
        {
            effect.gameObject.SetActive(true);
        }

        for(int i = 0; i < 3; i++)
        {
            guard.DOMoveY(17, 0.25f);
            yield return new WaitForSeconds(0.25f);
            guard.DOMoveY(16.2f, 0.25f);
            yield return new WaitForSeconds(0.25f);
        }

        guard.GetComponent<Animator>().SetBool("IsRunning", true);
        guard.DOMoveY(23.66f, 1f);
        yield return new WaitForSeconds(1f);
        animator.enabled = true;
        yield return new WaitForSeconds(1f);
        guard.DOMoveY(24.65f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        Destroy(guard.gameObject);
        animator.SetTrigger("Close");

        foreach(ParticleSystem effect in particles)
        {
            var main = effect.main;
            main.startSpeed = 2f;
        }

        yield return new WaitForSeconds(1f);
        Camera.main.transform.DOLocalMove(new Vector3(0, 0, -10), 0.5f);
        player.enabled = true;
    }
}
