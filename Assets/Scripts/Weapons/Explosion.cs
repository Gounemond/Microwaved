﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        GetComponent<ParticleSystem>().Play();
        Destroy(gameObject, 2f);
	}
	
}
