﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation2D : MonoBehaviour
{
    /* --- COMPONENTS --- */
    public Sprite[] frames;

    /* --- VARIABLES --- */
    public float frameRate;
    public int frameIndex = 0;
    public float timer = 0f;
    public bool playing = false;
    public Sprite frame;

    /* --- UNITY --- */
    void Start() {
        frame = frames[0];
    }

    void Update() {
        if (playing) { Animate(); }
    }

    /* --- METHODS --- */
    public void Play() {
        frameIndex = 0;
        timer = 0f;
        playing = true;
    }

    public void Stop() {
        frameIndex = 0;
        timer = 0f;
        playing = false;
    }

    void Animate() {
        timer = timer + Time.deltaTime;
        if (timer > (1f / frameRate)) {
            NextFrame();
            timer = 0f;
        }
    }

    void NextFrame(){
        frameIndex = (frameIndex + 1) % frames.Length;
        frame = frames[frameIndex];
    }
}
