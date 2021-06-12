using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public Ball leftBall;
    public Ball rightBall;
    public Cable cable;

    public float distance = 5f;
    public float damping = .4f;
    
    public float togetherForce = 10f;
    
    private void FixedUpdate() {
        var currentDistance = Vector3.Distance(leftBall.body.position, rightBall.body.position);
        if (currentDistance > distance) {
            //force to left to go to right ball
            leftBall.body.AddForce((rightBall.body.position - leftBall.body.position).normalized * togetherForce);
            //force to right to go to left ball
            rightBall.body.AddForce((leftBall.body.position - rightBall.body.position).normalized * togetherForce);
        }
        if (Mathf.Abs(currentDistance - distance) < damping) {
            
            
            
        }
        //maintain distance
        //if distance is full and still trying to back further away, build stress
        //if no longer trying to back further away, use stress and launch
    }
}
