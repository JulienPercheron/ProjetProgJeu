using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public PlayerScript playerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sol" || other.gameObject.tag == "Plateform" || other.gameObject.tag == "MoveablePlateform")
        {
            playerScript.characterState = PlayerAction.grounded;
            playerScript.SetGrounded(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sol" || other.gameObject.tag == "Plateform" || other.gameObject.tag == "MoveablePlateform")
        {
            playerScript.SetGrounded(false);
            playerScript.characterState = PlayerAction.Jumping;
        }
    }
}
