using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHook : MonoBehaviour
{

    public bool Debug = false;
    
    public float HookableRadius = 10.0f;
    public Material HookMaterial;
    
    private Rigidbody _rb;
    
    private bool isHook = false;
    private bool canHook = false;
    private List<GameObject> hookObjects;

    private SpringJoint _springJoint;
    private LineRenderer _lineRenderer;

    private PlayerScript _playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerMovement = GetComponent<PlayerScript>();
        hookObjects = new();
    }

    private void OnDrawGizmos()
    {
        if (!Debug) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, HookableRadius);
    }

    // Update is called once per frame
    void Update()
    {
        CheckingHookablePlatform();

        //Update line renderer for the hook
        if (isHook)
        {
            _lineRenderer.SetPosition(0, _rb.position + new Vector3(0, 1f, 0));
        }
        
        if (canHook && !isHook && Input.GetAxis("Fire2") > 0)
        {
            SetupHook();

            _playerMovement.enabled = false;
            
            isHook = true;
        }
    
        
        if (isHook && Input.GetAxis("Fire2") == 0)
        {   
            isHook = false;

            _playerMovement.enabled = true;
            
            Destroy(_lineRenderer);
            Destroy(_springJoint);
        }
        
        
    }

    void SetupHook()
    {
        _springJoint = gameObject.AddComponent<SpringJoint>();
        _lineRenderer = gameObject.AddComponent<LineRenderer>();

        _springJoint.autoConfigureConnectedAnchor = false;
        _springJoint.connectedBody = hookObjects[0].GetComponent<Rigidbody>();

        _springJoint.axis = new Vector3(1, 1, 1);
            
        float dist = Vector3.Distance(_rb.position, hookObjects[0].transform.position);
            
        _springJoint.minDistance = dist * 0.8f;
        _springJoint.maxDistance = dist * 0.25f;

        _springJoint.spring = 4.5f;
        _springJoint.damper = 7f;
        _springJoint.massScale = 4.5f;
            
        _lineRenderer.SetPosition(0, _rb.position + new Vector3(0,1f,0));
        _lineRenderer.SetPosition(1, hookObjects[0].transform.position);

        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;

        if(HookMaterial != null)
            _lineRenderer.material = HookMaterial;
    }
    
    void CheckingHookablePlatform()
    {
        hookObjects.Clear();
        
        Collider[] hitColliders;
        hitColliders = Physics.OverlapSphere(_rb.transform.position, HookableRadius);
        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.CompareTag("Hookable"))
                {
                    canHook = true;
                    hookObjects.Add(collider.gameObject);
                }
            }
        }
        
        canHook = hookObjects.Count > 0;

    }
    
}
