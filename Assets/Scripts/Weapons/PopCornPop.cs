using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopCornPop : MonoBehaviour
{
    protected float speed;
    protected float knockbackForce;

    protected int damage;

    public int PlayerId;
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    public void SetParameters(int d, float s, float k)
    {
        speed = s;
        damage = d;
        knockbackForce = k;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player") || other.tag == ("Wall"))
        {
            if (other.tag == ("Player"))
            {
                Vector3 hitDirection = (other.transform.position - transform.position);
                hitDirection.Normalize();
                other.transform.parent.parent.GetComponent<PlayerHitController>().HitYou(damage, hitDirection * knockbackForce, PlayerId);
            }
            Destroy(gameObject);
        }
    }

    public void SetPlayerId(int id)
    {
        PlayerId = id;
    }
}
