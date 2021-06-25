using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : Weapon2D
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 40f;

    public override void Attack()
    {
        if (startingAttack) { StartAttack(); }
        if (backSwinging) { BackSwing(); }
        if (swinging) { Swing(); }
        if (resetting) { Reset(); }
    }

    public override void Idle()
    {
        
    }


    void StartAttack()
    {
        // The bullet
        BulletContainer bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity, null).GetComponent<BulletContainer>();
        bullet.gameObject.SetActive(true);
        bullet.body.velocity = -(transform.position - (Vector3)targetPoint).normalized * bulletSpeed;
        startingAttack = false;
    }

    void BackSwing()
    {
    }

    void Swing()
    {
    }

    void Reset()
    {
    }
}
