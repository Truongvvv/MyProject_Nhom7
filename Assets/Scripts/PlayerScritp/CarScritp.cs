using UnityEngine;

public class CarScritp : MonoBehaviour
{
    [Header("Cấu hình Chuyển hướng")]
    public float normalTurnSpeed = 50f; // Tốc độ quay khi không drift

    [Header("Cấu hình Drift")]
    public KeyCode driftKey = KeyCode.LeftShift; // Phím kích hoạt drift
    public float driftThreshold = 0.1f; // Ngưỡng input ngang để kích hoạt drift
    public float driftTurnMultiplier = 3f; // Hệ số tăng tốc quay khi drift
    public float driftLateralForce = 15f; // Lực tác động bên khi drift

    [Header("Hiệu ứng Drift")]
    public TrailRenderer leftTrail;
    public TrailRenderer rightTrail;

    private Rigidbody rb;
    private float horizontalInput;
    private bool isDrifting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Đảm bảo hiệu ứng trail bị tắt ban đầu
        if (leftTrail != null) leftTrail.emitting = false;
        if (rightTrail != null) rightTrail.emitting = false;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Kiểm tra nếu drift được kích hoạt
        if (Input.GetKey(driftKey) && Mathf.Abs(horizontalInput) > driftThreshold)
        {
            if (!isDrifting)
            {
                isDrifting = true;
                if (leftTrail != null) leftTrail.emitting = true;
                if (rightTrail != null) rightTrail.emitting = true;
            }
        }
        else
        {
            if (isDrifting)
            {
                isDrifting = false;
                if (leftTrail != null) leftTrail.emitting = false;
                if (rightTrail != null) rightTrail.emitting = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (isDrifting)
        {
            float driftTurnSpeed = normalTurnSpeed * driftTurnMultiplier;
            transform.Rotate(Vector3.up * horizontalInput * driftTurnSpeed * Time.deltaTime);

            Vector3 lateralForce = transform.right * horizontalInput * driftLateralForce;
            rb.AddForce(lateralForce, ForceMode.Acceleration);
        }
        else
        {
            transform.Rotate(Vector3.up * horizontalInput * normalTurnSpeed * Time.deltaTime);
        }
    }
}