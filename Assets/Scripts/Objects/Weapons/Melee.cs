using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon2D
{

    public Particle particle;

    public override void Attack() {
        if (startingAttack) { StartAttack(); }
        if (backSwinging) { BackSwing(); }
        if (swinging) { Swing(); }
        if (resetting) { Reset(); }
    }

    void StartAttack() {
        hand.transform.localEulerAngles = new Vector3(0, 0, -90f);
        hand.simulated = true;
        hand.isKinematic = false;
        particle.Fire();
        startingAttack = false;
    }

    void BackSwing() {
        //hand.transform.localPosition = hand.transform.localPosition - Vector3.right * 0.5f * Time.deltaTime / backSwingTime; 
    }

    void Swing() { 
        hand.transform.localPosition = hand.transform.localPosition + Vector3.right * 0.5f * Time.deltaTime / swingTime;
    }

    void Reset() {
        hand.simulated = false;
        hand.isKinematic = false;
    }
}
