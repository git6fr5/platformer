using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike2D : Container2D
{
    /* --- VARIABLES --- */
    public int damage;
    public float forceMagnitude = 500f;

    /* --- OVERRIDE --- */
    public override void OnAdd(Collider2D collider) {
        if (collider.GetComponent<HitBox2D>() == null) { return; }
        print("hello");
        HitBox2D hitBox = collider.GetComponent<HitBox2D>();
        hitBox.Hurt(damage);
        hitBox.Knockback(forceMagnitude, Vector2.up);
    }
}
