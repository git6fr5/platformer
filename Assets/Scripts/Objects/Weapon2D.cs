using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2D : MonoBehaviour
{

    /* --- COMPONENTS --- */
    public Rigidbody2D hand;

    /* --- VARIABELS ---*/
    // properties
    public bool isEquipped = false;
    public bool point = false;
    // stats
    [Space(5)] [Header("Stats")]
    public int attackDamage = 5;
    [Range(50f, 2000f)] public float knockbackForce = 400f;
    // cast point
    [Space(5)] [Header("CastPoint")]
    [Range(0.05f, 0.5f)] public float backSwingTime = 0.5f;
    [Range(0.05f, 0.5f)] public float swingTime = 0.5f;
    [Range(0.05f, 0.5f)] public float resetTime = 0.5f;
    public bool startingAttack = false;
    public bool attacking = false;
    public bool backSwinging = false;
    public bool swinging = false;
    public bool resetting = false;
    // placeholders
    protected Vector2 targetPoint;
    protected Vector3 originalPosition;
    protected Quaternion originalRotation;

    /* --- UNITY --- */
    void Start() {
    }

    void Update() {  
        if (attacking) { Attack(); }
        if (!attacking) { Idle(); }
    }

    /* --- METHODS --- */
    public void Activate(Vector2 _targetPoint) {
        StartCoroutine(IEStartAttack());
        targetPoint = _targetPoint;
    }

    /* --- VIRTUAL --- */
    public virtual void Attack() {
        // do attack
    }

    public virtual void Idle() {
        // be idle
    }

    /* --- COROUTINES --- */
    IEnumerator IEStartAttack() {
        originalPosition = hand.transform.localPosition;
        originalRotation = hand.transform.localRotation;
        attacking = true;
        startingAttack = true;
        backSwinging = true;    
        yield return StartCoroutine(IEBackSwing(backSwingTime));
    }

    IEnumerator IEBackSwing(float delay) {
        yield return new WaitForSeconds(delay);
        backSwinging = false;
        swinging = true;      
        yield return StartCoroutine(IESwing(swingTime));

    }

    IEnumerator IESwing(float delay) {
        yield return new WaitForSeconds(delay);
        swinging = false;
        resetting = true;     
        yield return StartCoroutine(IEReset(resetTime)); ;
    }

    IEnumerator IEReset(float delay) {
        yield return new WaitForSeconds(delay);
        resetting = false;
        attacking = false;
        hand.transform.localPosition = originalPosition;
        hand.transform.localRotation = originalRotation;
        yield return null;
    }


}
