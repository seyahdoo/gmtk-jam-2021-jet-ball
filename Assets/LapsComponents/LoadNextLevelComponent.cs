using System.Collections.Generic;
using LapsRuntime;
using UnityEngine.SceneManagement;

public class LoadNextLevelComponent : LapsComponent {
    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public override object HandleInput(int slotId, object parameter, LapsComponent eventSource) {
        switch (slotId) {
            case 0:  LoadNextLevel(); return null;
            default: return null;
        }
    }
    public override void GetInputSlots(List<LogicSlot> slots) {
        slots.Add(new LogicSlot("load next level", 0));
    }
}
