using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree2D : MonoBehaviour
{
    /* --- COMPONENTS --- */
    public SpriteRenderer spriteRenderer;
    public Texture leafTex;

    /* --- VARIABLES --- */
    Material treeMat;

    /* --- UNITY --- */
    void Start() {
        treeMat = new Material(Shader.Find("Basic/WindShader"));
        treeMat.SetColor("_AddColor", new Color(0, 0, 0, 0));
        treeMat.SetColor("_MultiplyColor", new Color(0, 0, 0, 0));
        treeMat.SetTexture("_LeafTex", leafTex);
        spriteRenderer.material = treeMat;
    }
}
