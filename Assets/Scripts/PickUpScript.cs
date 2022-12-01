using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PowerupScript))]
public class PickUpScript : MonoBehaviour
{

    PowerupScript power;

    private void Start()
    {
        power = this.GetComponent<PowerupScript>();
    }


    private void Update()
    {
        this.transform.Rotate(0, 0.1f, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.AddComponent(power.GetType());
            
            Destroy(this.gameObject);
        }
    }
}
