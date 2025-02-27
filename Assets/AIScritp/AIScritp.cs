using System.Collections.Generic;
using UnityEngine;

public class AIScritp : MonoBehaviour
{   
    // Mảng chứa các điểm trên đường đã vẽ (waypoints)
    public Transform[] waypoints;

    // Tốc độ di chuyển của xe
    public float speed = 10.0f;

    // Tốc độ quay hướng về điểm tiếp theo
    public float rotationSpeed = 5.0f;

    // Ngưỡng khoảng cách để coi xe đã đến gần waypoint hiện tại
    public float waypointThreshold = 1.0f;

    // Chỉ số của waypoint hiện tại mà xe đang hướng tới
    private int currentWaypoint = 0;


    void Update()
    {
        // Kiểm tra nếu không có waypoint nào được gán
        if (waypoints == null || waypoints.Length == 0)
            return;

        // Lấy waypoint hiện tại
        Transform targetWaypoint = waypoints[currentWaypoint];
        Vector3 direction = targetWaypoint.position - transform.position;

        // Nếu xe đã gần waypoint hiện tại, chuyển sang waypoint kế tiếp
        if (direction.magnitude < waypointThreshold)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            return;
        }

        // Di chuyển xe về hướng waypoint
        Vector3 move = direction.normalized * speed * Time.deltaTime;
        transform.position += move;

        // Xoay xe dần dần về hướng của waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}