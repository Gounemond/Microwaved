using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    public bool invertRotation = false;
    public bool copyOnlyY = false;
    public Transform objectToCopy;

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update ()
    {
        if (copyOnlyY)
        {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, objectToCopy.transform.rotation.y, this.transform.rotation.z);
        }
        else
        {
            if (invertRotation)
            {
                this.transform.rotation = objectToCopy.rotation;
                // THE FUCK HOW DO I INVERT SIGN
            }
            else
            {
                this.transform.rotation = objectToCopy.rotation;
            }
        }
        
    }
}
