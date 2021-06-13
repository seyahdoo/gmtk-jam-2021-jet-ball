using UnityEngine;

public class CameraTarget : MonoBehaviour {
    public Ball leftBall;
    public Ball rightBall;
    private void Update() {
        transform.position = (leftBall.transform.position + rightBall.transform.position) / 2;
    }
}
