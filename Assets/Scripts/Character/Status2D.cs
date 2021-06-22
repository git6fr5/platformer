using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status2D : MonoBehaviour
{
    /* --- COMPONENTS --- */
    public Rigidbody2D body;
    public Collider2D hitBox;
    public Container2D groundCheck;

    /* --- VARIABLES --- */
    // movement
    public float dashForce = 100f;
    public bool canDash = true;
    public bool justDashed = false;
    public Coroutine dashReset = null;
    public float dashBuffer = 0.05f;
    public bool dashing = false;
    // jumping
    public float jumpForce = 100f;
    public bool canJump = true;
    public bool justJumped = false;
    public Coroutine jumpReset = null;
    public float jumpBuffer = 0.05f;
    public bool jumping = false;
    public bool jumpPeaking = false;
    public float jumpPeakingScale = 0.5f;
    public float jumpPeakBuffer = 0.05f;
    public bool jumpFalling = false;
    public float jumpFallingScale = 2f;
    // crouching
    public float crouchForce = 10f;
    public bool canCrouch = true;
    public bool stickyCrouch = false;


    /* --- UNITY --- */
    void Start() {
    }

    void Update() {
        DashFlag();
        JumpFlag();
    }

    /* --- METHODS --- */
    void DashFlag()
    {
        if (justDashed && dashReset == null)
        {
            dashing = true;
            dashReset = StartCoroutine(IEDashReset(dashBuffer));
        }
        if (!justDashed) {
            canDash = true;
        }
        else { canDash = false; }
        if (dashing)
        {
            // do stuff here maybe
        }
    }

    void JumpFlag() {
        if (justJumped && jumpReset == null) {
            jumping = true;
            jumpReset = StartCoroutine(IEJumpReset(jumpBuffer));
        }
        if (!justJumped && groundCheck.container.Count > 0) {
            jumping = false;
            canJump = true;
        }
        else {
            canJump = false; 
        }
        if (jumping) {
            if (Mathf.Abs(body.velocity.y) < jumpPeakBuffer) {
                jumpPeaking = true;
            }
            else { jumpPeaking = false; }
            if (body.velocity.y < -jumpPeakBuffer) {
                jumpFalling = true;
            }
            else { jumpFalling = false; }
        }
    }

    /* --- COROUTINES --- */
    IEnumerator IEJumpReset(float delay) {
        yield return new WaitForSeconds(delay);
        justJumped = false;
        jumpReset = null;
        yield return null;
    }

    IEnumerator IEDashReset(float delay) {
        yield return new WaitForSeconds(delay);
        justDashed = false;
        dashReset = null;
        yield return null;
    }
}
