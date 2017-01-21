﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Weapon
{
    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private float deceleration;

    public GameObject TestSphere;

    private Transform visualEffect;

    private Collider oldCollider;

    private Animator anim;
	
	// Update is called once per frame
	void Update ()
    {
        if (speed > 0)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            speed -= deceleration * Time.deltaTime;
        }
        else
            speed = 0;
    }

    protected override void fire()
    {
        direction = transform.forward;
        visualEffect = transform.FindChild("VisualEffect");
        anim = GetComponentInChildren<Animator>();

    }

    void explode()
    {
        speed = 0;
        Instantiate(TestSphere, transform.position, Quaternion.identity, transform);
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        //TODO Danneggia i giocatori

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            explode();
            audioSource.clip = HitAudio;
            audioSource.Play();
            anim.SetTrigger("Hit");
            Destroy(gameObject, 5f);
            //TODO danno diretto
        }
        if (other.tag == ("Wall"))
        {
            if (other != oldCollider)
            {
                oldCollider = other;
                Vector3 newDir = Vector3.Reflect(transform.forward, other.transform.forward);
                transform.rotation = Quaternion.LookRotation(newDir, transform.up);
            }
        }
    }
}