using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int playerLevel;
    public int playerMaxHealt;

    public GameData()
    {
        this.playerLevel = 10;
        this.playerMaxHealt = 100;
    }
}
