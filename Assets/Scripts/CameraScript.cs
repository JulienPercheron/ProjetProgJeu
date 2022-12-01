using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float eloignement = 4.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.y += 1.0f;
        pos.z -= eloignement;
        transform.position = pos;
    }
}
