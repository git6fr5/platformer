using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Container that travels from point to point
public class TravellingContainer : Container2D
{
    /* --- COMPONENTS --- */
    public Rigidbody2D body;
    public Transform[] points;

    /* --- VARIABLES --- */
    public int pointIndex = 0;
    Transform currPoint;

    /* --- UNITY --- */
    void Start() {
        if (points.Length > 0) {
            currPoint = points[0];
            TravelToPoint();
        }
    }

    /* --- OVERRIDE --- */
    public override void OnAdd(Collider2D collider) {
        if (collider.transform == currPoint) {
            NextPoint();
            TravelToPoint();
        }
    }

    /* --- METHODS --- */
    void NextPoint() {
        pointIndex = (pointIndex + 1) % points.Length;
        currPoint = points[pointIndex];
    }

    void TravelToPoint() {
        print("hello");
        body.velocity = ((Vector2)(currPoint.position - transform.position)).normalized * 5f;
    }
}
