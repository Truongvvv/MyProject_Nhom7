using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;         // Tốc độ xe
    public float turnSpeed = 50f;     // Tốc độ quay xe
    public float gravityForce = 10f;  // Lực trọng trường
    public float boostSpeed = 20f;    // Mức tăng tốc khi nhấn F

    private Rigidbody rb;
    private float horizontalInput;
    private bool isBoosting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // Bật trọng lực
    }

    void Update()
    {
        // Nhận input từ bàn phím hoặc mobile
        horizontalInput = Input.GetAxis("Horizontal");

        // Kiểm tra nếu nhấn phím F
        if (Input.GetKeyDown(KeyCode.F))
        {
            isBoosting = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            isBoosting = false;
        }
    }

    void FixedUpdate()
    {
        float currentSpeed = isBoosting ? speed + boostSpeed : speed;

        // Xe tự chạy về phía trước
        rb.linearVelocity = transform.forward * currentSpeed + Vector3.down * gravityForce;

        // Điều khiển xe rẽ trái/phải
        transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);
    }
}
