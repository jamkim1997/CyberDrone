using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LightOn : MonoBehaviour
{
    private Renderer[] renderers;
    public Material lightOnMaterial;
    public Material lightOffMaterial;
    public Tilemap wallTile;

    // Start is called before the first frame update
    void Start()
    {
        renderers = FindObjectsOfType<Renderer>();
    }

    private void TurnOnLight()
    {
        foreach(Renderer renderer in renderers)
        {
            renderer.material = lightOnMaterial;
        }
        wallTile.color = Color.white;
    }

    private void TurnOffLight()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material = lightOffMaterial;
        }
        wallTile.color = Color.gray;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Character")
        {
            Destroy(GetComponent<Collider2D>());
            StartCoroutine(TurnOn());
        }
    }

    IEnumerator TurnOn()
    {
        TurnOnLight();
        yield return new WaitForSeconds(0.2f);
        TurnOffLight();
        yield return new WaitForSeconds(0.2f);
        TurnOnLight();
        yield return new WaitForSeconds(0.2f);
        TurnOffLight();
        yield return new WaitForSeconds(0.1f);
        TurnOnLight();
        yield return new WaitForSeconds(0.2f);
        TurnOffLight();
        yield return new WaitForSeconds(0.05f);
        TurnOnLight();

    }
}
