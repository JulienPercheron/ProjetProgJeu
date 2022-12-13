using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab : MonoBehaviour
{
    [SerializeField] BoxCollider armsCollider;
    [SerializeField] PlayerScript script;
    // Start is called before the first frame update



    private void OnTriggerEnter(Collider plateform)
    {
        if(plateform.tag == "Plateform" && (script.characterState == PlayerAction.Jumping || script.characterState == PlayerAction.grounded) )
        {
            script.myRigidbody.velocity = Vector3.zero;
            script.myRigidbody.useGravity = false;
            script.characterState = PlayerAction.Hung;

        }
    }
}
