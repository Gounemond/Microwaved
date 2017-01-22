using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aluminum : Weapon
{
    public GameObject Explosion;
    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private int explosionDamage;
    [SerializeField]
    private float explosionKnockBack;
    [SerializeField]
    private float rollSpeed;


    private Transform visualEffect;

    private Animator anim;
    // Update is called once per frame
    void Update ()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        roll();
	}

    protected override void fire()
    {
        direction = transform.forward;
        visualEffect = transform.FindChild("VisualEffect");
        anim = GetComponentInChildren<Animator>();

    }

    void roll()
    {
        visualEffect.Rotate(transform.right, rollSpeed * Time.deltaTime);
    }

    void explode()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        speed = 0;
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        anim.SetTrigger("Hit");
        //Destroy(gameObject, 5f);
        foreach(Collider c in hits)
        {
            if(c.tag== "Player")
            {
                playerHit(c,explosionDamage, explosionKnockBack);
            }
        }
        Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player") || other.tag == ("Wall"))
        {
            explode();
            audioSource.clip = HitAudio;
            audioSource.Play();
            if(other.tag == "Player")
                playerHit(other);
        }
    }
}
