using UnityEngine;

public class Finishline : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject lossPanel;
    private void Start()
    {
        victoryPanel.SetActive(false);
        lossPanel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Kiểm tra nếu xe có tag "Player"
        {
            Time.timeScale = 0f;
            victoryPanel.SetActive(true);
        }
        else if (other.CompareTag("Enemy")) // Kiểm tra nếu xe có tag "Player"
        {
            
            Time.timeScale = 0f;
            lossPanel.SetActive(true);
        }
    }
}
