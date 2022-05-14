using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
    public Canvas canvas;
    public static TextMeshProUGUI text1;
    public static TextMeshProUGUI text2;
    private static bool IsCompleted1, IsCompleted2;
    private static List<string> missions = new List<string>();
    private int index;

    public void AddMission(string mission)
    {
        missions.Add(mission);
    }

    public void SetMission(List<string> missionList)
    {
        missions = missionList;
    }

    private void Update()
    {
        if(IsCompleted1 && IsCompleted2)
        {
            IsCompleted1 = false;
            IsCompleted2 = false;

            if(missions.Count % 2 == 0)
            {
                ChangeText(1, missions[index]);
                index++;
                ChangeText(2, missions[index]);
                index++;
            }
            else
            {
                if(index == missions.Count - 1)
                {
                    ChangeText(1, missions[index]);
                    ChangeText(2, "");
                    return;
                }

                ChangeText(1, missions[index]);
                index++;
                ChangeText(2, missions[index]);
                index++;
            }

            
            if(index < missions.Count)
            {
                
            }
            else
            {
                ChangeText(1, "");
            }
        }
    }


    private void Awake()
    {
        text1 = canvas.GetComponentsInChildren<TextMeshProUGUI>()[1];
        text2 = canvas.GetComponentsInChildren<TextMeshProUGUI>()[2];
    }

    private void Start()
    {
        text1.text = missions[index];
        index++;
        text2.text = missions[index];
        index++;
    }
    public static void ChangeText(int num, string msg)
    {
        TextMeshProUGUI text = GetText(num);
        text.color = Color.white;
        text.fontStyle = FontStyles.Normal;
        text.text = msg;
    }

    public static void ClearText(int num)
    {
        print($"Clear{num}");
        TextMeshProUGUI text = GetText(num);
        text.color = Color.red;
        text.fontStyle = FontStyles.Strikethrough;
        CompleteMission(num);
    }

    public static bool IsMissionCompleted(int num)
    {
        return num == 1 ? IsCompleted1 : IsCompleted2;
    }

    private static TextMeshProUGUI GetText(int num)
    {
        if(num == 1)
        {
            return text1;
        }
        else
        {
            return text2;
        }
    }

    private static void CompleteMission(int num)
    {
        if(num == 1)
        {
            IsCompleted1 = true;
        }
        else
        {
            IsCompleted2 = true;
        }
    }
}
