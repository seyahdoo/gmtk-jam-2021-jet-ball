using System.Collections.Generic;
using LapsRuntime;
using UnityEngine;

public class AudioClipPlayer : LapsComponent {
    public AudioClip[] startClips;
    public AudioClip[] loopClips;
    public AudioClip[] endClips;
    public bool playOnAwake = true;
    
    private AudioSource _audioSource;
    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) {
            _audioSource = this.gameObject.AddComponent<AudioSource>();
        }
    }
    public void Play() {
        
    }
    public void Stop() {
        
    }
    public void StopImmidiately() {
        
    }
    public override void GetInputSlots(List<LogicSlot> slots) {
        slots.Add(new LogicSlot("play", 0));
        slots.Add(new LogicSlot("stop", 1));
        slots.Add(new LogicSlot("stop immidiately", 2));
    }
    public override void GetOutputSlots(List<LogicSlot> slots) {
        slots.Add(new LogicSlot("on finished", 0));
    }
}
