using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public GameObject winPanel;  // kéo UI Win Panel vào đây
    [SerializeField] Script_Coins scriptCoins;
    private int totalCoins = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Nếu chưa gán script theo dõi xu thì tìm trong scene
            if (scriptCoins == null)
                scriptCoins = FindObjectOfType<Script_Coins>();

            // Nếu chưa tính tổng xu thì đếm các coin có trong scene (thực hiện 1 lần)
            if (totalCoins == 0)
            {
                Coin[] coins = FindObjectsOfType<Coin>();
                totalCoins = coins != null ? coins.Length : 0;
            }

            // Nếu không có xu trong scene hoặc đã thu hết thì cho win
            int collected = scriptCoins != null ? scriptCoins.Index : 0;
            if (totalCoins == 0 || collected >= totalCoins)
            {
                winPanel.SetActive(true);     // bật màn hình Win
                Time.timeScale = 0f;          // tạm dừng game
            }
            else
            {
                // Chưa đủ xu — có thể thêm feedback cho người chơi
                Debug.Log($"Finish blocked: collected {collected}/{totalCoins} coins");
            }
        }
    }
}
