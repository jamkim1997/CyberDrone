using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartL5 : MonoBehaviour
{
    private MissionUI missionUI;

    private void Awake()
    {
        missionUI = FindObjectOfType<MissionUI>();
        List<string> missionList = new List<string> { "- Open all safes", "", "- Escape from any exits" };

        missionUI.SetMission(missionList);
    }
}
