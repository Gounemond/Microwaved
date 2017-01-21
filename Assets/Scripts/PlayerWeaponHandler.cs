using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerWeaponHandler : MonoBehaviour
{
    public GameObject[] Weapons;

    [SerializeField]
    private float cookLevel;
    [SerializeField]
    private float cookSpeed;

    private int currentWeaponIndex; //-1 for no weapon

    private Transform weaponShootSpawn;

    private Player _player;

    private bool canHatch = true;
    private bool cooked = false;

    private float currentCookLevel;
	// Use this for initialization
	void Awake ()
    {
        _player = ReInput.players.GetPlayer(GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControlRewired>().playerId);
        weaponShootSpawn = transform.FindChild("WeaponShootSpawn");
        currentWeaponIndex = -1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_player.GetButtonDown("Attacking") && canHatch)
            openHatch();
        if (_player.GetButtonDown("Cooking") && currentWeaponIndex != -1)
            cook();
	}

    void cook ()
    {
        if (!cooked)
        {
            currentCookLevel += cookSpeed * Time.deltaTime;
            if (currentCookLevel > cookLevel)
                cooked = true;
        }
    }

    void openHatch()
    {
        if(currentWeaponIndex == -1)
        {
            //TODO sportellata normale
            Collider[] col = Physics.OverlapBox(weaponShootSpawn.position, new Vector3(1,1,1));
            foreach(Collider c in col)
            {
                if (c.tag == "PickUp")
                {
                    currentWeaponIndex = c.gameObject.GetComponent<PickUp>().Pick();
                }
            }
        }
        else
        {
            if (cooked)
            {
                GameObject go;
                if (currentWeaponIndex != 4)
                {
                    go = Instantiate(Weapons[currentWeaponIndex], weaponShootSpawn.position, weaponShootSpawn.rotation);
                    go.GetComponent<Weapon>().SetPlayerId(_player.id);
                    
                }
                else
                {
                    go = Instantiate(Weapons[currentWeaponIndex], weaponShootSpawn.position, weaponShootSpawn.rotation, transform);
                    go.GetComponent<Weapon>().SetPlayerId(_player.id);
                }
            }
            removeWeapon();
        }
        //currentWeaponIndex = (currentWeaponIndex + 1) % 6;
    }

    void removeWeapon ()
    {
        currentWeaponIndex = -1;
        cooked = false;
    }
}
