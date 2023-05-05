using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    // using static so it can be accessed 
    // by any script
    // without referencing this script
    public static int Money;
    public int startMoney = 400;

    void Start()
    {
        Money = startMoney;
    }
}
