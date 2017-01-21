using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PodPlayerController : MonoBehaviour {

    public event ActionInt OnMicrowaveSelected;                     // This event is triggered everytime a microwave is selected

    // Callback signature
    // Has: Return of type 'void' and 1 parameter of type 'string'
    public delegate void ActionInt(int index);

    public Animator podAnimator;
    public AnimationClip podTurningAnim;

    public int playerId = 0;     // Rewired player of this controller
    private Player rewiredPlayer;

    private bool _podAnimEnded = true;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        rewiredPlayer = ReInput.players.GetPlayer(playerId);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (rewiredPlayer.GetButton("Attacking") && _podAnimEnded)
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

        StartCoroutine(TurnPlatform());
    }


}
