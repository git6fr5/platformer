using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : Input2D
{
    /* --- OVERRIDE --- */
    public override void GetInput() {
        // movement
        dash = Input.GetAxisRaw("Horizontal");
        // jumping
        if (Input.GetAxisRaw("Vertical") > 0) {
            jump = true;
        }
        else {
            jump = false;
        }
        // crouching
        if (Input.GetAxisRaw("Vertical") < 0) {
            crouch = true;
        }
        else {
            crouch = false;
        }
    }
}
