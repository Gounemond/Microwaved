using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : Weapon
{

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    protected override void fire()
    {
        direction = transform.forward;
        Destroy(gameObject, 10f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            audioSource.clip = HitAudio;
            audioSource.Play();
            //TODO danno diretto
        }
        if (other.tag == ("Wall"))
        {
            Destroy(gameObject,5f);
        }
    }
}
