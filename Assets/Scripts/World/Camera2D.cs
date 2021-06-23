using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : Container2D
{
    public Transform follow;
    public Rigidbody2D body;

    void Update() {
        body.velocity = (Vector3)(Vector2)follow.position-  transform.position;
    }

    public override void OnRemove(Collider2D collider) {
        
    }

}
