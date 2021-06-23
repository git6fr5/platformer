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
    public Weapon2D weapon;
    public CharacterRenderer character;

    /* --- VARIABLES --- */
    public bool damp = true;

    /* --- UNITY --- */
    void Start() { 
    }

    void Update() {
        if (!state.justDashed && input.dash != 0) { Dash(); }
        if (state.onGround && !state.justJumped && input.jump) { Jump(); }
        if (state.jumping && state.justJumped && input.jump) { DoubleJump(); }
        if (input.crouch || state.stickyCrouch) { Crouch(); }
        if (weapon != null && !weapon.attacking && input.attack) { Attack(); }
        if (damp) { Damp(); }
        Renderer();
    }

    /* --- METHODS --- */
    void Dash() {    
        if (doDebug) { print(debugTag + "Dashing"); }
        body.velocity = new Vector2(0, body.velocity.y);
        body.AddForce(new Vector2(input.dash * state.dashForce, 0));
        state.justDashed = true;
    }

    void Jump() {
        if (doDebug) { print(debugTag + "Jumping"); }
        body.velocity = new Vector2(body.velocity.x, 0);
        body.AddForce(new Vector2(0, state.jumpForce));
        state.justJumped = true;
    }

    void DoubleJump() { 
        if (doDebug) { print(debugTag + "Double Jumping"); }
        body.velocity = new Vector2(body.velocity.x, 0);
        body.AddForce(new Vector2(0, state.jumpForce));
        state.doubleJumping = true;
        state.jumping = false;
    }

    void Crouch() {
        if (doDebug) { print(debugTag + "Crouching"); }
        body.AddForce(new Vector2(0, -state.crouchForce));
        body.velocity = new Vector2(body.velocity.x * 0.95f, body.velocity.y);
    }

    void Attack() {
        if (doDebug) { print(debugTag + "Attacking"); }
        weapon.Activate();
    }

    void Damp() { 
        body.velocity = new Vector2(body.velocity.x * 0.999f, body.velocity.y);
        if (body.velocity.y > 20f){
            body.velocity = new Vector2(body.velocity.x, 10f);
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
