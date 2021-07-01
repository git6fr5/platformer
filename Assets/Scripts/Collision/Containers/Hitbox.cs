using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Container that handles being attacked
public class Hitbox : Container2D
{
    public Hitbox() {}

    /* --- COMPONENTS --- */
    public Status2D state;

    /* --- VARIABLES --- */
    public bool canCollect;

    /* --- OVERRIDE --- */
    public override void OnAdd(Collider2D collider) {
    }

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

    public void Collect(Collectible collectible) {
        if (canCollect) { print("Collected something"); }
        if (state.bagpack == null) { return; }
        state.bagpack.Add(collectible);
        //state.inventory = 
    }
}
