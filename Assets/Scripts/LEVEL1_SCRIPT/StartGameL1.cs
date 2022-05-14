using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameL1 : MonoBehaviour
{
    private MissionUI missionUI;

    private void Awake()
    {
        missionUI = FindObjectOfType<MissionUI>();
        List<string> missionList = new List<string> { "- Steal confidential document", "- Turn Off camera", "- Collect 4 document", "- Open the safe", "- Steal SD card ", "- Validate SD card with a server", "Escape safely"};

        missionUI.SetMission(missionList);
    }
}
