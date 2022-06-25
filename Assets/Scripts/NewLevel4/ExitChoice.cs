using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitChoice : MonoBehaviour
{
    static bool ExistCard;

    public static bool GetCard()
    {
        return ExistCard;
    }

    public static void SetCard(bool value)
    {
        ExistCard = value;
    }
}
