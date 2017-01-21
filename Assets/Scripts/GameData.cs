using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public static List<PlayerData> playerData;
}

public class PlayerData : IComparable<PlayerData>
{
    public int playerId;
    public int microwaveSelected;
    public int kills;

    public PlayerData(int newPlayerId, int newMicrowaveSelected)
    {
        playerId = newPlayerId;
        microwaveSelected = newMicrowaveSelected;
        kills = 0;
    }

    //This method is required by the IComparable
    //interface. 
    public int CompareTo(PlayerData other)
    {
        if (other == null)
        {
            return 1;
        }

        // Elements get ordered naturally based on playerId
        return (int)(playerId - other.playerId);
    }
}
