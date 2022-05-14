using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4Start : MonoBehaviour
{
    private MissionUI missionUI;

    private void Awake()
    {
        missionUI = FindObjectOfType<MissionUI>();
        List<string> missionList = new List<string> { "- Open the safe", "- Exit safely" };

        missionUI.SetMission(missionList);
    }
}
