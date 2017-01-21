using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopCorn : Weapon
{
    [SerializeField]
    protected float burstTime;
    [SerializeField]
    protected int numberOfPops;

    public GameObject Pop;

    private int currentPop;

    private int playerId;

    IEnumerator burst()
    {
        GameObject pop = Instantiate(Pop, transform.position, transform.rotation);
        pop.GetComponent<PopCornPop>().SetParameters(damage, speed, knockbackForce);
        pop.GetComponent<PopCornPop>().SetPlayerId(playerId);
        yield return new WaitForSeconds(burstTime / numberOfPops);
        currentPop++;
        if (currentPop < numberOfPops)
            StartCoroutine(burst());
    }


    protected override void fire()
    {
        StartCoroutine(burst());
        Destroy(gameObject, 5f);
    }

}
