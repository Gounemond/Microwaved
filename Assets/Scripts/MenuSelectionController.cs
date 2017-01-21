using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectionController : MonoBehaviour
{
    public PodPlayerController[] podPlayer;

    public List<int> playerMicrowave;

	// Use this for initialization
	IEnumerator Start ()
    {
        // Initializes player game data
        GameData.playerData = new List<PlayerData>();	
        for (int i = 0; i < podPlayer.Length; i++)
        {
            // Every time a player joins and confirms his participation, we add him in the playerdata with his choice
            podPlayer[i].OnMicrowaveSelected += AddPlayerMicrowave;
        }

        // Wait until at least 2 players have confirmed their will to kill each other
        while (GameData.playerData.Count < 2)
        {
            yield return null;
        }

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddPlayerMicrowave(int playerId, int microwaveIndex)
    {
        GameData.playerData.Add(new PlayerData(playerId,microwaveIndex));
    }
}
