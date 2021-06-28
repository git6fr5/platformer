using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Outputs the commands
public class Output2D : MonoBehaviour
{
    /* --- DEBUG --- */
    public bool doDebug = false;
    string debugTag = "[Output2D]: ";

    /* --- COMPONENTS --- */
    public Input2D input;
    public Status2D state;
    public Rigidbody2D body;

    /* --- VARIABLES --- */
    public bool damp = true;

    /* --- UNITY --- */
    void Start() { 
    }

    void Update() {
        // reset
        //Reset();
        // dashing
        if (!state.justDashed && input.dash != 0) { Dash(); }
        // jumping
        if (state.onGround && !state.justJumped && input.jump) { Jump(); }
        // slam
        if (input.slam) { Slam(); }
        // attacking
        if (state.weapon != null && !state.weapon.attacking && input.attack) { Attack(); }
        if (state.secondaryWeapon != null && !state.secondaryWeapon.attacking && input.attack2) { SecondaryAttack(); }
        // misc
        if (damp) { Damp(); }
    }

    /* --- METHODS --- */
    void Dash() {    
        if (doDebug) { print(debugTag + "Dashing"); }
        // adjust the animation
        state.character.SetAnimation(state.character.dashAnimation);
        if (state.weapon != null && !state.weapon.point) { state.transform.right = Vector2.right * input.dash; }
        // add the force
        body.velocity = new Vector2(0, body.velocity.y);
        body.AddForce(new Vector2(input.dash * state.dashForce * state.dashMultiplier, 0));
        state.justDashed = true;
    }

    void Jump() {
        if (doDebug) { print(debugTag + "Jumping"); }
        // adjust the animation
        state.character.jumpParticle?.Fire();
        // add the force
        body.velocity = new Vector2(body.velocity.x, 0);
        body.AddForce(new Vector2(0, state.jumpForce));
        state.justJumped = true;
    }

    void Slam() {
        if (doDebug) { print(debugTag + "Slamming"); }
        // adjust the animation
        state.character.crouchParticle?.Fire();
        // add the force
        body.AddForce(new Vector2(0, -state.slamForce));
    }

    void Attack() {
        if (doDebug) { print(debugTag + "Attacking"); }
        state.weapon.Activate(input.targetPoint);
    }

    void SecondaryAttack() {
        if (doDebug) { print(debugTag + "Secondary Attack"); }
        state.secondaryWeapon.Activate(input.targetPoint);
    }

    void Damp() { 
        body.velocity = new Vector2(body.velocity.x * 0.999f, body.velocity.y);
        if (body.velocity.y > 20f){
            body.velocity = new Vector2(body.velocity.x, 10f);
        }
    }

    void Reset() {
        state.character.SetAnimation(null);
        state.character.SetMaterial(null);
        if (state.weapon.point) { Point(); }
    }

    void Point() { 
        // point
    }
}
