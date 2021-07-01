using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Container2D
{
    public enum Type { ores, gems, seeds, eggs };
    public Type type;
    public int level = 0;

    public override void OnAdd(Collider2D collider) {
        if (collider.tag == "Hitbox") {
            Hitbox hitbox = collider.GetComponent<Hitbox>();
            hitbox.Collect(this);
        }
    }
}
