using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon2D
{
    public Transform equipment;
    public Particle particle;

    public override void Attack() {
        if (startingAttack) { StartAttack(); }
        if (backSwinging) { BackSwing(); }
        if (swinging) { Swing(); }
        if (resetting) { Reset(); }
    }

    void StartAttack() {
        // fire the particle
        particle?.Fire(); 
        // rotate the weapon
        hand.transform.localEulerAngles = new Vector3(0, 0, -90f);
        // activate the body
        hand.transform.parent = null;
        hand.simulated = true;
        hand.isKinematic = false;
        // end the start attack
        startingAttack = false;
    }

    void BackSwing() {
    }

    void Swing() {
        hand.simulated = false;
        hand.isKinematic = false;
        hand.transform.parent = equipment;
    }

    void Reset() {
        
    }
}
