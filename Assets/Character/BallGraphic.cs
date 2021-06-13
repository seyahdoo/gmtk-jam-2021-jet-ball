using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallGraphic : MonoBehaviour {
    public GameSettings gameSettings;

    [NonSerialized] public bool sticking;
    private SpriteRenderer _spriteRenderer;
    private float _nextChangeTime;
    private bool _inStickSprite;
    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        if (sticking != _inStickSprite) {
            _nextChangeTime = Time.time + gameSettings.spriteChangeTime;
            _inStickSprite = sticking;
            if (sticking) {
                _spriteRenderer.sprite = PickRandomFromArray(gameSettings.stickSprites);
            }
            else {
                _spriteRenderer.sprite = PickRandomFromArray(gameSettings.neutralSprites);
            }
        }
        if (Time.time > _nextChangeTime) {
            _nextChangeTime = Time.time + gameSettings.spriteChangeTime;
            if (sticking) {
                _spriteRenderer.sprite = PickRandomFromArray(gameSettings.stickSprites);
            }
            else {
                _spriteRenderer.sprite = PickRandomFromArray(gameSettings.neutralSprites);
            }
        }
    }
    public void Crashed() {
        _nextChangeTime = Time.time + gameSettings.spriteChangeTime;
        _spriteRenderer.sprite = PickRandomFromArray(gameSettings.crashedSprites);
    }
    public void Death() {
        
    }
    public void Shocked() {
        
    }
    private Sprite PickRandomFromArray(Sprite[] array) {
        return array[Random.Range(0, array.Length)];
    }
}
