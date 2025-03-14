using UnityEngine;

public class CarScritp : MonoBehaviour
{
    [Header("Cấu hình Chuyển hướng")]
    public float normalTurnSpeed = 50f;          // Tốc độ quay khi không drift

    [Header("Cấu hình Drift")]
    public KeyCode driftKey = KeyCode.LeftShift; // Phím kích hoạt drift
    public float driftThreshold = 0.1f;            // Ngưỡng input ngang để kích hoạt drift
    public float driftTurnMultiplier = 3f;         // Hệ số tăng tốc quay khi drift
    public float driftLateralForce = 15f;          // Lực tác động bên khi drift

    private Rigidbody rb;
    private float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Lấy input chuyển hướng
        horizontalInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // Kiểm tra điều kiện drift: nhấn phím drift và đủ input chuyển hướng
        if (Input.GetKey(driftKey) && Mathf.Abs(horizontalInput) > driftThreshold)
        {
            // Tăng tốc độ quay khi drift
            float driftTurnSpeed = normalTurnSpeed * driftTurnMultiplier;
            transform.Rotate(Vector3.up * horizontalInput * driftTurnSpeed * Time.deltaTime);

            // Áp dụng lực bên để tăng khả năng chuyển hướng (hiệu ứng drift)
            Vector3 lateralForce = transform.right * horizontalInput * driftLateralForce;
            rb.AddForce(lateralForce, ForceMode.Acceleration);
        }
        else
        {
            // Khi không drift, chuyển hướng bình thường
            transform.Rotate(Vector3.up * horizontalInput * normalTurnSpeed * Time.deltaTime);
        }
    }
}
