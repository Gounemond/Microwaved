using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField]
    private int hp;

    private Rigidbody rigid;

	// Use this for initialization
	void Awake ()
    {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HitYou(int damage, Vector3 knockBack, int player)
    {
        if (knockBack.y < 0)
        {
            knockBack.y = 0;
        }
        Debug.Log(knockBack);
        rigid.AddForce(knockBack, ForceMode.Impulse);
        hp -= damage;
        if (hp <= 0)
            die(player);
    }

    void die(int player)
    {
        Debug.Log("DIE! DIE! DIE!");
        BattleArenaManager.instance.AddPlayerKill(player);
        
        //respawn
    }
}
