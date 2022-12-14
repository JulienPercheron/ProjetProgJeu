using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public PlayerScript playerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sol" || other.gameObject.tag == "Plateform")
        {
            playerScript.characterState = PlayerAction.grounded;
            playerScript.SetGrounded(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sol" || other.gameObject.tag == "Plateform")
        {
            playerScript.SetGrounded(false);
            playerScript.characterState = PlayerAction.Jumping;
        }
    }
}
