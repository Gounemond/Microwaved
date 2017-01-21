using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int WeaponIndex;

    public int Pick()
    {
        Destroy(this.gameObject, .1f);
        return WeaponIndex;
    }

}
