using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : Weapon
{
    [SerializeField]
    private float rotationValue;
    [SerializeField]
    private float rotationSpeed;

    private Transform visualEffect;

    private Animator anim;

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        transform.Rotate(transform.up, rotationValue * Time.deltaTime);
        visualEffect.Rotate(visualEffect.up, rotationSpeed * Time.deltaTime);
        
    }

    protected override void fire()
    {
        direction = transform.forward;
        visualEffect = transform.FindChild("VisualEffect");
        anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player") || other.tag == ("Wall"))
        {
            audioSource.clip = HitAudio;
            audioSource.Play();
            anim.SetTrigger("Hit");
            Destroy(gameObject, 5f);
            //TODO danno diretto
        }
    }
}
