using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoudownText : MonoBehaviour
{
    public Text countdownText; // UI Text để hiển thị đếm ngược
    public float countdownTime = 3f;
    private bool countdownFinished = false;

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        float countdown = countdownTime;
        while (countdown > 0)
        {
            countdownText.text = Mathf.Ceil(countdown).ToString(); // Làm tròn số hiển thị
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.5f); // Hiển thị "GO!" trong 0.5s
        countdownText.text = ""; // Xóa chữ "GO!" sau khi bắt đầu

        countdownFinished = true; // Cho phép game bắt đầu
    }

    public bool IsCountdownFinished()
    {
        return countdownFinished;
    }
}
