using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aluminum : Weapon
{
    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private float explosionDamage;
    [SerializeField]
    private float rollSpeed;

    public GameObject TestSphere;

    private Transform visualEffect;

    private Animator anim;
    // Update is called once per frame
    void Update ()
    {
        transform.Translate(direction * speed * Time.deltaTime);
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
        speed = 0;
        Instantiate(TestSphere, transform.position, Quaternion.identity, transform);
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        anim.SetTrigger("Hit");
        Destroy(gameObject, 5f);
        //TODO Danneggia i giocatori

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player") || other.tag == ("Wall"))
        {
            explode();
            audioSource.clip = HitAudio;
            audioSource.Play();
            //TODO danno diretto
        }
    }
}
