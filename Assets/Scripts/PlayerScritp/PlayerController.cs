using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Import UI cho Text

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;         // Tốc độ xe
    public float turnSpeed = 50f;     // Tốc độ quay xe
    public float gravityForce = 10f;  // Lực trọng trường tùy chỉnh
    public float boostSpeed = 20f;    // Mức tăng tốc khi nhấn F
    public GameObject boostEffect;    // Hiệu ứng tăng tốc
    public Text countdownText;        // UI Text hiển thị đếm ngược

    public AudioSource engineAudio;   // Âm thanh chạy xe (lặp lại liên tục)
    public AudioSource boostAudio;    // Âm thanh tăng tốc

    private Rigidbody rb;
    private float horizontalInput;
    private bool isBoosting = false;
    private bool canMove = false;     // Xe không thể chạy khi chưa hết đếm ngược

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;

        // Tắt hiệu ứng boost khi bắt đầu
        if (boostEffect != null)
        {
            boostEffect.SetActive(false);
        }

        // Đảm bảo âm thanh boost không phát sớm
        if (boostAudio != null)
        {
            boostAudio.Stop();
        }

        // Thiết lập và phát âm thanh engine
        if (engineAudio != null)
        {
            engineAudio.loop = true;   // Lặp lại liên tục
            engineAudio.Play();          // Phát ngay khi game bắt đầu (không bị dừng bởi boost)
        }

        // Bắt đầu đếm ngược
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        int countdown = 3;

        while (countdown > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = countdown.ToString(); // Hiển thị số giây còn lại
            }
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        if (countdownText != null)
        {
            countdownText.text = "GO!"; // Hiển thị GO khi bắt đầu
        }

        yield return new WaitForSeconds(0.5f); // Giữ chữ "GO!" trong 0.5s

        if (countdownText != null)
        {
            countdownText.text = ""; // Xóa text sau khi GO!
        }

        canMove = true; // Cho phép xe chạy
    }

    void Update()
    {
        if (!canMove) return; // Không cho xe chạy nếu chưa hết đếm ngược

        horizontalInput = Input.GetAxis("Horizontal");

        // Khi nhấn F: bật tăng tốc, hiệu ứng và âm thanh boost
        if (Input.GetKeyDown(KeyCode.F))
        {
            isBoosting = true;
            if (boostEffect != null)
            {
                boostEffect.SetActive(true);
            }
            if (boostAudio != null && !boostAudio.isPlaying)
            {
                boostAudio.Play();
            }
        }

        // Khi thả F: tắt tăng tốc, hiệu ứng và âm thanh boost
        if (Input.GetKeyUp(KeyCode.F))
        {
            isBoosting = false;
            if (boostEffect != null)
            {
                boostEffect.SetActive(false);
            }
            if (boostAudio != null && boostAudio.isPlaying)
            {
                boostAudio.Stop();
            }
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return; // Không cho xe chạy nếu chưa hết đếm ngược

        float currentSpeed = isBoosting ? speed + boostSpeed : speed;

        rb.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);

        Vector3 horizontalVelocity = transform.forward * currentSpeed;
        rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);

        transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);
    }
}