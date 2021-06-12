using System.Collections.Generic;
using LapsRuntime;
using UnityEngine;

public class Collider2DListener : LapsComponent {
    public bool activateOnce;
    public LayerMask layerMask;
    public bool _enabled = true;
    private void OnTriggerEnter2D(Collider2D other) {
        if (_enabled) {
            if (LapsMath.LayerMaskContains(layerMask, other.gameObject.layer)) {
                if (activateOnce) {
                    _enabled = false;
                }
                FireOutput(0, other);
            }
        }
    }
    public override void GetOutputSlots(List<LogicSlot> slots) {
        slots.Add(new LogicSlot("on trigger enter", 0, typeof(Collider2D)));
    }
}
