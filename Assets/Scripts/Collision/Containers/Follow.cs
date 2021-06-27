using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Container that follows a target
public class Follow : Container2D
{
    /* --- COMPONENTS --- */
    public Collider2D follow;
    public Collider2D bound;
    public Rigidbody2D body;

    /* --- VARIABLES --- */
    // speed
    [Space(5)][Header("Speed")]
    public float inSpeed = 0f;
    public float outSpeed = 20f;

    /* --- UNITY --- */
    void Update() {
        Vector3 pos = follow.transform.position - (Vector3)bound.offset;
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        //Move();
    }

    void Move() {
        // decide what speed to move at
        float x = inSpeed;
        if (!container.Contains(follow)) { x = outSpeed; }
        // move the body
        Vector3 position = transform.position + (Vector3)bound.offset;
        Vector2 direction = (Vector2)(follow.transform.position - position).normalized;
        body.velocity = x * direction;
    }
}
