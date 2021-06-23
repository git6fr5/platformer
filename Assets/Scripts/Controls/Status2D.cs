using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls the state of a character
public class Status2D : MonoBehaviour
{
    /* --- COMPONENTS --- */
    public Rigidbody2D body;
    public Collider2D hitBox;
    public Container2D groundContainer;
    public Slider healthSlider;

    /* --- VARIABLES --- */
    // health
    [Space(5)] [Header("Health")] 
    public int maxHealth = 100;
    public int currHealth = 100;
    // hurt
    [Space(5)] [Header("Hurt")]
    public bool justHurt = false;
    public Coroutine hurtReset = null;
    [Range(0.05f, 0.25f)] public float hurtBuffer = 0.2f;
    // knockback
    [Space(5)] [Header("Knockback")]
    public bool justKnocked = false;
    public Coroutine knockReset = null;
    [Range(0.05f, 0.25f)] public float knockBuffer = 0.05f;
    // dashing
    [Space(5)] [Header("Dash")]
    [Range(50f, 1000f)] public float dashForce = 100f;
    public bool dashing = false;
    public bool justDashed = false;
    public Coroutine dashReset = null;
    [Range(0.05f, 0.25f)] public float dashBuffer = 0.05f;
    // jumping
    [Space(5)] [Header("Jumping")]
    [Range(50f, 1000f)] public float jumpForce = 100f;
    public bool jumping = false;
    public bool doubleJumping = false;
    public bool justJumped = false;
    public Coroutine jumpReset = null;
    [Range(0.05f, 0.25f)] public float jumpBuffer = 0.2f;
    [Range(1f, 15f)] public float defaultGravity = 10f;
    [Range(1f, 15f)] public float jumpGravity = 3f;
    public bool onGround = false;
    // crouching
    [Space(5)] [Header("Crouching")]
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
        GroundFlag();
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
            gameObject.SetActive(false);
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
    }

    void JumpFlag() {
        if (justJumped && jumpReset == null) {
            jumping = true;
            jumpReset = StartCoroutine(IEJumpReset(jumpBuffer));
        }
        if (!justJumped && onGround) {
            jumping = false;
        }
        if (jumping || doubleJumping) {
            if (body.velocity.y > 0f) {
                body.gravityScale = jumpGravity;
            }
            else {
                body.gravityScale = defaultGravity;
            }
        }
    }

    void GroundFlag() { 
        if (groundContainer.container.Count > 0) {
            onGround = true;
        }
        else {
            onGround = false;
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
