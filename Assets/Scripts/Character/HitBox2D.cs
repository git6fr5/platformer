using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox2D : Container2D
{
    /* --- COMPONENTS --- */
    public Status2D state;

    /* --- METHODS --- */
    public void Hurt(int damage) {
        if (!state.justHurt) {
            // hurt them
            if (doDebug) { print(debugTag + "Ouch! " + name + " was hurt."); }
            state.currHealth -= damage;
            state.justHurt = true;
        }
    }

    public void Knockback(float magnitude, Vector2 direction) {
        if (!state.justKnocked) {
            state.body.velocity = Vector2.zero;
            state.body.AddForce(magnitude * direction.normalized);
            state.justKnocked = true;
        }
    }
}
