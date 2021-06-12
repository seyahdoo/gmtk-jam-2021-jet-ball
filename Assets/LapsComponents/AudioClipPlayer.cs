using System.Collections.Generic;
using LapsRuntime;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioClipPlayer : LapsComponent {
    public AudioClip clip;
    public bool playOnAwake = false;
    
    private AudioSource _audioSource;
    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }
    public void Play() {
        _audioSource.PlayOneShot(clip);
    }
    public void Stop() {
        
    }
    public void StopImmidiately() {
        
    }
    public override object HandleInput(int slotId, object parameter, LapsComponent eventSource) {
        switch (slotId) {
            case 0: Play(); return null;
            default: return null;
        }
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
