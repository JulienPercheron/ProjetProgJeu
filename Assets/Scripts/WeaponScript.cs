using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : PowerupScript
{
    GameObject bullet;

    bool goRight;

    void Start()
    {
        bullet = Resources.Load<GameObject>("Prefab/Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(bullet);
            goRight = this.GetComponent<PlayerScript>().facingRight;
            if (goRight)
            {
                go.transform.position = new Vector3(transform.position.x + 1, transform.position.y+1, transform.position.z);
            }
            else
            {
                go.transform.position = new Vector3(transform.position.x - 1, transform.position.y+1, transform.position.z);
            }

            go.GetComponent<BulletScript>().goRight = goRight;
        }
    }
}
