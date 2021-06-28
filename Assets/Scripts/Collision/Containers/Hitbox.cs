using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Container that handles being attacked
public class Hitbox : Container2D
{
    /* --- COMPONENTS --- */
    public Status2D state;
    public Particle hurtParticle;

    /* --- METHODS --- */
    public void Hurt(int damage) {
        if (!state.justHurt) {
            if (doDebug) { print(debugTag + "Ouch! " + name + " was hurt."); }
            state.currHealth -= damage;
            state.justHurt = true;
        }
    }

    public void Knockback(float magnitude, Vector2 direction) {
        if (!state.justKnocked) {
            // knock the player back
            state.body.velocity = Vector2.zero;
            state.body.AddForce(magnitude * direction.normalized);
            state.justKnocked = true;
        }
    }
}
