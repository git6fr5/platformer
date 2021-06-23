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
    public Character2D character;

    /* --- UNITY --- */
    void Start() { 
    }

    void Update() {
        Dash();
        Jump();
        Crouch();
        Damp();
        Renderer();
    }

    /* --- METHODS --- */
    void Dash() {
        if (RightRoll()) { return; }
        if (LeftRoll()) { return; }
        if (state.canDash && input.dash != 0) {
            if (doDebug) { print(debugTag + "Dashing"); }
            body.velocity = new Vector2(0, body.velocity.y);
            body.AddForce(new Vector2(input.dash * state.dashForce, 0));
            state.justDashed = true;
        }
    }

    bool RightRoll() {
        if (state.canDash && input.rightRoll) {
            if (doDebug) { print(debugTag + "Rolling"); }
            body.AddForce(Vector2.right * 1000f);
            input.rightRoll = false;
            return true;
        }
        return false;
    }

    bool LeftRoll() {
        if (state.canDash && input.leftRoll) {
            if (doDebug) { print(debugTag + "Rolling"); }
            body.AddForce(-Vector2.right * 1000f);
            input.leftRoll = false;
            return true;
        }
        return false;
    }

    void Jump() {
        if (DoubleJump()) { return; }
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

    bool DoubleJump() { 
        if (state.jumping && input.doubleJump) {
            if (doDebug) { print(debugTag + "Double Jumping"); }
            body.velocity = new Vector2(body.velocity.x, 0);
            body.AddForce(new Vector2(0, state.jumpForce));
            state.doubleJumping = true;
            state.jumping = false;
            return true;
        }
        return false;
    }

    void Crouch() {
        if (Slam()) { return; }
        if (state.canCrouch && (input.crouch || state.stickyCrouch)) {
            if (doDebug) { print(debugTag + "Crouching"); }
            body.AddForce(new Vector2(0, -state.crouchForce));
            body.velocity = new Vector2(body.velocity.x * 0.95f, body.velocity.y);
        } 
    }

    bool Slam() { 
        if (state.canCrouch && input.slam) {
            if (doDebug) { print(debugTag + "Rolling"); }
            body.AddForce(Vector2.down * 1000f);
            body.velocity = new Vector2(0f, body.velocity.y);
            return true;
        }
        return false;
    }

    void Damp() { 
        if (input.damp) {
            body.velocity = new Vector2(body.velocity.x * 0.95f, body.velocity.y);
        }
    }

    void Renderer() {
        if (input.dash != 0) {
            character.transform.right = new Vector2(input.dash, 0);
            character.SetAnimation(character.dashAnimation);
        }
        else {
            character.SetAnimation(null);
        }
        if (state.justHurt) {
            character.SetMaterial(character.hurtMaterial);
        }
        else {
            character.SetMaterial(null);
        }
    }
}
