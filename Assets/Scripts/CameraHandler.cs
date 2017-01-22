using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public LayerMask layerMask;

    private GameObject[] players;
    private Transform[] playerPositions;

    private Vector3 centerOfPlayers;

    private Vector3 currentVelocity;

    private float smooth = 0.3f;
    private float planeSmooth = 0.1f;

    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        playerPositions = new Transform[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            playerPositions[i] = players[i].transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = new float();
        float y = new float();
        float z = new float();
        float angle = new float();
        RaycastHit info;
        foreach (Transform t in playerPositions)
        {
            x += t.position.x;
            y += t.position.y;
            z += t.position.z;
        }
        x /= playerPositions.Length;
        y /= playerPositions.Length;
        z /= playerPositions.Length;
        angle = (180 - transform.rotation.eulerAngles.x) * Mathf.Deg2Rad;

        //move on the plane
        Vector3 dir = new Vector3(0, Mathf.Sin(angle), Mathf.Cos(angle));
        Physics.Raycast(new Ray(new Vector3(x, y, z), dir), out info, Mathf.Infinity, layerMask);
        //Debug.DrawLine(new Vector3(x, y, z), info.point, Color.red);
        //Debug.DrawRay(new Vector3(x, y, z), dir, Color.red, 30);
        transform.position = Vector3.SmoothDamp(transform.position, info.point, ref currentVelocity, smooth);

        //move the plane

        float maxXY = 0;
        float minXY = Mathf.Infinity;
        float maxYZ = 0;
        float minYZ = Mathf.Infinity;
        Vector3 planeVelocity = new Vector3();
        foreach (Transform t in playerPositions)
        {
            float a = Mathf.Atan2(transform.position.y, t.position.x - transform.position.x);
            if (a > maxXY)
                maxXY = a;
            if (a < minXY)
                minXY = a;
            float b = Mathf.Atan2(transform.position.y, t.position.z - transform.position.z);
            if (b > maxYZ)
                maxYZ = b;
            if (b < minYZ)
                minYZ = b;
        }
        maxXY *= Mathf.Rad2Deg;
        maxYZ *= Mathf.Rad2Deg;
        minXY *= Mathf.Rad2Deg;
        minYZ *= Mathf.Rad2Deg;
        //Debug.Log("MaxXY:  " + maxXY + "   MinXY:   " + minXY + "  MaxYZ:  " + maxYZ + "  MinYZ:   " + minYZ);

        //precedenza all'allontanamento
        if (maxXY > 130 || minXY < 50 || maxYZ > 70 || minYZ < 50) //Danger zone
        {
            if (maxXY > 138 || minXY < 42)
            {
                transform.position = Vector3.SmoothDamp(transform.position, transform.position - transform.forward * 3, ref planeVelocity, planeSmooth);
            }

            if (maxYZ > 78 || minYZ < 43)
            {
                transform.position = Vector3.SmoothDamp(transform.position, transform.position - transform.forward * 3, ref planeVelocity, planeSmooth);
            }
        }
        else
        {
            //avvicinamento
            /*if (maxXY < 135 || minXY > )
                transform.position = Vector3.SmoothDamp(transform.position, transform.position + transform.forward * 3, ref planeVelocity, planeSmooth);

            if (maxYZ < 130 || minYZ > 95)
                transform.position = Vector3.SmoothDamp(transform.position, transform.position + transform.forward * 3, ref planeVelocity, planeSmooth);
        */
            transform.position = Vector3.SmoothDamp(transform.position, transform.position + transform.forward * 3, ref planeVelocity, planeSmooth);
        }
        

    }
}
