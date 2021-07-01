using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : Container2D
{
    /* --- CLASS --- */
    public Blade() {
        containerTags = new string[] { "Hitbox", "Arena" };
    }

    /* --- COMPONENTS --- */
    public Weapon2D weapon;
    public Hitbox ownHitbox;

    /* --- VARIABLES --- */
    public int bladeLength;
    public int bladeBreadth;

    /* --- UNITY --- */

    /* --- OVERRIDE --- */
    public override void OnAdd(Collider2D collider) {
        if (collider.tag == "Hitbox") {
            Hitbox hitbox = collider.GetComponent<Hitbox>();
            if (hitbox != ownHitbox) {
                Hit(hitbox);
            }
        }
        else if (collider.tag == "Arena") {
            Arena arena = collider.GetComponent<Arena>();
            Cut(arena);
        }
    }

    /* --- VIRTUAL --- */
    public virtual void Hit(Hitbox hitbox) {
        // hit the hitbox
        hitbox.Hurt(weapon.attackDamage);
        hitbox.Knockback(weapon.knockbackForce, hitbox.transform.position - weapon.hand.transform.position);
    }

    public virtual void Cut(Arena arena) {
        print("Cutting arena");
        arena.CutGrid((Vector2)transform.position, bladeBreadth, bladeLength);
    }
}
