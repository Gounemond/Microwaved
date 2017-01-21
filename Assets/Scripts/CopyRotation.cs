using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    public bool invertRotation = false;
    public Transform objectToCopy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (invertRotation)
        {
            this.transform.rotation = objectToCopy.rotation * Quaternion.Euler(0,-1,0);
        }
        else
        {
            this.transform.rotation = objectToCopy.rotation;
        }
    }
}
