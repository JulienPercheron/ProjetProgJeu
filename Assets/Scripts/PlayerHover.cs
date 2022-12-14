using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerScript))]
public class PlayerHover : MonoBehaviour
{
    private PlayerScript _movement;
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
        _movement = GetComponent<PlayerScript>();
        _rb = GetComponent<Rigidbody>();
        gliderMesh.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_movement.IsJumping() && Input.GetAxisRaw("Fire3") > 0 && !isGlide)
        {
            isGlide = true;
        }
        
        if(Input.GetAxisRaw("Fire3") == 0 && isGlide)
        {
            isGlide = false;
        }

        //Si le joueur utilise son glider on vide la jauge endurance au fil du temps
        if(isGlide && glideTime < glideMaxTime){
            glideTime += Time.deltaTime;
            if (glideTime >= glideMaxTime)
            {
                glideTime = glideMaxTime;
                _rb.useGravity = true;
                canGlide = false;
            }
            
            loadingCircle.fillAmount = 1 - (glideTime / glideMaxTime);

            gliderMesh.gameObject.SetActive(true);

            _rb.velocity = new Vector3(Mathf.Clamp(_rb.velocity.x, 0,  4), -glideSpeed);
        }
        
        //Rempli la jauge d'endurance si le joueur n'est pas en train d'utiliser son glider
        if(!isGlide && glideTime > 0 && _movement.characterState == PlayerAction.grounded)
        {
            glideTime -= Time.deltaTime * 0.6f;
            loadingCircle.fillAmount = 1 - (glideTime / glideMaxTime);
            gliderMesh.gameObject.SetActive(false);
            _rb.useGravity = false;
        }

        //Remet la gravite si la jauge d'endurance est vide
        if (isGlide && glideTime >= glideMaxTime)
        {
            isGlide = false;
            
        }
    }
}
