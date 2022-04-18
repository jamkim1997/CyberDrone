using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    
    bool gameHasEnded = false;
    float delayTime = 2f;
    public void EndGame ()
    {
        if (gameHasEnded == false)
        gameHasEnded = true;
        Debug.Log("Game Over");
        Invoke ("DelayedAction", delayTime);
        
        //Restart();
    }

    void DelayedAction(){
        SceneManager.LoadScene("Game_Over");
    }
}
