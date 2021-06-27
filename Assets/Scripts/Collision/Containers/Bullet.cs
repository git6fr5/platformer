using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Container2D
{
    /* --- CLASS --- */
    public Bullet() {
        containerTags = new string[] { "Hitbox" };
    }

    /* --- VARIABLES --- */
    public Weapon2D weapon;

    /* --- UNITY --- */

    /* --- OVERRIDE --- */
    public override void OnAdd(Collider2D collider) {
        Hitbox hitbox = collider.GetComponent<Hitbox>();
        Hit(hitbox);
    }

    /* --- VIRTUAL --- */
    public virtual void Hit(Hitbox hitbox) {      
    }
}
