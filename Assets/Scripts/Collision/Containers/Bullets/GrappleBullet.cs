using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleBullet : BulletContainer
{

    public Rigidbody2D playerBody;
    public float forceMagnitude = 500f;
    public bool isHooked = false;

    void Update() {
        if (isHooked)
        {
            Vector2 force = ((Vector2)(transform.position - playerBody.transform.position)).normalized * forceMagnitude;
            playerBody.AddForce(force);
        }
    }

    public override void DoThing()
    {
        body.velocity = Vector2.zero;
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        isHooked = true;
        StartCoroutine(IEDestroy(0.75f));
    }

    IEnumerator IEDestroy(float delay) {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        yield return null;
    }
}
