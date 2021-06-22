using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renderer2D : MonoBehaviour
{
    /* --- COMPONENTS --- */
    public Input2D input;
    public SpriteRenderer spriteRenderer;
    public Sprite defaultSprite;
    public Animation2D dashAnimation;

    /* --- VARIABLES --- */
    public Animation2D currAnimation;

    /* --- UNITY --- */
    void Start() {
    }

    void Update() {
        Animate();
    }

    /* --- METHODS --- */
    public void SetAnimation(Animation2D _animation) {
        if (currAnimation == _animation) { return; }
        if (currAnimation != null) { currAnimation.Stop(); }
        currAnimation = _animation;
        if (currAnimation != null) { currAnimation.Play(); }
    }

    void Animate() {
        if (currAnimation != null && currAnimation.playing) {
            spriteRenderer.sprite = currAnimation.frame;
            return;
        }
        spriteRenderer.sprite = defaultSprite;
    }
}
