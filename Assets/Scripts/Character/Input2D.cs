using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input2D : MonoBehaviour
{
    
    /* --- VARIABLES --- */
    [HideInInspector] public float dash;
    [HideInInspector] public bool jump;
    [HideInInspector] public bool crouch;

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
