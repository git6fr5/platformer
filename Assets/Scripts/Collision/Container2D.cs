using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container2D : MonoBehaviour
{
    /* --- DEBUG --- */
    public bool doDebug = false;
    protected string debugTag = "[Container2D]: ";

    /* --- COMPONENTS ---*/
    public string[] containerTags;

    /* --- VARIABLES --- */
    public List<Collider2D> container = new List<Collider2D>();

    /* --- UNITY --- */
    void OnTriggerEnter2D(Collider2D collider) {
        Add(collider);
    }

    void OnTriggerExit2D(Collider2D collider) {
        Remove(collider);  
    }

    /* --- METHODS --- */
    void Add(Collider2D collider) {
        foreach (string containerTag in containerTags) {
            if (!container.Contains(collider) && collider.tag == containerTag) {
                if (doDebug) { print(debugTag + "Added " + collider.name); }
                container.Add(collider);
                OnAdd(collider);
            }
        }  
    }

    void Remove(Collider2D collider) { 
        if (container.Contains(collider)) {
            if (doDebug) { print(debugTag + "Removed " + collider.name); }
            container.Remove(collider);
            OnRemove(collider);
        }
    }

    /* --- VIRTUAL --- */
    public virtual void OnAdd(Collider2D collider) { 
        // on adding
    }

    public virtual void OnRemove(Collider2D collider) { 
        // on adding
    }
}
