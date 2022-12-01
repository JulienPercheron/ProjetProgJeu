using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 5.0f;
    public bool goRight;

    private void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (goRight)
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }
        else
        {
            transform.position -= transform.right * Time.deltaTime * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
