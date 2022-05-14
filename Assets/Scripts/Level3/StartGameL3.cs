using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameL3 : MonoBehaviour
{
    private MissionUI missionUI;

    private void Awake()
    {
        missionUI = FindObjectOfType<MissionUI>();
        List<string> missionList = new List<string> { "- Disable the cameras", "- Exit safely" };

        missionUI.SetMission(missionList);
    }

    void Start()
    {
        
    }
}
