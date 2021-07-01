using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler,  IDragHandler,  IEndDragHandler 
{
    /* --- COMPONENTS --- */
    // linking
    public SlotUI nextSlot;
    // ui
    public Image displayImage;
    public Image countHolder;
    public Text count;

    /* --- VARIABLES --- */
    // collectible
    public Collectible slotCollectible;
    public int numCollected = 0;
    // ui
    public bool pressed = false;
    [Range(0.05f, 1f)] public float idleOpacity = 0.5f;
    [Range(0.05f, 1f)] public float hoverOpacity = 1f;
    public Color pressColor = new Color(1, 0, 0, 1);
    [Range(0.05f, 1f)] public float depressTime = 0.25f;
    Material slotMaterial;
    Vector3 originalPos;

    void Start() {
        SetMaterial();
        SnapPosition(true);
    }

    void Update() {
        Display();  
        if (pressed) { Depress(); }
    }

    /* --- POINTER --- */
    public void OnPointerClick(PointerEventData eventData) {
        // on pointer click
        slotMaterial.SetColor("_MultiplyColor", pressColor);
        pressed = true;
        StartCoroutine(IEDepress(depressTime));
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // on pointer enter
        slotMaterial.SetFloat("_Opacity", hoverOpacity);
    }

    public void OnPointerExit(PointerEventData eventData) {
        // on pointer exit
        slotMaterial.SetFloat("_Opacity", idleOpacity);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        originalPos = displayImage.transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        // on pointer drag
        Vector3 position = (Vector3)(Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        displayImage.transform.position = position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        SnapPosition(false);
    }

    /* --- METHODS --- */
    public bool SameAs(Collectible collectible) {
        if (slotCollectible == null) { return false; }
        return (slotCollectible.type == collectible.type && slotCollectible.level == collectible.level);
    }

    public bool IsEmpty() {
        return (slotCollectible == null);
    }

    public void Add(Collectible collectible) { 
        if (slotCollectible == null) {
            slotCollectible = collectible;
            numCollected = 1;
        }
        else {
            numCollected += 1;
        }
        collectible.gameObject.SetActive(false);
    }

    void Display() { 
        if (slotCollectible == null) {
            // disable the components
            displayImage.enabled = false;
            count.enabled = false;
            countHolder.enabled = false;
        }
        else {
            // enable the components
            displayImage.enabled = true;
            countHolder.enabled = true;
            count.enabled = true;
            displayImage.sprite = slotCollectible.GetComponent<SpriteRenderer>().sprite;
            count.text = string.Format("{0:00}", numCollected);
        }
    }

    void Depress() {
        Color currColor = slotMaterial.GetColor("_MultiplyColor");
        Color gradientColor = pressColor / depressTime * Time.deltaTime;
        slotMaterial.SetColor("_MultiplyColor", currColor - gradientColor);
    }

    void SetMaterial() {
        // create a material preset
        slotMaterial = new Material(Shader.Find("Basic/OpacityOutlineShader"));
        // set the preset values
        slotMaterial.SetFloat("_Opacity", idleOpacity);
        slotMaterial.SetColor("_AddColor", new Color(0, 0, 0, 0));
        slotMaterial.SetColor("_MultiplyColor", new Color(0, 0, 0, 0));
        slotMaterial.SetFloat("_OutlineWidth", 0f);
        // set the material
        GetComponent<Image>().material = slotMaterial;
    }

    void SnapPosition(bool snap) {
        if (snap) { originalPos = displayImage.transform.localPosition; }
        displayImage.transform.localPosition = originalPos;
    }

    /* --- COROUTINE --- */
    IEnumerator IEDepress(float delay) {
        yield return new WaitForSeconds(delay);
        pressed = false;
        yield return null;
    }
}
