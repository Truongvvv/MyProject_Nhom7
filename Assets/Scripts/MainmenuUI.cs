using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Scene1"); 
    }

    // Hàm xử lý khi nhấn nút Exit
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game đã thoát!"); // Chỉ hiển thị khi chạy ở chế độ Editor
    }
}
