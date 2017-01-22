using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.parent.GetComponent<PlayerHitController>().HitYou(200, Vector3.up * 10000, -1);
        }
    }
}
