using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dildo : Weapon
{
    private Collider oldCollider;

    private Animator anim;
    // Update is called once per frame
    void Update ()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime,Space.World);
    }

    protected override void fire()
    {
        direction = transform.forward;
        anim = GetComponentInChildren<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == ("Player"))
        {
            //TODO danno diretto
            audioSource.clip = HitAudio;
            audioSource.Play();
            anim.SetTrigger("Hit");
            //Destroy(gameObject, 5f);

            playerHit(other);
            Destroy(gameObject);
        }

        if (other.tag == ("Wall"))
        {
            if (other != oldCollider)
            {
                oldCollider = other;
                Vector3 newDir = Vector3.Reflect(transform.forward, other.transform.forward);
                transform.rotation = Quaternion.LookRotation(newDir, transform.up);
                audioSource.clip = HitAudio;
                audioSource.Play();
            }
        }


    }


}
