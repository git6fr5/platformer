using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls the state of a character
public class Status2D : MonoBehaviour
{
    /* --- COMPONENTS --- */
    // rendering
    [Space(5)][Header("Renderer")]
    public Character character;
    public Slider healthSlider;
    // collision
    [Space(5)][Header("Collision")]
    public Rigidbody2D body;
    public Collider2D mesh;
    public Collider2D hitbox;
    public Container2D vision;
    public Container2D groundCheck;
    // equipment
    [Space(5)][Header("Equipment")]
    public Weapon2D weapon;
    public Weapon2D secondaryWeapon;

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
    [Range(1.15f, 3f)] public float dashMultiplier = 1f;
    [Range(1.15f, 3f)] public float defaultDashMultiplier = 1f;
    [Range(1.15f, 3f)] public float quickDashMultiplier = 2f;
    public bool dashing = false;
    public bool quickDashing = false;
    public bool justDashed = false;
    public Coroutine dashReset = null;
    public Coroutine quickDashReset = null;
    [Range(0.05f, 0.25f)] public float dashBuffer = 0.05f;
    [Range(0.05f, 0.75f)] public float quickDashBuffer = 0.25f;
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
    [Space(5)] [Header("Slamming")]
    public float slamForce = 10f;

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
        if (quickDashing && quickDashReset == null) {
            dashMultiplier = quickDashMultiplier;
            quickDashReset = StartCoroutine(IEQuickDashReset(quickDashBuffer)); 
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
        if ((jumping || doubleJumping) && body.velocity.y > 0f) {
                body.gravityScale = jumpGravity;
        }
        else {
            body.gravityScale = defaultGravity;
        }
    }

    void GroundFlag() { 
        if (groundCheck.container.Count > 0) {
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

    IEnumerator IEQuickDashReset(float delay) {
        yield return new WaitForSeconds(delay);
        dashMultiplier = defaultDashMultiplier;
        quickDashing = false;
        quickDashReset = null;
        yield return null;
    }
}
