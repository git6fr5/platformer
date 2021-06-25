using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Input2D
{
    /* --- VARIABLES --- */
    public float doubleTapBuffer = 0.2f;
    public KeyCode rightDashKey = KeyCode.D;
    public KeyCode leftDashKey = KeyCode.A;
    public KeyCode jumpKey = KeyCode.W;
    public KeyCode slamKey = KeyCode.S;
    public KeyCode crouchKey = KeyCode.LeftShift;
    public KeyCode quickDashKey = KeyCode.Space;
    public int attackButton = 0;
    public int secondaryAttackButton = 1;

    /* --- OVERRIDE --- */
    public override void GetInput() {
        // dash 
        if (Input.GetKey(rightDashKey)) { dash = 1; }
        else if (Input.GetKey(leftDashKey)) { dash = -1; }
        else { dash = 0; }
        // quick dash
        if (Input.GetKeyDown(quickDashKey)) { quickDash = true; }
        else { quickDash = false; }
        // jumping 
        if (Input.GetKey(jumpKey)) { jump = true; }
        else { jump = false; }
        // slow fall
        if (Input.GetKey(slamKey)) { slam = true; }
        else { slam = false; }
        // crouching
        if (Input.GetKey(crouchKey)) { crouch = true; }
        else { crouch = false; }
        // attacking
        if (Input.GetMouseButtonDown(attackButton)) { 
            attack = true; 
        }
        else { attack = false; }
        if (Input.GetMouseButtonDown(secondaryAttackButton)) { 
            attack2 = true; 
        }
        else { attack2 = false; }
        targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
