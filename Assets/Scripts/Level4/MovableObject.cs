using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 positionToMoveTo;
    public float speed;        
    void Start()
    {
        StartCoroutine(LerpPosition(positionToMoveTo, speed));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LerpPosition(Vector3 target, float speed)
    {
        Vector2 startPosition = transform.position;
        while (true)
        {
        float time = Mathf.PingPong(Time.time * speed, 1);
        //Vector3 startPosition = lastMoveDir;          
        
        
        transform.position = Vector3.Lerp(startPosition, target, time);
        yield return null;
        }
    }
            
}
