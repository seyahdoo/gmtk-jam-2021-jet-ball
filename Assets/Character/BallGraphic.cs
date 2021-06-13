using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallGraphic : MonoBehaviour {
    public GameSettings gameSettings;

    [NonSerialized] public bool sticking;
    private SpriteRenderer _spriteRenderer;
    private float _nextChangeTime;
    private State _state;
    private enum State {
        Sticking,
        Dead,
        Shocked,
        Neutral,
    }
    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        if ((_state == State.Neutral) && sticking) {
            _nextChangeTime = Time.time;
            _state = State.Sticking;
        }
        if ((_state == State.Sticking) && !sticking) {
            _nextChangeTime = Time.time;
            _state = State.Neutral;
        }
        if (Time.time >= _nextChangeTime) {
            _nextChangeTime = Time.time + gameSettings.spriteChangeTime;
            switch (_state) {
                case State.Sticking:
                    _spriteRenderer.sprite = PickRandomFromArray(gameSettings.stickSprites);
                    break;
                case State.Dead:
                    _spriteRenderer.sprite = PickRandomFromArray(gameSettings.deadSprites);
                    break;
                case State.Shocked:
                    _spriteRenderer.sprite = PickRandomFromArray(gameSettings.shockedSprites);
                    break;
                case State.Neutral:
                    _spriteRenderer.sprite = PickRandomFromArray(gameSettings.neutralSprites);
                    break;
            }
        }
    }
    public void Crashed() {
        _nextChangeTime = Time.time + gameSettings.spriteChangeTime;
        _spriteRenderer.sprite = PickRandomFromArray(gameSettings.crashedSprites);
    }
    public void Death() {
        _state = State.Dead;
        _nextChangeTime = Time.time;
    }
    public void Shocked() {
        _state = State.Shocked;
        _nextChangeTime = Time.time;
    }
    private Sprite PickRandomFromArray(Sprite[] array) {
        return array[Random.Range(0, array.Length)];
    }
}
