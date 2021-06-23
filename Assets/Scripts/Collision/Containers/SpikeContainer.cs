using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeContainer : Container2D
{
    /* --- VARIABLES --- */
    public int damage;
    public float forceMagnitude = 500f;

    /* --- OVERRIDE --- */
    public override void OnAdd(Collider2D collider) {
        if (collider.GetComponent<HitboxContainer>() == null) { return; }
        print("hello");
        HitboxContainer hitbox = collider.GetComponent<HitboxContainer>();
        hitbox.Hurt(damage);
        hitbox.Knockback(forceMagnitude, Vector2.up);
    }
}
