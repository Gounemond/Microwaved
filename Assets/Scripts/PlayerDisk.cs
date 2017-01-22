using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisk : MonoBehaviour
{

    private bool follow = false;

    public Transform target;
	
	// Update is called once per frame
	void Update ()
    {
        if(follow)
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }

    public void SetPlayerToFollowe(Transform t)
    {
        target = t;
        follow = true;
    }
}
