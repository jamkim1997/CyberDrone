using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private L2Player player;
    private MissionUI missionUI;
    public RectTransform doorUI;

    private void Awake()
    {
        player = FindObjectOfType<L2Player>(true);
        missionUI = FindObjectOfType<MissionUI>();
        GameManager.SetIsHidden(true);
    }

    void Start()
    {
        if(GameManager.GetIsHidden())
        {
            player.transform.position = new Vector3(-16.92f, -35.58f, 0);
            doorUI.position = new Vector3(-2.14f, -20.48f, 0);
            List<string> missionList = new List<string> {"- Take a bug USB", "- Get into the office", "- Access the server", "- Do hacking the server", "- Escape through the vent", "- Take Key Card", "- Enter electricity room", "- Turn Off lights", "- Escape carefully !!!" };

            missionUI.SetMission(missionList);
        }
        else
        {
            player.transform.position = new Vector3(18.8f, -35.58f, 0);
            List<string> missionList = new List<string> { "- Take a bug USB", "- Get into the lab", "- Access the server", "- Do hacking the server", "- Escape through the vent", "- Take Key Card", "- Enter electricity room", "- Turn Off lights", "- Escape carefully !!!" };

            missionUI.SetMission(missionList);
        }
        player.gameObject.SetActive(true);
    }
}
