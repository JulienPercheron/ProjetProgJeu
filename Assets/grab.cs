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
            script.SetHung();
            Debug.Log("GARFIELD");
        }
    }

    private void OnTriggerExit(Collider plateform)
    {
        if (plateform.tag == "Plateform" && (script.characterState == PlayerAction.Jumping || script.characterState == PlayerAction.grounded))
        {
            script.SetGrounded(false);
        }
    }
}
