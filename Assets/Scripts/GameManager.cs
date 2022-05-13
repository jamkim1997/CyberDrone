using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    static int currentLevel = 1;
    static bool isHidden;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void EndGame()
    {
        Invoke ("DelayedAction", 0.5f);
        FindObjectOfType<Audio_Manager>().Play("GameOver");
    }

    void DelayedAction(){
        SceneManager.LoadScene("Game_Over");
    }

    public static void LoadScene(string sceneName)
    {
        currentLevel = int.Parse(sceneName);
        SceneManager.LoadScene(currentLevel);
        isHidden = false;
    }

    public static void NextScene()
    {
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
        isHidden = false;
    }

    public static void RePlay()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public static bool GetIsHidden()
    {
        return isHidden;
    }

    public static void SetIsHidden(bool hidden)
    {
        isHidden = hidden;
    }
}
