using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public int StartingItems;
    public float MaxTime;
    public float MinTime;

    public GameObject[] PickUps;

    private Transform[] spawnPoints;

    private float currentTime;
    private bool randomTry = true;
	// Use this for initialization
	void Awake ()
    {
        spawnPoints = transform.GetComponentsInChildren<Transform>();
        StartCoroutine(readyRandom());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (randomTry)
        {
            if (Random.value < (currentTime - MinTime) / (MaxTime - MinTime))
                spawn();
            randomTry = false;
        }
        currentTime += Time.deltaTime;

	}

    private void spawn()
    {
        bool exit;
        Vector3 pos;
        int count = 0;
        do
        {
            exit = true;
            pos = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
            Collider[] col = Physics.OverlapSphere(pos, 1);
            foreach (Collider c in col)
            {
                if (c.tag == "PickUp")
                {
                    exit = false;
                }
            }
            count++;
        } while (exit == false || count < 30);
        if(exit && count < 30)
            Instantiate(PickUps[Random.Range(0, PickUps.Length)], pos, Quaternion.identity);
        currentTime = 0;
    }

    public void Start()
    {
        for (int i = 0; i < StartingItems; i++)
            spawn();
    }

    IEnumerator readyRandom()
    {
        yield return new WaitForSeconds(Mathf.Min(1, MaxTime- currentTime));
        randomTry = true;
        StartCoroutine(readyRandom());
    }

}
