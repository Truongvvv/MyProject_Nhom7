using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    public float speed = 20f;         // Tốc độ xe
    public float turnSpeed = 50f;     // Tốc độ quay xe
    public float gravityForce = 10f;  // Lực trọng trường tùy chỉnh
    public float boostSpeed = 20f;    // Mức tăng tốc khi nhấn F

    private Rigidbody rb;
    private float horizontalInput;
    private bool isBoosting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // Tắt trọng lực mặc định của Unity
        
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

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

        // Áp dụng trọng lực tùy chỉnh
        rb.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);

        // Giữ nguyên vận tốc theo trục Y, chỉ điều khiển tốc độ ngang
        Vector3 horizontalVelocity = transform.forward * currentSpeed;
        rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);

        // Điều khiển xoay xe
        transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);
    }
}
