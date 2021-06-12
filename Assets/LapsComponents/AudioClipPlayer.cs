using System;
using System.Collections;
using System.Collections.Generic;
using LapsRuntime;
using UnityEngine;

public class AudioClipPlayer : LapsComponent {
    public AudioClip clip;
    public bool playOnAwake = true;
    
    private AudioSource _audioSource;
    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) {
            _audioSource = this.gameObject.AddComponent<AudioSource>();
        }
    }
}
