using System;
using UnityEngine;
using Rewired;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControlRewired : MonoBehaviour
    {
        public int playerId = 0;     // Rewired player of this controller
        private Player rewiredPlayer;

        private CarController m_Car; // the car controller we want to use


        private void Start()
        {
            // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
            rewiredPlayer = ReInput.players.GetPlayer(playerId);


            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = rewiredPlayer.GetAxis("Steering");
            float v = rewiredPlayer.GetAxis("Moving") ;
#if !MOBILE_INPUT
            float handbrake = rewiredPlayer.GetAxis("Handbrake");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
