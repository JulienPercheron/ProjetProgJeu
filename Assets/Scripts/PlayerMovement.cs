using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpHeight = 5.0f;
    

    bool jumping;
    bool grounded;
    float horizontalMovement;

    Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody>();
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

        horizontalMovement = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalMovement = horizontalMovement * Time.deltaTime * speed;
        myRigidbody.position = new Vector3(myRigidbody.position.x + horizontalMovement, myRigidbody.position.y, myRigidbody.position.z);

        if (jumping && grounded)
        {
            myRigidbody.AddForce(0.0f, jumpHeight, 0.0f, ForceMode.Impulse);
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

    public bool IsJumping()
    {
        return !grounded;
    }
}
