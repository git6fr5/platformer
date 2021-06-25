using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContainer : Container2D
{

    /* --- VARIABLES --- */
    public Weapon2D weapon;
    public Rigidbody2D body;
    public Dungeon2D dungeon;

    /* --- UNITY --- */

    /* --- OVERRIDE --- */
    public override void OnAdd(Collider2D collider) {
        if (collider.tag == "Platform") {
            print("HIT THE PLATFORM!");
            DoThing();
        }
 
    }

    public virtual void DoThing() {
        
    }
}
