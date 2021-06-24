using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRenderer : Renderer2D
{
    /* --- VARIABLES --- */
    // animations
    [Space(5)][Header("Animations")]
    public Animation2D idleAnimation;
    public Animation2D dashAnimation;
    public Animation2D jumpAnimation;
    public Animation2D crouchAnimation;
    public Animation2D rollAnimation;
    public Animation2D hangAnimation;
    // materials
   [Space(5)][Header("Materials")]
    public Material hurtMaterial;
}
