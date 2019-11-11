using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : SingletonMonoBehaviour<PlayerData>
{
    public Player player;


    public void SavePlayerData()
    {
        string player_data = JsonUtility.ToJson(player);
        PlayerPrefs.SetString(Const.USER_DATA, player_data);
    }
}

public class Player
{
    public string name;
    public int level;
    public int score;

    public Player(string name,int level,int score)
    {
        this.name = name;
        this.level = level;
        this.score = score;
    }
}
