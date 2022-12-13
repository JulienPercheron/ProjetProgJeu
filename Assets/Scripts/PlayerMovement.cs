using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : EntityScript
{
    public GameObject loseScreen;

    public float jumpHeight = 5.0f;

    public bool facingRight = true;


    bool jumping;
    bool grounded;
    float horizontalMovement;
     

    // Start is called before t he first frame update
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
            facingRight = true;
        }
        else if(horizontalMovement < -0.1)
        {
            transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            facingRight = false;
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
            animator.SetBool("Jumping", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sol")
        {
            grounded = false;
            animator.SetBool("Jumping", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            this.PointsDeVie -= 1;
            if (this.PointsDeVie <= 0)
            {
                loseScreen.SetActive(true);
                Destroy(this.gameObject);
            }
        }
    }

    public bool IsJumping()
    {
        return !grounded;
    }
}
