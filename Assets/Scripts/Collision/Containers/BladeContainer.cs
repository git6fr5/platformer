using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeContainer : Container2D
{
    /* --- VARIABLES --- */
    public Weapon2D weapon;
    public Collider2D blade;

    /* --- UNITY --- */
    void Update() { 
        if (weapon.attacking) {
            blade.enabled = true;
        }
        else {
            blade.enabled = false;
        }
    }

    /* --- OVERRIDE --- */
    public override void OnAdd(Collider2D collider) {
        if (collider.GetComponent<HitboxContainer>() == null) { return; }
        HitboxContainer hitbox = collider.GetComponent<HitboxContainer>();
        hitbox.Hurt(weapon.attackDamage);
        hitbox.Knockback(weapon.knockbackForce, collider.transform.position - weapon.hand.position);
    }
}
