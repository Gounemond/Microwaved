using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

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

        // Wait until all the connected players have choosen and confirmed a microwave
        while (GameData.playerData.Count < ReInput.controllers.GetJoysticks().Length && GameData.playerData.Count < 2)
        {
            yield return null;
        }

        //Countdown: 3
        yield return new WaitForSeconds(1);

        //Countdown: 2
        yield return new WaitForSeconds(1);

        //Countdown: 1
        yield return new WaitForSeconds(1);

        // Load next level
        GameData.playerData.Sort();

        SceneManager.LoadScene(1);
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void AddPlayerMicrowave(int playerId, int microwaveIndex)
    {
        GameData.playerData.Add(new PlayerData(playerId,microwaveIndex));
    }
}
