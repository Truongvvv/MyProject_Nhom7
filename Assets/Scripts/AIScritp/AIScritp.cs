using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 10.0f;
    public float rotationSpeed = 5.0f;
    public float waypointThreshold = 1.0f;

    private int currentWaypoint = 0;
    private bool canMove = false; // Chặn AI chạy trước khi hết đếm ngược

    void Start()
    {
        StartCoroutine(WaitForCountdown());
    }

    IEnumerator WaitForCountdown()
    {
        CoudownText countdown = FindObjectOfType<CoudownText>();
        while (countdown != null && !countdown.IsCountdownFinished())
        {
            yield return null; // Chờ đến khi đếm ngược xong
        }
        canMove = true; // Cho phép AI xe chạy
    }

    void Update()
    {
        if (!canMove) return; // Nếu chưa hết đếm ngược, AI không chạy

        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform targetWaypoint = waypoints[currentWaypoint];
        Vector3 direction = targetWaypoint.position - transform.position;

        if (direction.magnitude < waypointThreshold)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            return;
        }

        Vector3 move = direction.normalized * speed * Time.deltaTime;
        transform.position += move;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}