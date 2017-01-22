using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityStandardAssets.Vehicles.Car;

public class PlayerWeaponHandler : MonoBehaviour
{
    public GameObject[] Weapons;
    public GameObject BoneWithHatchAnimator;
    public GameObject ObjectWithOwnCollider;
    public AudioClip CookingInProgress;
    public AudioClip CookingDone;
    public AudioClip HatchOpenSound;

    [SerializeField]
    private float cookLevel;
    [SerializeField]
    private float cookSpeed;
    [SerializeField]
    private float knockbackForce;

    private int currentWeaponIndex; //-1 for no weapon

    private Transform weaponShootSpawn;

    private Player _player;

    private bool canHatch = true;
    private bool cooked = false;

    private Animator anim;

    private Collider ownCollider;

    private float currentCookLevel;

    private AudioSource audSource;

	// Use this for initialization
	void Awake ()
    {

        weaponShootSpawn = transform.FindChild("WeaponShootSpawn");
        currentWeaponIndex = -1;
        anim = BoneWithHatchAnimator.GetComponent<Animator>();
        ownCollider = ObjectWithOwnCollider.GetComponent<Collider>();
        audSource = GetComponent<AudioSource>();
	}

    void Start()
    {
        _player = ReInput.players.GetPlayer(GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControlRewired>().playerId);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_player.GetButton("Attacking") && canHatch)
        {
            openHatch();
        }
        if (_player.GetButton("Cooking") && currentWeaponIndex != -1)
            cook();
        else
            audSource.Stop();
        if (!canHatch)
            hatchIsOpen();
	}

    void cook ()
    {
        if (!cooked)
        {
            currentCookLevel += cookSpeed * Time.deltaTime;
            audSource.clip = CookingInProgress;
            audSource.loop = true;
            audSource.Play();
            if (currentCookLevel > cookLevel)
            {
                cooked = true;
                audSource.clip = CookingDone;
                audSource.loop = false;
                audSource.Play();
                currentCookLevel = 0;
            }

        }
    }

    void openHatch()
    {
        audSource.loop = false;
        audSource.clip = HatchOpenSound;
        audSource.Play();
        StartCoroutine(resetHatch());
        hatchIsOpen();
        Collider[] col = Physics.OverlapBox(weaponShootSpawn.position, new Vector3(1, 1, 2));
        foreach (Collider c in col)
        {
            if (c.tag == "Player" && c != ownCollider)
            {
                Vector3 hitDirection = transform.forward;
                /*Vector3 hitDirection = (c.transform.position - transform.position);
                hitDirection.Normalize();*/
                c.transform.parent.parent.GetComponent<PlayerHitController>().HitYou(1, hitDirection * knockbackForce, _player.id);
                Debug.Log(hitDirection * knockbackForce);
            }
        }
        canHatch = false;
        if(currentWeaponIndex != -1)
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
                removeWeapon();
            }

        }
        //currentWeaponIndex = (currentWeaponIndex + 1) % 6;
    }

    void hatchIsOpen()
    {
        if (currentWeaponIndex == -1)
        {
            //TODO sportellata normale
            Collider[] col = Physics.OverlapBox(weaponShootSpawn.position, new Vector3(1, 1, 2));
            foreach (Collider c in col)
            {
                if (c.tag == "PickUp")
                {
                    currentWeaponIndex = c.gameObject.GetComponent<PickUp>().Pick();
                }
                if (c.tag == "Player" && c.gameObject != gameObject)
                {

                }
            }
        }
    }

    void removeWeapon ()
    {
        currentWeaponIndex = -1;
        cooked = false;
    }

    IEnumerator resetHatch()
    {
        anim.SetBool("Open", true);
        yield return new WaitForSeconds(2);
        anim.SetBool("Open", false);
        canHatch = true;
    }
}
