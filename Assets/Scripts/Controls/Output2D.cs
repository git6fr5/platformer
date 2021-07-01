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

    /* --- VARIABLES --- */
    public bool damp = true;

    /* --- UNITY --- */
    void Start() { 
    }

    void Update() {
        ProcessInput();
    }

    /* --- METHODS --- */
    void ProcessInput() {
        // dashing
        if (!state.justDashed && input.dash != 0) { Dash(); }
        else if (input.dash == 0) { state.dashing = false; }
        // jumping
        if (state.onGround && !state.justJumped && input.jump) { Jump(); }
        // slam
        if (input.slam) { Slam(); }
        // attacking
        if (state.weapon != null && !state.weapon.attacking && input.attack) { Attack(); }
        if (state.secondaryWeapon != null && !state.secondaryWeapon.attacking && input.attack2) { SecondaryAttack(); }
        // inventory
        if (state.bagpack != null && state.bagpack.gameObject.activeSelf != input.bagpack) { Bagpack(); }
        // misc
        if (damp) { Damp(); }
    }

    /* --- ACTIONS --- */
    void Dash() {    
        if (doDebug) { print(debugTag + "Dashing"); }
        // adjust the animation
        if (state.weapon != null && !state.weapon.point) { state.transform.right = Vector2.right * input.dash; }
        // add the force
        state.body.velocity = new Vector2(0, state.body.velocity.y);
        state.body.AddForce(new Vector2(input.dash * state.dashForce * state.dashMultiplier, 0));
        state.justDashed = true;
    }

    void Jump() {
        if (doDebug) { print(debugTag + "Jumping"); }
        // adjust the animation
        state.character.jumpParticle?.Fire();
        // add the force
        state.body.velocity = new Vector2(state.body.velocity.x, 0);
        state.body.AddForce(new Vector2(0, state.jumpForce));
        state.justJumped = true;
    }

    void Slam() {
        if (doDebug) { print(debugTag + "Slamming"); }
        // adjust the animation
        state.character.crouchParticle?.Fire();
        // add the force
        state.body.AddForce(new Vector2(0, -state.slamForce));
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
        state.body.velocity = new Vector2(state.body.velocity.x * 0.999f, state.body.velocity.y);
        if (state.body.velocity.y > 20f){
            state.body.velocity = new Vector2(state.body.velocity.x, 10f);
        }
    }

    /* --- INVENTORY --- */
    void Bagpack() {
        state.bagpack.gameObject.SetActive(input.bagpack);
    }
}
