using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Input2D
{
    /* --- VARIABLES --- */
    public KeyCode rightDashKey = KeyCode.D;
    public KeyCode leftDashKey = KeyCode.A;
    public KeyCode jumpKey = KeyCode.W;
    public KeyCode slamKey = KeyCode.S;
    public int attackButton = 0;
    public int secondaryAttackButton = 1;

    /* --- OVERRIDE --- */
    public override void GetInput() {
        // dash 
        if (Input.GetKey(rightDashKey)) { dash = 1; }
        else if (Input.GetKey(leftDashKey)) { dash = -1; }
        else { dash = 0; }
        // jumping 
        if (Input.GetKey(jumpKey)) { jump = true; }
        else { jump = false; }
        // slam
        if (Input.GetKey(slamKey)) { slam = true; }
        else { slam = false; }
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
