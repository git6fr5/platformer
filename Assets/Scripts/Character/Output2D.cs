using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Output2D : MonoBehaviour
{
    /* --- DEBUG --- */
    public bool doDebug = false;
    string debugTag = "[Output2D]: ";

    /* --- COMPONENTS --- */
    public Input2D input;
    public Status2D state;
    public Rigidbody2D body;

    /* --- UNITY --- */
    void Start() { 
    }

    void Update() {
        Dash();
        Jump();
        Crouch();
        Damp();
    }

    /* --- METHODS --- */
    void Dash() { 
        if (state.canDash && input.dash != 0) {
            if (doDebug) { print(debugTag + "Dashing"); }
            body.velocity = new Vector2(0, body.velocity.y);
            body.AddForce(new Vector2(input.dash * state.dashForce, 0));
            state.justDashed = true;
        }
    }

    void Jump() {
        if (state.canJump && input.jump) {
            if (doDebug) { print(debugTag + "Jumping"); }
            body.velocity = new Vector2(body.velocity.x, 0);
            body.AddForce(new Vector2(0, state.jumpForce));
            state.justJumped = true;
        }
        if (state.jumpPeaking) { body.gravityScale = state.jumpPeakingScale; }
        else if (state.jumpFalling) { body.gravityScale = state.jumpFallingScale; }
        else { body.gravityScale = 1f; }
    }

    void Crouch() {
        if (state.canCrouch && (input.crouch || state.stickyCrouch)) {
            if (doDebug) { print(debugTag + "Crouching"); }
            body.AddForce(new Vector2(0, -state.crouchForce));
        } 
    }

    void Damp() { 
        if (input.damp) {
            body.velocity = new Vector2(body.velocity.x * 0.95f, body.velocity.y);
        }
    }
}
