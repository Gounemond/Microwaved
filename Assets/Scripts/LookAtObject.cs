using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour {

    public bool invert = false;
    public Transform objectToPoint;

    // Use this for initialization
    void Start()
    {
        if (invert)
        {
            transform.LookAt(objectToPoint);
            transform.Rotate(new Vector3(0, 180, 0));
        }
        else
        {
            transform.LookAt(objectToPoint);
        }
    }
}
