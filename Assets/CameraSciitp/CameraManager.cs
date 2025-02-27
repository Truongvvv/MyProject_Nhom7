
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target; // Xe cần theo dõi
    public Vector3 offset = new Vector3(0, 5, -10); // Vị trí offset phía sau xe
    public float smoothSpeed = 5f; // Tốc độ mượt mà

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera target is missing!");
            return;
        }

        // Tính toán vị trí mong muốn
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Di chuyển camera mượt mà đến vị trí mong muốn
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Xoay camera theo hướng của xe
        transform.LookAt(target.position + target.forward * 5f);
    }
}
