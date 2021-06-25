using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2D : MonoBehaviour
{
    // rendering
    [Space(5)][Header("Renderer")]
    public CharacterRenderer characterRenderer;
    // collision
    [Space(5)][Header("Collision")]
    public Collider2D mesh;
    public Collider2D hitbox;
    public Container2D vision;
    public Container2D groundCheck;
    public Container2D wallCheck;
    public Container2D climbCheck;
    // equipment
    [Space(5)][Header("Equipment")]
    public Weapon2D weapon;
    public Weapon2D secondaryWeapon;
}
