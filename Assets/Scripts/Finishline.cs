using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Finishline : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject lossPanel;
    public LeaderBoardMG leaderboardUI; // Kéo thả vào Inspector
    private List<string> ranking = new List<string>();
    private void Start()
    {
        victoryPanel.SetActive(false);
        lossPanel.SetActive(false);
        leaderboardUI.HideLeaderboard();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerCar"))
        {
            string carName = other.gameObject.name;

            // Kiểm tra nếu xe chưa có trong danh sách ranking
            if (!ranking.Contains(carName))
            {
                leaderboardUI.UpdateLeaderboard(carName);

                // Chỉ dừng thời gian và hiển thị bảng xếp hạng nếu là "PlayerCar"
                if (other.CompareTag("PlayerCar"))
                {
                    Time.timeScale = 0f;
                    leaderboardUI.ShowLeaderboard();
                }
            }
        }
        //if (other.CompareTag("Player")) // Kiểm tra nếu xe có tag "Player"
        //{
        //    Time.timeScale = 0f;
        //    victoryPanel.SetActive(true);
        //}
        //else if (other.CompareTag("Enemy")) // Kiểm tra nếu xe có tag "Player"
        //{

        //    Time.timeScale = 0f;
        //    lossPanel.SetActive(true);
        //}
    }
}
