using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject {
    [Header("Gameplay Settings")]
    public float inputToForceRatio = 60f;
    public LayerMask stickableLayers;
    public LayerMask deadlyLayers;
    public LayerMask winLayers;
    public float impactToTriggerCrash = 15f;
    public InputSettings inputSettings;
    public float gameResetDelay = 2f;

    [Header("Player Render Settings")]
    public Sprite[] stickSprites;
    public Sprite[] crashedSprites;
    public Sprite[] neutralSprites;
    public Sprite[] deadSprites;
    public Sprite[] shockedSprites;
    public Sprite[] winSprites;
    public float spriteChangeTime = 1f;
    public float crashStateTime = .4f;
    
    [Header("Audio Settings")]
    public AudioClip stickClip;
    public AudioClip crashClip;
    public AudioClip deadClip;
}
