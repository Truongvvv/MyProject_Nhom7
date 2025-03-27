using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class LeaderBoardMG : MonoBehaviour
{
    public Text leaderboardText;
    public GameObject leaderboardPanel; // Bảng xếp hạng UI

    private List<string> ranking = new List<string>();

    void Start()
    {
        HideLeaderboard(); // Ẩn bảng xếp hạng khi bắt đầu
    }

    public void UpdateLeaderboard(string carName)
    {
        ranking.Add(carName);
        RefreshUI();
    }

    void RefreshUI()
    {
        for (int i = 0; i < ranking.Count; i++)
        {
            leaderboardText.text += (i + 1) + ". " + ranking[i] + "\n";
        }
    }

    public void ShowLeaderboard()
    {
        leaderboardPanel.SetActive(true); // Hiện bảng xếp hạng
    }

    public void HideLeaderboard()
    {
        leaderboardPanel.SetActive(false); // Ẩn bảng xếp hạng
    }
}
