using System;
using System.Collections.Generic;
using LapsRuntime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour {
    public bool isLeft = false;
    public Vector2 input = Vector2.zero;
    public bool stickButtonPressed = false;
    public GameSettings gameSettings;
    public Ball otherBall;
    
    private Rigidbody2D _body;
    private BallGraphic _ballGraphic;
    private AudioSource _audioSource;
    private HashSet<Collider2D> _stickableSurfaces = new HashSet<Collider2D>();
    private bool _sticked = false;
    private bool _dead;
    public bool Sticked => _sticked;
    private void OnEnable() {
        gameSettings.inputSettings.Enable();
    }
    private void OnDisable() {
        gameSettings.inputSettings.Disable();
    }
    private void Awake() {
        gameSettings.inputSettings = new InputSettings();
        _audioSource = GetComponent<AudioSource>();
        _ballGraphic = GetComponentInChildren<BallGraphic>();
        _body = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        if (isLeft) {
            input = gameSettings.inputSettings.Character.LeftMovement.ReadValue<Vector2>();
            stickButtonPressed = Math.Abs(gameSettings.inputSettings.Character.LeftStick.ReadValue<float>() - 1) < .1f;
        }
        else {
            input = gameSettings.inputSettings.Character.RightMovement.ReadValue<Vector2>();
            stickButtonPressed = Math.Abs(gameSettings.inputSettings.Character.RightStick.ReadValue<float>() - 1) < .1f;
        }
    }
    private void FixedUpdate() {
        if (stickButtonPressed && _stickableSurfaces.Count >= 1) {
            _body.velocity = Vector2.zero;
            _body.angularVelocity = 0f;
            if (_sticked == false) {
                _audioSource.PlayOneShot(gameSettings.stickClip);
            }
            _sticked = true;
            _body.isKinematic = true;
            _ballGraphic.sticking = true;
        }
        else {
            _sticked = false;
            _body.isKinematic = false;
            _ballGraphic.sticking = false;
            if (otherBall.Sticked) {
                var force = input * gameSettings.inputToForceRatio;
                _body.AddForce(force);
            }
        }
    }
    private void Death() {
        if(_dead) return;
        _dead = true;
        otherBall.OtherDead();
        _ballGraphic.Death();
    }
    private void OtherDead() {
        _dead = true;
        _ballGraphic.Shocked();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.relativeVelocity.magnitude > gameSettings.impactToTriggerCrash) {
            _ballGraphic.Crashed();
            _audioSource.PlayOneShot(gameSettings.crashClip);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (LapsMath.LayerMaskContains(gameSettings.stickableLayers, other.gameObject.layer)) {
            _stickableSurfaces.Add(other);
        }
        if (LapsMath.LayerMaskContains(gameSettings.deadlyLayers, other.gameObject.layer)) {
            Death();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (LapsMath.LayerMaskContains(gameSettings.stickableLayers, other.gameObject.layer)) {
            _stickableSurfaces.Remove(other);
        }
    }
}
