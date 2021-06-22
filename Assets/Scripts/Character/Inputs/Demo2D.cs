using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo2D : Input2D
{
    /* --- COMPONENTS --- */
    public Container2D vision;

    /* --- VARIABLES --- */
    Collider2D target;
    float buffer = 1f;

    /* --- OVERRIDE --- */
    public override void GetInput()
    {
        if (vision.container.Count == 0) {
            dash = 0;
            jump = false;
            crouch = false;
            damp = false;
            return;
        }
        else {
            target = vision.container[0];
            dash = Mathf.Sign(target.transform.position.x - transform.position.x);
            if (target.transform.position.y > transform.position.y + buffer){
                jump = true;
            }
            else { jump = false; }
            if (target.transform.position.y < transform.position.y -buffer) {
                crouch = true;
            }
            else { crouch = false; }
        }
    }
}
