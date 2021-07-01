using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Container2D
{
    /* --- CLASS --- */
    public Table() {
        containerTags = new string[] { "Hitbox" };
    }

    /* --- COMPONENTS --- */
    public GameObject tableUI;

    /* --- VARIABLES --- */

    /* --- UNITY --- */

    /* --- OVERRIDE --- */
    public override void OnAdd(Collider2D collider) {
        if (collider.tag == "Hitbox") {
            Hitbox hitbox = collider.GetComponent<Hitbox>();
            Interact(hitbox);
        }
    }

    public override void OnRemove(Collider2D collider) {
        if (collider.tag == "Hitbox") {
            Hitbox hitbox = collider.GetComponent<Hitbox>();
            Uninteract(hitbox);
        }
    }

    /* --- VIRTUAL --- */
    public virtual void Interact(Hitbox hitbox) {
        print("Interacting");
        tableUI.SetActive(true);
    }

    public virtual void Uninteract(Hitbox hitbox) {
        print("Uninteracting");
        tableUI.SetActive(false);
    }

}
