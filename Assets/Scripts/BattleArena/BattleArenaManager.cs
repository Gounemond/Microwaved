using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BattleArenaManager : MonoBehaviour
{

    public static BattleArenaManager instance;

    public Color[] playerColor;

    public GameObject[] microwavePrefab;

    public GameObject[] microwavePlayer;

    

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

        // Spawn microwaves and assign them the players
        for (int i = 0; i < GameData.playerData.Count; i++)
        {
            microwavePlayer[i] = (GameObject)Instantiate(microwavePrefab[GameData.playerData[i].microwaveSelected], BattleArenaElements.instance.spawnPosition[i].position, Quaternion.identity);
            microwavePlayer[i].GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControlRewired>().playerId = i;
        }
    }

    IEnumerator Start()
    {
        // Assign player colors and apply kinematic to keep them steady
        for (int i = 0; i < GameData.playerData.Count; i++)
        {
            //microwavePlayer[i].GetComponent<Renderer>().material.color = playerColor[i];
            microwavePlayer[i].GetComponent<Rigidbody>().isKinematic = true;
        }

        yield return StartCoroutine(CountDown());

        // Remove kinematic from players to let them move
        for (int i = 0; i < GameData.playerData.Count; i++)
        {
            microwavePlayer[i].GetComponent<Rigidbody>().isKinematic = false;
        }


        //Gameplay

        //Ending

        //Leaderboard

        //Reload menu
        yield return null;
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(2);
        // CountDown with voiceover and GUI
        // GUI Call
        BattleArenaElements.instance.casterAudioSource.PlayOneShot(BattleArenaElements.instance.vo_Three);
        yield return new WaitForSeconds(1);

        // GUI Call
        BattleArenaElements.instance.casterAudioSource.PlayOneShot(BattleArenaElements.instance.vo_Two);
        yield return new WaitForSeconds(1);

        // GUI Call
        BattleArenaElements.instance.casterAudioSource.PlayOneShot(BattleArenaElements.instance.vo_One);
        yield return new WaitForSeconds(1);

        // GUI Call
        BattleArenaElements.instance.casterAudioSource.PlayOneShot(BattleArenaElements.instance.vo_LetsMicrowave);
    }

    // Update is called once per frame
    void Update () {
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
