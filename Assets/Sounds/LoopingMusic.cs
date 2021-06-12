using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingMusic : MonoBehaviour {
    public AudioClip startClip;
    public AudioClip loopClip;
    public AudioSource audioSource;
    private void Awake() {
        audioSource.clip = startClip;
        audioSource.Play();
    }
    private void Update() {
        if (!audioSource.isPlaying) {
            audioSource.clip = loopClip;
            audioSource.Play();
        }
    }
}
