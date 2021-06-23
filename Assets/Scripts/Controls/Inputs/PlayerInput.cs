using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Input2D
{
    /* --- VARIABLES --- */
    public float doubleTapBuffer = 0.2f;
    public string rightDashKey;
    public string leftDashKey;
    public string jumpKey;
    public string crouchKey;
    public int attackButton;

    /* --- OVERRIDE --- */
    public override void GetInput() {
        // movement 
        if (Input.GetKey(rightDashKey)) { dash = 1; }
        else if (Input.GetKey(leftDashKey)) { dash = -1; }
        else { dash = 0; }
        // jumping 
        if (Input.GetKeyDown(jumpKey)) { jump = true; }
        else { jump = false; }
        // crouching
        if (Input.GetKey(crouchKey)) { crouch = true; }
        else { crouch = false; }
        // attacking
        if (Input.GetMouseButtonDown(attackButton)) { attack = true; }
        else { attack = false; }
    }
}
