using System.Collections;
using System.Collections.Generic;
using LapsRuntime;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject {
    [Header("Gameplay Settings")]
    public float inputToForceRatio = 60f;
    public LayerMask stickableLayers;
    public LayerMask deadlyLayers;
    public float impactToTriggerCrash = 15f;
    public InputSettings inputSettings;

    [Header("Player Render Settings")]
    public Sprite[] stickSprites;
    public Sprite[] crashedSprites;
    public Sprite[] neutralSprites;
    public Sprite[] deadSprites;
    public Sprite[] shockedSprites;
    public float spriteChangeTime = 1f;
    public float crashStateTime = .4f;
    
    [Header("Audio Settings")]
    public AudioClip stickClip;
    public AudioClip crashClip;
    
}
