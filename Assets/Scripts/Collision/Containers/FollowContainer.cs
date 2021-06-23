using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Container that follows a target
public class FollowContainer : Container2D
{
    public Collider2D follow;
    public Collider2D bound;
    public Rigidbody2D body;
    public float inSpeed = 0f;
    public float outSpeed = 20f;

    void Update() {
        float x = inSpeed;
        if (!container.Contains(follow)) { x = outSpeed;  }
        body.velocity = x * ((Vector3)(Vector2)follow.transform.position-  (transform.position + (Vector3)bound.offset)).normalized;
    }

}
