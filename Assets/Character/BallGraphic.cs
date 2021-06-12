using UnityEngine;
using Random = UnityEngine.Random;

public class BallGraphic : MonoBehaviour {
    public Sprite[] stickSprites;
    public Sprite[] crashedSprites;
    public Sprite[] neutralSprites;
    public float changeTime = 1f;
    public float crashStateTime = .4f;
    public bool sticking;
    public SpriteRenderer spriteRenderer;

    private float _nextChangeTime;
    private bool _inStickSprite;
    private void Update() {
        if (sticking != _inStickSprite) {
            _nextChangeTime = Time.time + changeTime;
            _inStickSprite = sticking;
            if (sticking) {
                spriteRenderer.sprite = PickRandomFromArray(stickSprites);
            }
            else {
                spriteRenderer.sprite = PickRandomFromArray(neutralSprites);
            }
        }
        if (Time.time > _nextChangeTime) {
            _nextChangeTime = Time.time + changeTime;
            if (sticking) {
                spriteRenderer.sprite = PickRandomFromArray(stickSprites);
            }
            else {
                spriteRenderer.sprite = PickRandomFromArray(neutralSprites);
            }
        }
    }
    public void Crashed() {
        _nextChangeTime = Time.time + changeTime;
        spriteRenderer.sprite = PickRandomFromArray(crashedSprites);
    }
    public Sprite PickRandomFromArray(Sprite[] array) {
        return array[Random.Range(0, array.Length)];
    }
}
