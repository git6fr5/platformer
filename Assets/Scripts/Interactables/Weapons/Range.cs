using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : Weapon2D
{

    public Transform handParent;
    [Range(0f, 50f)] public float throwSpeed = 25f;
    [Range(0f, 50f)] public float throwSpin = 5f;
    float spinDirection;

    public override void Attack()
    {
        if (startingAttack) { StartAttack(); }
        if (backSwinging) { BackSwing(); }
        if (swinging) { Swing(); }
        if (resetting) { Reset(); }
    }

    public override void Idle()
    {
    }

    void StartAttack()
    {
        hand.simulated = true;
        hand.transform.parent = null;
        hand.velocity = hand.transform.right.normalized * throwSpeed;// (targetPoint - (Vector2)hand.transform.position).normalized * throwSpeed;
        spinDirection = -Mathf.Sign(hand.transform.right.x);
        startingAttack = false;
    }

    void BackSwing()
    {
    }

    void Swing()
    {
        hand.transform.RotateAround(hand.transform.position, Vector3.forward, spinDirection * throwSpin);
    }

    void Reset()
    {
        hand.velocity = Vector2.zero;
        hand.simulated = false;
        hand.transform.parent = handParent;
    }
}
