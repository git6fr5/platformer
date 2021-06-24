using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Input2D
{
    /* --- VARIABLES --- */
    public float doubleTapBuffer = 0.2f;
    public KeyCode rightDashKey = KeyCode.D;
    public KeyCode leftDashKey = KeyCode.A;
    public KeyCode quickDashKey = KeyCode.LeftShift;
    public KeyCode jumpKey = KeyCode.W;
    public KeyCode crouchKey = KeyCode.S;
    public int attackButton = 0;

    /* --- OVERRIDE --- */
    public override void GetInput() {
        // movement 
        if (Input.GetKey(rightDashKey)) { dash = 1; }
        else if (Input.GetKey(leftDashKey)) { dash = -1; }
        else { dash = 0; }
        if (Input.GetKeyDown(quickDashKey)) { quickDash = true; }
        else { quickDash = false; }
        // jumping 
        if (Input.GetKeyDown(jumpKey)) { jump = true; }
        else { jump = false; }
        // crouching
        if (Input.GetKey(crouchKey)) { crouch = true; }
        else { crouch = false; }
        // attacking
        if (Input.GetMouseButtonDown(attackButton)) { 
            attack = true; 
            targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else { attack = false; }
    }
}
