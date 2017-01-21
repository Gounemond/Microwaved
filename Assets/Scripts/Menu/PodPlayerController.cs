using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PodPlayerController : MonoBehaviour {

    public event ActionInt OnMicrowaveSelected;                       // This event is called when the player has confirmed his microwave

    public int microwaveClassSelected = -1;

    // Callback signature
    // Has: Return of type 'void' and 1 parameter of type 'string'
    public delegate void ActionInt(int playerId, int indexMicrowave);

    public Animator podAnimator;
    public AnimationClip podTurningAnim;

    public Transform microwaveSpawnLocation1;
    public Transform microwaveSpawnLocation2;
    public GameObject playerMicrowave1;
    public GameObject playerMicrowave2;

    public AudioSource podMechanism;
    public AudioSource microwaveEngine;
    public AudioSource microwaveCooking;


    public GameObject[] microwavePrefab;

    [Header ("Rewired stuff")]
    public int playerId = 0;     // Rewired player of this controller

    private Player _rewiredPlayer;

    private bool _podAnimEnded = true;
    private bool _selectionConfirmed = false;

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
        // If the player hasn't confirmed his selection
        if (!_selectionConfirmed)
        {
            // Cycle through microwaves if the platform is not rotating
            if (_rewiredPlayer.GetButton("Attacking") && _podAnimEnded)
            {
                SelectNextMicrowave();
            }

            if (_rewiredPlayer.GetButton("Cooking") && _podAnimEnded)
            {
                microwaveCooking.Play();
                _selectionConfirmed = true;

                if (OnMicrowaveSelected != null)
                {
                    OnMicrowaveSelected(playerId, microwaveClassSelected);
                }
            }
        }
	}

    IEnumerator TurnPlatform()
    {
        _podAnimEnded = false;
        podAnimator.SetTrigger("Turn");
        podMechanism.Play();
        yield return new WaitForSeconds(podTurningAnim.length);
        microwaveEngine.Play();
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

        // Checks the position "on bottom" of the pod and spawns there the selected microwave
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

        // After having the microwave spawned, turns the platform
        StartCoroutine(TurnPlatform());
    }


}
