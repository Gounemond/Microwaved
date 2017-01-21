using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopCorn : Weapon
{
    
    protected override void fire()
    {
        ParticleSystem p = GetComponentInChildren<ParticleSystem>();
        p.Play();
        Destroy(gameObject, 5f);
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player")
        {
            //TODO danno al giocatore
        }
        audioSource.clip = HitAudio;
        audioSource.Play();
    }
}
