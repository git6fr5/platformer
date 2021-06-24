using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gathers the inputs
public class Input2D : MonoBehaviour
{
    /* --- DEBUG --- */
    public bool doDebug = false;
    protected string debugTag = "[Input2D]: ";

    /* --- VARIABLES --- */
    public float dash;
    public bool quickDash;
    public bool jump;
    public bool doubleJump;
    public bool crouch;
    public bool attack;
    public Vector2 targetPoint;

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
