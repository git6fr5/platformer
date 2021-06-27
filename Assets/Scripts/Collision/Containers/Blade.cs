using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : Container2D
{
    /* --- CLASS --- */
    public Blade() {
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
        // hit the hitbox
        hitbox.Hurt(weapon.attackDamage);
        hitbox.Knockback(weapon.knockbackForce, hitbox.transform.position - weapon.hand.transform.position);
    }
}
