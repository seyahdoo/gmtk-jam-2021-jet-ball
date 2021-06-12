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
    public Rigidbody2D otherBody;
    public DistanceJoint2D Joint2D;
    public float powerDistanceError = .4f;
    public float powerFillSpeed = 5f;
    public float flingMultiplier = 1f;
    public float maxPower = 5f;
    
    public float maxFuel = 10f;
    public float inputToForceRatio = 1f;
    private float _currentFuel;
    private float _distance;
    [SerializeField] private float _power = 0f;
    private void OnEnable() {
        inputSettings.Enable();
    }
    private void OnDisable() {
        inputSettings.Disable();
    }
    private void Awake() {
        inputSettings = new InputSettings();
        _currentFuel = maxFuel;
        _distance = Joint2D.distance;
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
            _power = 0f;
            body.isKinematic = true;
        }
        else {
            var currentDistance = Vector3.Distance(body.position, otherBody.position);
            if (Mathf.Abs(currentDistance - _distance) < powerDistanceError 
                && Vector2.Dot((otherBody.position - body.position).normalized, input.normalized) < 0) {
                _power += powerFillSpeed * Time.fixedDeltaTime;
                _power = Mathf.Clamp(_power, 0f, maxPower);
            }
            else {
                if (_power > .1f) {
                    body.AddForce((otherBody.position - body.position).normalized * (_power * flingMultiplier), ForceMode2D.Impulse);
                    _power = 0f;
                }
            }
            body.isKinematic = false;
            var force = input * inputToForceRatio;
            body.AddForce(force);
        }
    }
}
