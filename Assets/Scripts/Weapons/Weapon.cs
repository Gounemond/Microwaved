using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AudioClip StartAudio;
    public AudioClip HitAudio;

    public int PlayerId;

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
        Destroy(this.gameObject, 20f);
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

    protected virtual void playerHit(Collider other)
    {
        Vector3 hitDirection = (other.transform.position - transform.position);
        hitDirection.Normalize();
        other.transform.parent.parent.GetComponent<PlayerHitController>().HitYou(damage, hitDirection * knockbackForce, PlayerId);
        Debug.Log(hitDirection * knockbackForce);
    }

    protected virtual void playerHit(Collider other, int d, float kb)
    {
        Vector3 hitDirection = (other.transform.position - transform.position);
        hitDirection.Normalize();
        other.transform.parent.parent.GetComponent<PlayerHitController>().HitYou(d, hitDirection * kb, PlayerId);
        Debug.Log(hitDirection * kb);
    }

    public void SetPlayerId(int id)
    {
        PlayerId = id;
    }

}
