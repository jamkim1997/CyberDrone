using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTut : MonoBehaviour
{
    private MissionUI missionUI;

    private void Awake()
    {
        missionUI = FindObjectOfType<MissionUI>();
    }

    void Start()
    {
        List<string> missionList = new List<string> { "- Open the safe", "- Extract safely" };

        missionUI.SetMission(missionList);
    }
}
