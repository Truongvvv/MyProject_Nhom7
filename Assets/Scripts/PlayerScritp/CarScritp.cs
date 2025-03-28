using UnityEngine;

public class CarScript : MonoBehaviour
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

    [Header("Âm thanh Drift")]
    public AudioSource driftAudio; // Audio Source phát âm thanh drift

    private Rigidbody rb;
    private float horizontalInput;
    private bool isDrifting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Tắt hiệu ứng vệt bánh xe ban đầu
        if (leftTrail != null) leftTrail.emitting = false;
        if (rightTrail != null) rightTrail.emitting = false;

        // Đảm bảo âm thanh ban đầu tắt
        if (driftAudio != null)
        {
            driftAudio.loop = true; // Lặp liên tục khi drift
            driftAudio.Stop();
        }
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        bool driftCondition = Input.GetKey(driftKey) && Mathf.Abs(horizontalInput) > driftThreshold;

        if (driftCondition && !isDrifting)
        {
            isDrifting = true;
            if (leftTrail != null) leftTrail.emitting = true;
            if (rightTrail != null) rightTrail.emitting = true;

            // Bật âm thanh drift
            if (driftAudio != null && !driftAudio.isPlaying)
            {
                driftAudio.Play();
            }
        }
        else if (!driftCondition && isDrifting)
        {
            isDrifting = false;
            if (leftTrail != null) leftTrail.emitting = false;
            if (rightTrail != null) rightTrail.emitting = false;

            // Tắt âm thanh drift
            if (driftAudio != null && driftAudio.isPlaying)
            {
                driftAudio.Stop();
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