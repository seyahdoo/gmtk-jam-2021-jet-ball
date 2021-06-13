using System.Collections;
using System.Collections.Generic;
using LapsRuntime;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject {
    public float inputToForceRatio = 60f;
    public LayerMask stickableLayers;
    public LayerMask deadlyLayers;
    public AudioClip stickClip;
    public AudioClip crashClip;
    public float impactToTriggerCrash = 15f;
    public InputSettings inputSettings;

}
