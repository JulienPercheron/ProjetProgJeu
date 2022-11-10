using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpHeight = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = this.transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        this.transform.position = new Vector3(x, this.transform.position.y, this.transform.position.z);

        if (Input.GetButtonDown("Jump"))
        {
            this.GetComponent<Rigidbody>().AddForce(0.0f, jumpHeight, 0.0f, ForceMode.Impulse);
        }
    }
}
