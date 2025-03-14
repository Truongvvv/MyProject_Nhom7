using UnityEngine;
using System.Collections;

public class BoostScritp : MonoBehaviour
{
    public float baseSpeed = 20f;         // Tốc độ cơ bản
    public float boostMultiplier = 2f;    // Hệ số tăng tốc
    public float boostDuration = 5f;      // Thời gian boost

    private bool isBoosting = false;
    private float currentSpeed;

    void Start()
    {
        currentSpeed = baseSpeed; // Đặt tốc độ ban đầu
    }

    public void ActivateBoost()
    {
        if (!isBoosting)
        {
            StartCoroutine(BoostCoroutine());
        }
    }

    private IEnumerator BoostCoroutine()
    {
        isBoosting = true;
        currentSpeed = baseSpeed * boostMultiplier; // Tăng tốc
        Debug.Log(" Boost Activated!");

        yield return new WaitForSeconds(boostDuration);

        currentSpeed = baseSpeed; // Trở lại tốc độ bình thường
        isBoosting = false;
        Debug.Log(" Boost Ended!");
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
