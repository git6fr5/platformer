using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2D : Renderer2D
{
    /* --- VARIABLES --- */
    // animations
    public Animation2D idleAnimation;
    public Animation2D dashAnimation;
    public Animation2D jumpAnimation;
    public Animation2D crouchAnimation;
    public Animation2D rollAnimation;
    public Animation2D hangAnimation;
    // materials
    public Material hurtMaterial;
}
