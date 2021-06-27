using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : Container2D
{
    /* --- CLASS --- */
    public Blade() {
        containerTags = new string[] { "Hitbox", "Arena" };
    }

    /* --- COMPONENTS --- */
    public Weapon2D weapon;
    public Hitbox ownHitbox;

    /* --- VARIABLES --- */
    public int bladeLength;
    public int bladeBreadth;

    /* --- UNITY --- */

    /* --- OVERRIDE --- */
    public override void OnAdd(Collider2D collider) {
        if (collider.tag == "Hitbox") {
            Hitbox hitbox = collider.GetComponent<Hitbox>();
            if (hitbox != ownHitbox) {
                Hit(hitbox);
            }
        }
        else if (collider.tag == "Arena") {
            Arena arena = collider.GetComponent<Arena>();
            Cut(arena);
        }
    }

    /* --- VIRTUAL --- */
    public virtual void Hit(Hitbox hitbox) {
        // hit the hitbox
        hitbox.Hurt(weapon.attackDamage);
        hitbox.Knockback(weapon.knockbackForce, hitbox.transform.position - weapon.hand.transform.position);
    }

    public virtual void Cut(Arena arena) { 
        int[] bladePoint = arena.PointToGrid((Vector2)transform.position);
        for (int i = -bladeBreadth; i < bladeBreadth + 1; i++) {
            for (int j = -bladeLength; j < bladeLength + 1; j++) {
                CutTile(arena, bladePoint[0] + i, bladePoint[1] + j);
            }
        }
        int[] playerPoint = arena.PointToGrid((Vector2)ownHitbox.transform.position);
        for (int i = (int)Mathf.Min(playerPoint[0], bladePoint[0]); i < (int)Mathf.Max(playerPoint[0], bladePoint[0]) + 1; i++)
        {
            for (int j = (int)Mathf.Min(playerPoint[1], bladePoint[1]); j < (int)Mathf.Max(playerPoint[1], bladePoint[1]) + 1; j++)
            {
                CutTile(arena, i, j);
            }

        }
    }

    void CutTile(Arena arena, int i, int j) {
        if (!arena.PointInGrid(new int[] { i, j})) { return; }
        arena.grid[i][j] = (int)Arena.Tiles.empty;
        arena.CleanCell(i, j);
        arena.PrintTile(i, j);
    }
}
