using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public static List<PlayerData> playerData;
}

public class PlayerData
{
    int playerId;
    int microwaveSelected;
    int kills;

    public PlayerData(int newPlayerId, int newMicrowaveSelected)
    {
        playerId = newPlayerId;
        microwaveSelected = newMicrowaveSelected;
        kills = 0;
    }
}
