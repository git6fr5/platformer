using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Input2D
{
    /* --- COMPONENTS --- */
    public Vision vision;
    public Arena arena;

    /* --- VARIABLES --- */
    public float followRadius = 1.5f;
    Hitbox target;
    float buffer = 1f;

    /* --- OVERRIDE --- */
    public override void GetInput()
    {
        if (vision.target == null) {
            Stay();
        }
        else {
            if (Vector2.Distance(vision.target.transform.position, transform.position) > followRadius) {
                Follow(vision.target);
            }
            else { Stay(); }
        }
    }

    /* --- METHODS --- */
    void Follow(Hitbox target) {
        dash = Mathf.Sign(target.transform.position.x - transform.position.x);
        if (target.transform.position.y > transform.position.y + buffer){
            jump = true;
        }
        else { jump = false; }
        if (target.transform.position.y < transform.position.y -buffer) {
            slam = true;
        }
        else { slam = false; }
    }

    void Stay() {
        dash = 0;
        jump = false;
        slam = false;
    }

    void Path() {

        int[] targetPoint = arena.PointToGrid(vision.target.transform.position);
        int[] currPoint = arena.PointToGrid(transform.position);

    }
}
