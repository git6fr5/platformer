using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : Bullet
{

    public Rigidbody2D playerBody;
    public float hookSpeed = 500f;
    public bool isHooked = false;

    void Update() {
        if (isHooked) {
        }
    }

    public override void Hit(Hitbox hitbox) {
        isHooked = true;
        StartCoroutine(IEDestroy(0.75f));
    }

    IEnumerator IEDestroy(float delay) {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        yield return null;
    }
}
