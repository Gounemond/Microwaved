using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BattleArenaManager : MonoBehaviour
{

    public static BattleArenaManager instance;

    public Color[] playerColor;

    public GameObject[] microwavePrefab;

    public GameObject[] microwavePlayer;

    public Transform[] spawnPosition;

    // Use this for initialization
    void Awake()
    {
        if (!instance)
        {
            instance = this as BattleArenaManager;
        }
        else
        {
            DestroyObject(gameObject);
        }

        for (int i = 0; i < GameData.playerData.Count; i++)
        {
            microwavePlayer[i] = (GameObject)Instantiate(microwavePrefab[GameData.playerData[i].microwaveSelected], spawnPosition[i].position, Quaternion.identity);
            microwavePlayer[i].GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControlRewired>().playerId = i;
        }
    }

    IEnumerator Start()
    {
        //Intro
        yield return new WaitForSeconds(1);
        /*for (int i = 0; i < GameData.playerData.Count; i++)
        {
            microwavePlayer[i] = (GameObject)Instantiate(microwavePrefab[GameData.playerData[i].microwaveSelected],spawnPosition[i].position, Quaternion.identity);
            microwavePlayer[i].GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControlRewired>().playerId = i;
        }*/
        //Spawn players


        //Countdown

        //Gameplay

        //Ending

        //Leaderboard

        //Reload menu
        yield return null;
    }

    // Update is called once per frame
    void Update () {
		// Debug vari
	}

    /// <summary>
    /// Add one kill to the designated player
    /// </summary>
    /// <param name="playerId">The player that will receive a kill count</param>
    public void AddPlayerKill(int playerId)
    {
        for (int i = 0; i < GameData.playerData.Count; i++)
        {
            if (GameData.playerData[i].playerId == playerId)
            {
                GameData.playerData[i].kills++;
            }
        }
    }
}
