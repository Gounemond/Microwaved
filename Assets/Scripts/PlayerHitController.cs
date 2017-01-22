using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitController : MonoBehaviour
{
    public GameObject Explosion;
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
        Instantiate(Explosion, transform.position, Quaternion.identity);
        if(player!= -1)
            BattleArenaManager.instance.AddPlayerKill(player);
        StartCoroutine(respawn());
        //respawn
    }

    IEnumerator respawn()
    {
        int rnd = Random.Range(0, 4);
        /*transform.position = BattleArenaElements.instance.spawnPositionUnderground[rnd].position;
        yield return new WaitForSeconds(2f);*/
        transform.position = BattleArenaElements.instance.spawnPosition[rnd].position;
        hp = 100;
        yield return 0;
    }
}
