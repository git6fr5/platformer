using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Renderer2D
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
    // particles
    [Space(5)][Header("State Particles")]
    public Particle hurtParticle;
    [Space(5)][Header("Motion Particles")]
    public Particle quickDashParticle;
    public Particle jumpParticle;
    public Particle doubleJumpParticle;
    public Particle crouchParticle;
    // materials
    [Space(5)][Header("Materials")]
    public Material hurtMaterial;
}
