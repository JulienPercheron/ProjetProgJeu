using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerHover : MonoBehaviour
{
    private PlayerMovement _movement;
    private Rigidbody _rb;

    public Image loadingCircle;

    public float glideMaxTime = 2.0f;
    public float glideSpeed = 2.0f;
    public float horizontalHoverSpeed = 1.0f;

    private float glideTime;

    private bool canGlide = true;
    private float glideCooldownTimer = 0;
    private float glideCooldown = 0.3f;

    private bool isGlide = false;

    public MeshRenderer gliderMesh;
    
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_movement.IsJumping() && Input.GetKeyDown(KeyCode.LeftShift) && !isGlide)
        {
            isGlide = true;
        }
        
        if(Input.GetKeyUp(KeyCode.LeftShift) && isGlide)
        {
            isGlide = false;
        }

        if(isGlide){
            glideTime += Time.deltaTime;
            if (glideTime > glideMaxTime)
            {
                glideTime = 0;
                isGlide = false;
            }

            loadingCircle.fillAmount = 1 - (glideTime / glideMaxTime);
            _rb.useGravity = false;
            gliderMesh.gameObject.SetActive(true);

            _rb.velocity = new Vector3(Mathf.Clamp(_rb.velocity.x, 0,  4), -glideSpeed);
        }
        else
        {
            gliderMesh.gameObject.SetActive(false);
            canGlide = false;
            _rb.useGravity = true;
            glideTime = 0;
            loadingCircle.fillAmount = 1 - (glideTime / glideMaxTime);
        }
    }
}
