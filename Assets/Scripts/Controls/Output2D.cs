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
        // dashing
        if (!state.justDashed && input.dash != 0) { Dash(); }
        if (!state.quickDashing && input.quickDash) { QuickDash(); }
        // jumping
        if (state.onGround && !state.crouching && !state.justJumped && input.jump) { Jump(); }
        //if (state.jumping && state.justJumped && input.jump) { DoubleJump(); }
        // crouching
        if (input.crouch || state.stickyCrouch) { Crouch(); }
        else if (!input.crouch && state.crouching) { Uncrouch(); }
        // slam
        if (input.slam) { Slam(); }
        // attacking
        if (state.character.weapon != null && !state.character.weapon.attacking && input.attack) { Attack(); }
        // misc
        if (damp) { Damp(); }
        Renderer();
    }

    /* --- METHODS --- */
    void Dash() {    
        if (doDebug) { print(debugTag + "Dashing"); }
        body.velocity = new Vector2(0, body.velocity.y);
        body.AddForce(new Vector2(input.dash * state.dashForce * state.dashMultiplier, 0));
        state.justDashed = true;
    }

    void QuickDash() {
        if (doDebug) { print(debugTag + "Quick Dashing"); }
        state.character.characterRenderer.quickDashParticle?.Fire();
        state.quickDashing = true;
    }

    void Jump() {
        if (doDebug) { print(debugTag + "Jumping"); }
        state.character.characterRenderer.jumpParticle?.Fire();
        body.velocity = new Vector2(body.velocity.x, 0);
        body.AddForce(new Vector2(0, state.jumpForce));
        state.justJumped = true;
    }

    void DoubleJump() { 
        if (doDebug) { print(debugTag + "Double Jumping"); }
        state.character.characterRenderer.doubleJumpParticle?.Fire();
        body.velocity = new Vector2(body.velocity.x, 0);
        body.AddForce(new Vector2(0, state.jumpForce));
        state.doubleJumping = true;
        state.jumping = false;
    }

    void Crouch() {
        if (doDebug) { print(debugTag + "Crouching"); }
        body.velocity = new Vector2(body.velocity.x * 0.95f, body.velocity.y );
        state.character.mesh.enabled = false;
        state.crouching = true;
    }
    
    void Uncrouch() {
        state.character.mesh.enabled = true;
        state.crouching = false;
    }

    void Slam() {
        if (doDebug) { print(debugTag + "Slamming"); }
        if (!state.crouching) { state.character.characterRenderer.crouchParticle?.Fire(); }
        body.AddForce(new Vector2(0, -state.crouchForce));
        state.crouching = true;
    }

    void Attack() {
        if (doDebug) { print(debugTag + "Attacking"); }
        state.character.weapon.Activate(input.targetPoint);
    }

    void Damp() { 
        body.velocity = new Vector2(body.velocity.x * 0.999f, body.velocity.y);
        if (body.velocity.y > 20f){
            body.velocity = new Vector2(body.velocity.x, 10f);
        }
    }

    void Renderer() {
        if (input.dash != 0) {
            state.character.transform.right = new Vector2(input.dash, 0);
            state.character.characterRenderer.SetAnimation(state.character.characterRenderer.dashAnimation);
        }
        else {
            state.character.characterRenderer.SetAnimation(null);
        }
        if (state.justHurt) {
            state.character.characterRenderer.SetMaterial(state.character.characterRenderer.hurtMaterial);
        }
        else {
            state.character.characterRenderer.SetMaterial(null);
        }
    }
}
