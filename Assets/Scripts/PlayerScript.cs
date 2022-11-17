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

    Rigidbody myRigidbody;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }

        horizontalMovement = Input.GetAxis("Horizontal");
        animator.SetFloat("Velocity", Mathf.Abs(horizontalMovement));

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newHorizontalMovement = horizontalMovement * Time.fixedDeltaTime * speed;
        myRigidbody.position = new Vector3(myRigidbody.position.x + newHorizontalMovement, myRigidbody.position.y, myRigidbody.position.z);
        
        if(horizontalMovement > 0.1)
        {
            transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }
        else if(horizontalMovement < -0.1)
        {
            transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }

        

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
}
