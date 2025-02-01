using System;
using System.Collections.Generic;
using LapsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour {
    public bool isLeft = false;
    public Vector2 input = Vector2.zero;
    public bool stickButtonPressed = false;
    public GameSettings gameSettings;
    public Ball otherBall;
    public AudioSource crashStickAudioSource;
    public AudioSource deadAudioSource;
    public FixedJoint2D joint;
    public VariableJoystick joystick;
    
    private Rigidbody2D _body;
    private BallGraphic _ballGraphic;
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
        if (gameSettings.inputSettings == null) {
            gameSettings.inputSettings = new InputSettings();
        }
        _ballGraphic = GetComponentInChildren<BallGraphic>();
        _body = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        if (Application.isEditor) {
            if (isLeft) {
                input = gameSettings.inputSettings.Character.LeftMovementPC.ReadValue<Vector2>();
            }
            else {
                input = gameSettings.inputSettings.Character.RightMovementPC.ReadValue<Vector2>();
            }
        }
        else {
            if (isLeft) {
                input = gameSettings.inputSettings.Character.LeftMovementWebGL.ReadValue<Vector2>();
            }
            else {
                input = gameSettings.inputSettings.Character.RightMovementWebGL.ReadValue<Vector2>();
            }
        }
        var joystickInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (joystickInput.magnitude > 0.1f) {
            input = joystickInput;
        }
        stickButtonPressed = input.magnitude < 0.1f;
    }
    private void FixedUpdate() {
        if (_dead) return;
        if (stickButtonPressed && _stickableSurfaces.Count >= 1) {
            _body.velocity = Vector2.zero;
            _body.angularVelocity = 0f;
            if (_sticked == false) {
                crashStickAudioSource.PlayOneShot(gameSettings.stickClip);
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
        deadAudioSource.PlayOneShot(gameSettings.deadClip);
        Invoke(nameof(ResetLevel), gameSettings.gameResetDelay);
    }
    private void OtherDead() {
        _dead = true;
        _ballGraphic.Shocked();
    }
    private void Win() {
        _ballGraphic.Win();
        otherBall._ballGraphic.Win();
    }
    private void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.relativeVelocity.magnitude > gameSettings.impactToTriggerCrash) {
            _ballGraphic.Crashed();
            crashStickAudioSource.PlayOneShot(gameSettings.crashClip);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (LapsMath.LayerMaskContains(gameSettings.stickableLayers, other.gameObject.layer)) {
            _stickableSurfaces.Add(other);
        }
        if (LapsMath.LayerMaskContains(gameSettings.deadlyLayers, other.gameObject.layer)) {
            Death();
        }
        if (LapsMath.LayerMaskContains(gameSettings.winLayers, other.gameObject.layer)) {
            Win();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (LapsMath.LayerMaskContains(gameSettings.stickableLayers, other.gameObject.layer)) {
            _stickableSurfaces.Remove(other);
        }
    }
}
