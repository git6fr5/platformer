using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : Container2D
{

    public Hitbox target;
    public Hitbox ownHitbox;

    public Vision() {
        containerTags = new string[] { "Hitbox" };
    }

    public override void OnAdd(Collider2D collider) {
        if (collider.tag == "Hitbox")
        {
            Hitbox hitbox = collider.GetComponent<Hitbox>();
            if (hitbox != ownHitbox)
            {
                target = hitbox;
            }
        }
    }
}
