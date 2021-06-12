using System;
using System.Collections.Generic;
using LapsRuntime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour {
    public bool isLeft = false;
    public Vector2 input = Vector2.zero;
    public bool stickButtonPressed = false;
    public InputSettings inputSettings;
    public Rigidbody2D body;
    public float inputToForceRatio = 1f;
    public LayerMask stickableLayers;
    public Ball otherBall;
    public BallGraphic BallGraphic;
    public float impactToTriggerCrash = 1f;
    
    private HashSet<Collider2D> _stickableSurfaces = new HashSet<Collider2D>();
    private bool _sticked = false;
    public bool Sticked => _sticked;
    private void OnEnable() {
        inputSettings.Enable();
    }
    private void OnDisable() {
        inputSettings.Disable();
    }
    private void Awake() {
        inputSettings = new InputSettings();
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
        if (stickButtonPressed && _stickableSurfaces.Count >= 1) {
            body.velocity = Vector2.zero;
            body.angularVelocity = 0f;
            body.isKinematic = true;
            _sticked = true;
            BallGraphic.sticking = true;
        }
        else {
            body.isKinematic = false;
            _sticked = false;
            BallGraphic.sticking = false;
            if (otherBall.Sticked) {
                var force = input * inputToForceRatio;
                body.AddForce(force);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.relativeVelocity.magnitude > impactToTriggerCrash) {
            BallGraphic.Crashed();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (LapsMath.LayerMaskContains(stickableLayers, other.gameObject.layer)) {
            _stickableSurfaces.Add(other);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (LapsMath.LayerMaskContains(stickableLayers, other.gameObject.layer)) {
            _stickableSurfaces.Remove(other);
        }
    }
}
