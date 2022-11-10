using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpHeight = 5.0f;
    

    bool jumping;
    bool grounded;
    float horizontalMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumping = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalMovement = this.transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        this.transform.position = new Vector3(horizontalMovement, this.transform.position.y, this.transform.position.z);

        if (jumping && grounded)
        {
            this.GetComponent<Rigidbody>().AddForce(0.0f, jumpHeight, 0.0f, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sol")
        {
            grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sol")
        {
            grounded = false;
        }
    }
}
