using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button2D : MonoBehaviour
{
    public enum ButtonState { none, pressed, depressed };

    /*--- Components ---*/
    public MenuManager menu;
    ButtonState buttonState = ButtonState.none;

    public Color fade;
    public float fadeTime;
    public float fadeIntensity;

    /* --- Internal Variables --- */
    public bool interactable = false;

    /* --- Unity Methods --- */
    void Start()
    {
        ButtonSetAddColor(new Color(0, 0, 0, 0));
        GetComponent<Image>().material.SetFloat("_OutlineWidth", 0f);
    }

    void OnEnable()
    {

    }

    void Update()
    {
        if (buttonState == ButtonState.pressed)
        {
            ButtonAddColor(fade * fadeIntensity * Time.deltaTime / fadeTime);
        }
        else if (buttonState == ButtonState.depressed)
        {
            ButtonAddColor(-(Vector4)fade * fadeIntensity * Time.deltaTime / fadeTime);
        }
        if (interactable) {
            GetComponent<Collider2D>().enabled = true;
        }
        else {
            GetComponent<Collider2D>().enabled = false;
        }
    }

    /* --- Methods --- */
    void OnMouseDown()
    {
        Press();
        buttonState = ButtonState.pressed;
        StartCoroutine(IEButtonDepress(fadeTime));
    }

    public virtual void Press() { 
    }

    private IEnumerator IEButtonDepress(float delay)
    {
        yield return new WaitForSeconds(delay);

        buttonState = ButtonState.depressed;
        StartCoroutine(IEButtonIdle(fadeTime));

        yield return null;
    }

    private IEnumerator IEButtonIdle(float delay)
    {
        yield return new WaitForSeconds(delay);

        buttonState = ButtonState.none;
        ButtonSetAddColor(new Color(0, 0, 0, 0));

        yield return null;
    }

    void ButtonAddColor(Color color)
    {
        GetComponent<Image>().material.SetColor("_AddColor", GetComponent<Image>().material.GetColor("_AddColor") + color);
    }

    void ButtonSetAddColor(Color color)
    {
        GetComponent<Image>().material.SetColor("_AddColor", color);
    }

    void OnMouseOver()
    {
        GetComponent<Image>().material.SetFloat("_OutlineWidth", 4f);
        
    }

    void OnMouseExit()
    {
        print("hello");
        GetComponent<Image>().material.SetFloat("_OutlineWidth", 0f);

    }
}
