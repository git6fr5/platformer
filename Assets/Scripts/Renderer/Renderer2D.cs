using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renderer2D : MonoBehaviour
{
    /* --- COMPONENTS --- */
    public SpriteRenderer spriteRenderer;
    // defaults
    [Space(5)] [Header("Default")]
    public Sprite defaultSprite;
    public Material defaultMaterial;

    /* --- VARIABLES --- */
    public Animation2D currAnimation;
    public bool render = true;

    /* --- UNITY --- */
    void Start() {
    }

    void Update() {
        Render();
    }

    /* --- METHODS --- */
    void Render() {
        if (!render) { spriteRenderer.enabled = false; }
        else if (spriteRenderer.enabled == false){
            spriteRenderer.enabled = true;
        }
        if (currAnimation != null && currAnimation.playing) {
            spriteRenderer.sprite = currAnimation.frame;
            return;
        }
        spriteRenderer.sprite = defaultSprite;
    }

    public void SetAnimation(Animation2D _animation) {
        if (currAnimation == _animation) { return; }
        if (currAnimation != null) { currAnimation.Stop(); }
        currAnimation = _animation;
        if (currAnimation != null) { currAnimation.Play(); }
    }

    public void SetMaterial(Material _material) {
        if (_material != null) { 
            spriteRenderer.material = _material;
            return;
        }
        spriteRenderer.material = defaultMaterial;
    }
}
