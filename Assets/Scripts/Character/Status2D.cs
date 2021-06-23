using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status2D : MonoBehaviour
{
    /* --- COMPONENTS --- */
    public Rigidbody2D body;
    public Collider2D hitBox;
    public Container2D groundCheck;
    public Slider healthSlider;

    /* --- VARIABLES --- */
    // health
    public int maxHealth = 100;
    public int currHealth = 100;
    // hurt
    public bool justHurt = false;
    public Coroutine hurtReset = null;
    public float hurtBuffer = 0.2f;
    // knockback
    public bool justKnocked = false;
    public Coroutine knockReset = null;
    public float knockBuffer = 0.05f;
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
    public bool doubleJumping = false;
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
        SetHealth();
    }

    void Update() {
        DashFlag();
        JumpFlag();
        HurtFlag();
        KnockFlag();
    }

    /* --- METHODS --- */
    void SetHealth() {
        healthSlider.maxValue = maxHealth;
    }

    public void HurtFlag() {
        if (justHurt && hurtReset == null) {
            hurtReset = StartCoroutine(IEHurtReset(hurtBuffer));
        }
        healthSlider.value = currHealth;
        if (currHealth <= 0) { 
            //die
        }
    }

    public void KnockFlag() {
        if (justKnocked && knockReset == null) {
            knockReset = StartCoroutine(IEKnockReset(knockBuffer));
        }
    }

    void DashFlag() {
        if (justDashed && dashReset == null) {
            dashing = true;
            dashReset = StartCoroutine(IEDashReset(dashBuffer));
        }
        if (!justDashed) {
            canDash = true;
        }
        else { canDash = false; }
        if (dashing) {
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
            doubleJumping = false;
            canJump = true;
        }
        else {
            canJump = false; 
        }
        if (jumping || doubleJumping) {
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
    IEnumerator IEHurtReset(float delay) {
        yield return new WaitForSeconds(delay);
        justHurt = false;
        hurtReset = null;
        yield return null;
    }

    IEnumerator IEKnockReset(float delay) {
        yield return new WaitForSeconds(delay);
        justKnocked = false;
        knockReset = null;
        yield return null;
    }

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
