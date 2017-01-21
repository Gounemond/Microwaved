using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PodPlayerController : MonoBehaviour {

    public event ActionInt OnMicrowaveSelected;                     // This event is triggered everytime a microwave is selected

    public int microwaveClassSelected;

    // Callback signature
    // Has: Return of type 'void' and 1 parameter of type 'string'
    public delegate void ActionInt(int index);

    public Animator podAnimator;
    public AnimationClip podTurningAnim;

    public Transform microwaveSpawnLocation1;
    public Transform microwaveSpawnLocation2;
    public GameObject playerMicrowave1;
    public GameObject playerMicrowave2;

    public GameObject[] microwavePrefab;

    [Header ("Rewired stuff")]
    public int playerId = 0;     // Rewired player of this controller

    private Player _rewiredPlayer;

    private bool _podAnimEnded = true;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        _rewiredPlayer = ReInput.players.GetPlayer(playerId);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _podAnimEnded)
        //if (_rewiredPlayer.GetButton("Attacking") && _podAnimEnded)
        {
            SelectNextMicrowave();
        }
	}

    IEnumerator TurnPlatform()
    {
        _podAnimEnded = false;
        podAnimator.SetTrigger("Turn");
        yield return new WaitForSeconds(podTurningAnim.length);
        _podAnimEnded = true;
    }

    public void SelectNextMicrowave()
    {
        // Cycle to the next microwave class
        if (microwaveClassSelected == (int)GameEnums.microwaveClasses.MASTERCHEF)
        {
            microwaveClassSelected = (int)GameEnums.microwaveClasses.NORMIE;
        }
        else
        {
            microwaveClassSelected++;
        }

        if (microwaveSpawnLocation1.position.y > microwaveSpawnLocation2.position.y)
        {
            Destroy(playerMicrowave2);
            playerMicrowave2 = (GameObject)Instantiate(microwavePrefab[microwaveClassSelected]);
            playerMicrowave2.transform.position = microwaveSpawnLocation2.position;
            playerMicrowave2.transform.rotation = microwaveSpawnLocation2.rotation;
            playerMicrowave2.transform.parent = microwaveSpawnLocation2;
        }
        else
        {
            Destroy(playerMicrowave1);
            playerMicrowave1 = (GameObject)Instantiate(microwavePrefab[microwaveClassSelected]);
            playerMicrowave1.transform.position = microwaveSpawnLocation1.position;
            playerMicrowave1.transform.rotation = microwaveSpawnLocation1.rotation;
            playerMicrowave1.transform.parent = microwaveSpawnLocation1;
        }

        StartCoroutine(TurnPlatform());
    }


}
