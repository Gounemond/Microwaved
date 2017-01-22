using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class BattleArenaManager : MonoBehaviour
{

    public static BattleArenaManager instance;

    public Color[] playerColor;

    public GameObject[] microwavePrefab;

    public GameObject[] microwavePlayer;

    public PlayerDisk[] playerBobs;

    

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
            playerBobs[i].SetPlayerToFollowe(microwavePlayer[i].transform);
            playerBobs[i].GetComponent<SpriteRenderer>().color = playerColor[i];
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
        yield return new WaitForSeconds(180);

        //Ending
        SceneManager.LoadScene(0);

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
