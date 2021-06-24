﻿using System.Collections;
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
    // particles
    [Space(5)][Header("Particles")]
    public ParticleRenderer quickDashParticle;
    // materials
   [Space(5)][Header("Materials")]
    public Material hurtMaterial;
}
