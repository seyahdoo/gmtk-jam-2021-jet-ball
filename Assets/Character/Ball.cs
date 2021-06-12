using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    public float fuelFillSpeed = 5f;
    public float inputToForceRatio = 1f;
    public float fuelUseSpeed = 3;

    private float _distance;
    [SerializeField] private float _currentFuel;
    [SerializeField] private float _curentPower = 0f;
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
            _curentPower = 0f;
            _currentFuel += fuelFillSpeed;
            _currentFuel = Mathf.Clamp(_currentFuel, 0f, maxFuel);
            body.isKinematic = true;
        }
        else {
            var currentDistance = Vector3.Distance(body.position, otherBody.position);
            if (Mathf.Abs(currentDistance - _distance) < powerDistanceError 
                && Vector2.Dot((otherBody.position - body.position).normalized, input.normalized) < 0) {
                _curentPower += powerFillSpeed * Time.fixedDeltaTime;
                _curentPower = Mathf.Clamp(_curentPower, 0f, maxPower);
            }
            else {
                if (_curentPower > .1f) {
                    body.AddForce((otherBody.position - body.position).normalized * (_curentPower * flingMultiplier), ForceMode2D.Impulse);
                    _curentPower = 0f;
                }
            }
            _currentFuel -= input.magnitude * fuelUseSpeed * Time.fixedDeltaTime;
            _currentFuel = Mathf.Clamp(_currentFuel, 0, maxFuel);
            body.isKinematic = false;
            if (_currentFuel > 0.1f) {
                var force = input * inputToForceRatio;
                body.AddForce(force);
            }
        }
    }
}
