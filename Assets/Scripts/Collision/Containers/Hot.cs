using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hot : Container2D
{
    /* --- CLASS --- */
    public Hot() {
        containerTags = new string[] { "Hitbox" };
    }

    /* --- COMPONENTS --- */

    /* --- VARIABLES --- */
    public int damage = 2;
    public float knockbackForce = 500f;

    /* --- UNITY --- */
    void OnTriggerStay2D(Collider2D collider) { 
        if (collider.tag == "Hitbox") {
            Hitbox hitbox = collider.GetComponent<Hitbox>();
            Hit(hitbox);
        }
    }

    /* --- OVERRIDE --- */
    public override void OnAdd(Collider2D collider) {
        
    }

    /* --- VIRTUAL --- */
    public virtual void Hit(Hitbox hitbox) {
        // hit the hitbox
        hitbox.Hurt(damage);
        hitbox.Knockback(knockbackForce, Vector3.up);
    }
}
