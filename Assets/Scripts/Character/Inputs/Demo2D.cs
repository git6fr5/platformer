using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo2D : Input2D
{
    /* --- COMPONENTS --- */
    public Container2D vision;

    /* --- VARIABLES --- */
    public float followRadius = 1.5f;
    Collider2D target;
    float buffer = 1f;

    /* --- OVERRIDE --- */
    public override void GetInput()
    {
        if (vision.container.Count == 0) {
            Stay();
        }
        else {
            target = vision.container[0];
            if (Vector2.Distance(target.transform.position, transform.position) > followRadius) {
                Follow(target);
            }
            else { Stay(); }
        }
    }

    /* --- METHODS --- */
    void Follow(Collider2D target) {
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

    void Stay() {
        dash = 0;
        jump = false;
        crouch = false;
        damp = false;
    }
}
