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
        if (other.CompareTag("Player"))
        {
            string carName = other.gameObject.name;
            if (!ranking.Contains(carName))
            {
                ranking.Add(carName);
                leaderboardUI.UpdateLeaderboard(carName); // Cập nhật UI
            }
        }
        if (other.CompareTag("PlayerCar"))
        {
            string carName = other.gameObject.name;
            if (!ranking.Contains(carName))
            {
                Time.timeScale = 0f;
                ranking.Add(carName);
                leaderboardUI.UpdateLeaderboard(carName);
                leaderboardUI.ShowLeaderboard();// Cập nhật UI
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
