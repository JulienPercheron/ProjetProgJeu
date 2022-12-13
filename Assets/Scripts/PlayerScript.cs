using System;
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

public enum PlayerAction
{
    Jumping, grounded, Climbing, Hung
}

    private float _fallMultiplier;
    private float _lowJumpMultiplier;
    public PlayerAction characterState;
    private bool jumping;
    private bool grounded;
    float horizontalMovement;
     
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
        if (characterState == PlayerAction.Hung)
        {
            if (Input.GetButtonDown("Jump"))
            {
                characterState = PlayerAction.Jumping;
                jumping = true;

            }



        }
        if (characterState != PlayerAction.Climbing && characterState != PlayerAction.Hung)
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }

        horizontalMovement = Input.GetAxis("Horizontal");
        animator.SetFloat("Velocity", Mathf.Abs(horizontalMovement));

    }




    // Update is called once per frame
    void FixedUpdate()
    {

    
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






        

        IEnumerator ClimbingCoroutine()
        {

            yield return new WaitForSeconds(2.5f);

            transform.position = new Vector3(transform.position.x + 0.25f, transform.position.y + 2.50f, transform.position.z);
            myRigidbody.useGravity = true;
            characterState = PlayerAction.grounded;



    }





    private void Climbs()
    {
        characterState = PlayerAction.Climbing;
        //animation here;
        StartCoroutine(ClimbingCoroutine());


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sol"  || other.tag == "Plateform")
        {
            characterState = PlayerAction.grounded;
            grounded = true;
            animator.SetBool("Jumping", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sol" || other.tag == "Plateform")
        {

            grounded = false;
            animator.SetBool("Jumping", true);
            characterState =  PlayerAction.Jumping;
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




