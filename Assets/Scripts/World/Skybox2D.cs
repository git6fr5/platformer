using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skybox2D : MonoBehaviour
{
    /* --- ENUMS --- */
    public enum Setting { day, night, underground, numSettings }

    /* --- COMPONENTS --- */
    public Image image;
    public Image placeholder;
    public Sprite day;
    public Sprite night;
    public Sprite underground;

    /* --- VARIABLES --- */
    public Setting currSetting = Setting.day;
    public Dictionary<Setting, Sprite> settingDict = new Dictionary<Setting, Sprite>();
    float dissolveValue = 1f;
    [Range(0.001f, 0.999f)] public float dissolveRate = 0.05f;
    public bool dissolve = false;
    public bool transitioning = false;


    void Start() {
        Initialize();
    }

    void Update() { 
        if (settingDict[currSetting] != image.sprite && !transitioning) { Transition(); }
        if (dissolve) { Dissolve(); }
    }

    void Initialize() {
        settingDict.Add(Setting.day, day);
        settingDict.Add(Setting.night, night);
        settingDict.Add(Setting.underground, underground);
    }

    void SetLocation() { 
        
    }

    void Transition() {
        dissolveValue = 1f;
        dissolve = true;
        transitioning = true;
        placeholder.sprite = settingDict[currSetting];
    }

    void Dissolve() {
        dissolveValue -= dissolveRate;
        if (dissolveValue <= 0f) {
            dissolve = false;
            image.sprite = settingDict[currSetting];
            dissolveValue = 1f;
            image.material.SetFloat("_DissolveValue", 1);
            transitioning = false;
            return;
        }
        image.material.SetFloat("_DissolveValue", dissolveValue);
    }
}
