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
    // horizontal
    public float dash;
    // vertical
    public bool jump;
    public bool slam;
    // action
    public bool attack;
    public bool attack2;
    public Vector2 targetPoint;
    // inventory
    public bool bagpack;

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
