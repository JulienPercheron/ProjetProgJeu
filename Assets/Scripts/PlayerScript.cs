using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction
{
    Jumping, grounded, Climbing, Hung
}

public class PlayerScript : EntityScript
{
    public GameObject loseScreen;

    public float jumpHeight = 5.0f;

    public bool facingRight = true;


    private float _fallMultiplier;
    private float _lowJumpMultiplier;
    public PlayerAction characterState;
    private bool jumping;

    [SerializeField]
    private bool grounded;
    float horizontalMovement;
    
    public float TestConstant = 100;

    private GameObject onPlatform;
    
    // Start is called before t he first frame update
    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody>();
        animator = this.GetComponentInChildren<Animator>();
    
        characterState = PlayerAction.grounded;
        _fallMultiplier = 2.5f;
        _lowJumpMultiplier = 2f;
    }


    private void Update()
    {
            if (Input.GetButtonDown("Jump"))
            {
                characterState = PlayerAction.Jumping;
                jumping = true;
                myRigidbody.useGravity = true;
            }



        }
        if (characterState != PlayerAction.Climbing && characterState != PlayerAction.Hung)
        {
            if(Input.GetAxisRaw("Jump") > 0)
            {
                jumping = true;
            }
            if (Input.GetAxisRaw("Jump") == 0)
            {
                jumping = false;
            }

        if (characterState != PlayerAction.Hung)
        {
    
        
            horizontalMovement = Input.GetAxis("Horizontal");

            if (horizontalMovement > 0.1)
            {
                transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                facingRight = true;
            }
            else if (horizontalMovement < -0.1)
            {
                transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
                facingRight = false;
            }

            animator.SetFloat("Velocity", Mathf.Abs(horizontalMovement));

        }

    }




    // Update is called once per frame
    void FixedUpdate()
    {
        if (onPlatform)
        {
            myRigidbody.velocity += onPlatform.GetComponent<Rigidbody>().velocity;
        }

        if (characterState != PlayerAction.Hung){
            
            horizontalMovement = horizontalMovement * Time.deltaTime * speed;
            myRigidbody.position = new Vector3(myRigidbody.position.x + horizontalMovement, myRigidbody.position.y, myRigidbody.position.z);


            if (jumping && grounded)
            {
                myRigidbody.AddForce(0.0f, jumpHeight, 0.0f, ForceMode.Impulse);
            }

            if (myRigidbody.velocity.y < 0)
            {
                myRigidbody.velocity += Vector3.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;

            }
            else if (myRigidbody.velocity.y > 0 && !(Input.GetButtonUp("Jump")))
            {

                myRigidbody.velocity = Vector3.up * Mathf.Clamp(myRigidbody.velocity.y + Vector3.up.y * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime, -30f, 15f);

            }
        }
      
    }






        




    public void SetGrounded(bool grounded)
    {
        this.grounded = grounded;
        animator.SetBool("Jumping", !grounded);
    }

    public void SetHung()
    {
        if(other.gameObject.tag == "Sol"  || other.tag == "Plateform" || other.tag == "MoveablePlateform")
        {
            characterState = PlayerAction.grounded;
            grounded = true;
            animator.SetBool("Jumping", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Sol"  || other.tag == "Plateform" || other.tag == "MoveablePlateform")
        {
            if (other.tag == "MoveablePlateform")
                onPlatform = other.gameObject;
            else
                onPlatform = null;

            characterState = PlayerAction.grounded;
            grounded = true;
            animator.SetBool("Jumping", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sol" || other.tag == "Plateform" || other.tag == "MoveablePlateform")
        {
            grounded = false;
            animator.SetBool("Jumping", true);
            characterState =  PlayerAction.Jumping;
        }
    }
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.useGravity = false;
        grounded = true;
        characterState = PlayerAction.Hung;
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




