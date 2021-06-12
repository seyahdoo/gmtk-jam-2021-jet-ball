using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour {
    public LineRenderer lineRenderer;
    public Ball leftBall;
    public Ball rightBall;
    public int pointCount = 20;
    private void Update() {
        if (lineRenderer.positionCount != pointCount) {
            lineRenderer.positionCount = pointCount;
        }
        for (int i = 0; i < pointCount; i++) {
            var f = (float)i / (float)(pointCount - 1);
            var newPos = Vector3.LerpUnclamped(leftBall.transform.position, rightBall.transform.position, f);
            newPos.z = transform.position.z;
            lineRenderer.SetPosition(i,newPos);
        }
    }
}
