using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArenaElements : MonoBehaviour {

    // Singleton implementation
    protected static BattleArenaElements _instance;
    public static BattleArenaElements instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(BattleArenaElements)) as BattleArenaElements;
            return _instance;
        }
    }
    public Transform[] spawnPosition;
    public Transform[] spawnPositionUnderground;

    [Header("Audio stuff")]
    public AudioSource casterAudioSource;
    public AudioSource dingAudioSource;
    public AudioClip vo_Three;
    public AudioClip vo_Two;
    public AudioClip vo_One;
    public AudioClip vo_LetsMicrowave;

    public AudioClip vo_Microwaved;
}
