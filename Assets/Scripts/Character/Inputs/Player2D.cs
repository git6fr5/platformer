using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : Input2D
{
    /* --- VARIABLES --- */
    public float doubleTapBuffer = 0.2f;
    public string rightDashKey;
    public string leftDashKey;
    public string jumpKey;
    public string crouchKey;
    public string dampKey;

    public Dictionary<string, float> tappedButtonTimer = new Dictionary<string, float>();
    
    /* --- OVERRIDE --- */
    public override void GetInput() {
        SingleTaps();
        rightRoll = DoubleTap(rightDashKey);
        leftRoll = DoubleTap(leftDashKey);
        doubleJump = DoubleTap(jumpKey);
        slam = DoubleTap(crouchKey);
    }

    void SingleTaps() {
        // movement
        if (Input.GetKey(rightDashKey)) { dash = 1; }
        else if (Input.GetKey(leftDashKey)) { dash = -1; }
        else { dash = 0; }
        // jumping
        if (Input.GetKey(jumpKey)) { jump = true; }
        else { jump = false; }
        // crouching
        if (Input.GetKey(crouchKey)) { crouch = true; }
        else { crouch = false; }
        // dampening
        if (Input.GetKey(dampKey)) { damp = true; }
        else { damp = false; }
    }

    bool DoubleTap(string key) {
        bool condition = false;
        if (!tappedButtonTimer.ContainsKey(key)) { tappedButtonTimer.Add(key, 0f); }
        if (tappedButtonTimer[key] <= 0f) {
            tappedButtonTimer[key] = 0f; 
        }
        if (Input.GetKeyDown(key)) {
            if (tappedButtonTimer[key] > 0f) {
                if (doDebug) { print(debugTag + "Double Tapped: " + key); }
                condition = true; 
            }
            else { tappedButtonTimer[key] = doubleTapBuffer; }
        }
        tappedButtonTimer[key] -= Time.deltaTime;
        if (tappedButtonTimer[key] <= 0f)
        {
            tappedButtonTimer[key] = 0f;
        }
        return condition;
    }
}
