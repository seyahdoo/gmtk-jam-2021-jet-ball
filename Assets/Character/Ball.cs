using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour {
    public bool isLeft = false;
    public Vector2 input = Vector2.zero;
    public bool stickButtonPressed = false;
    public InputSettings inputSettings;
    public Rigidbody2D body;
    
    public float maxFuel = 10f;
    public float inputToForceRatio = 1f;
    private float _currentFuel;
    private void OnEnable() {
        inputSettings.Enable();
    }
    private void OnDisable() {
        inputSettings.Disable();
    }
    private void Awake() {
        inputSettings = new InputSettings();
        body = GetComponent<Rigidbody2D>();
        _currentFuel = maxFuel;
    }
    private void Update() {
        if (isLeft) {
            input = inputSettings.Character.LeftMovement.ReadValue<Vector2>();
            stickButtonPressed = Math.Abs(inputSettings.Character.LeftStick.ReadValue<float>() - 1) < .1f;
        }
        else {
            input = inputSettings.Character.RightMovement.ReadValue<Vector2>();
            stickButtonPressed = Math.Abs(inputSettings.Character.RightStick.ReadValue<float>() - 1) < .1f;
        }
    }
    private void FixedUpdate() {
        if (stickButtonPressed) {
            body.velocity = Vector2.zero;
            body.angularVelocity = 0f;
            body.isKinematic = true;
        }
        else {
            body.isKinematic = false;
            var force = input * inputToForceRatio;
            body.AddForce(force);
        }
    }
}
