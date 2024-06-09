using UnityEngine;

public class Mouse : MonoBehaviour
{
    private float sen = 1200f;
    private float y = 0f;
    public bool isdie = true;

    void Update()
    {
        if (isdie == false) {
            float mx = Input.GetAxis("Mouse X") * sen * Time.deltaTime;
            y += mx;
            transform.localRotation = Quaternion.Euler(0, y, 0);
        }
    }

    public void die() {
        isdie = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
