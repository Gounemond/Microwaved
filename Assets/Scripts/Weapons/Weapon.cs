using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AudioClip StartAudio;
    public AudioClip HitAudio;

    protected AudioSource audioSource;

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float knockbackForce;

    [SerializeField]
    protected int damage;

    protected Vector3 direction;



    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    void Start()
    {
        audioSource.clip = StartAudio;
        audioSource.Play();
        fire();
    }

    protected virtual void fire()
    {

    }

}
