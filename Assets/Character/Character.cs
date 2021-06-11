using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour {
    public bool isLeft = false;
    public Vector2 input = Vector2.zero;
    public float maxFuel = 10f;
    public float inputToForceRatio = 1f;
    private float _currentFuel;
    private Rigidbody2D _rigidbody;
    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentFuel = maxFuel;
    }
    private void Update() {
        if (isLeft) {
            input = new Vector2(Input.GetAxis("HorizontalLeft"), Input.GetAxis("VerticalLeft"));
        }
        else {
            input = new Vector2(Input.GetAxis("HorizontalRight"), Input.GetAxis("VerticalRight"));
        }
    }
    private void FixedUpdate() {
        var force = input * inputToForceRatio;
        _rigidbody.AddForce(force);
    }
}
