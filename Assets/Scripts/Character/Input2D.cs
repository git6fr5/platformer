using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input2D : MonoBehaviour
{
    /* --- DEBUG --- */
    public bool doDebug = false;
    protected string debugTag = "[Input2D]: ";

    /* --- VARIABLES --- */
    [HideInInspector] public float dash;
    [HideInInspector] public bool jump;
    [HideInInspector] public bool crouch;
    [HideInInspector] public bool damp;
    [HideInInspector] public bool rightRoll;
    [HideInInspector] public bool leftRoll;
    [HideInInspector] public bool doubleJump;
    [HideInInspector] public bool slam;

    /* --- UNITY --- */
    void Start() { 
    }

    void Update() {
        GetInput();
    }
    
    /* --- VIRTUAL --- */
    public virtual void GetInput() {      
        // get the input
    }

}
