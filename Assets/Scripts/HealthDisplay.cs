using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public PlayerHealth playerHealth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Tự động tìm PlayerHealth component nếu chưa gán
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }

        // Log chi tiết để debug
        Debug.Log($"[HealthDisplay] Start() - PlayerHealth: {(playerHealth != null ? "OK" : "NULL")}, hearts.Length: {(hearts != null ? hearts.Length : 0)}, fullHeart: {(fullHeart != null ? "OK" : "NULL")}, emptyHeart: {(emptyHeart != null ? "OK" : "NULL")}");

        if (playerHealth == null)
            Debug.LogError("[HealthDisplay] PlayerHealth not found! Không thể hiển thị máu.");
        if (hearts == null || hearts.Length == 0)
            Debug.LogError("[HealthDisplay] Hearts array is null or empty! Hãy kéo các Image vào mảng hearts trong Inspector.");
        if (fullHeart == null)
            Debug.LogError("[HealthDisplay] fullHeart sprite is not assigned! Hãy gán sprite vào fullHeart trong Inspector.");
        if (emptyHeart == null)
            Debug.LogError("[HealthDisplay] emptyHeart sprite is not assigned! Hãy gán sprite vào emptyHeart trong Inspector.");
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra null toàn diện
        if (playerHealth == null || hearts == null || hearts.Length == 0 || fullHeart == null || emptyHeart == null)
            return;

        health = playerHealth.health;
        maxHealth = playerHealth.maxHealth;

        // Duyệt qua các phần tử trong array (không vượt quá độ dài)
        for (int i = 0; i < hearts.Length; i++)
        {
            // Bỏ qua phần tử null mà không log warning
            if (hearts[i] == null)
                continue;

            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // Hiển thị trái tim nếu i < maxHealth, ngược lại ẩn đi
            if(i < maxHealth)
            {
                hearts[i].enabled = true;
            }    
            else
            {
                hearts[i].enabled = false;
            }    
        }
    }
}
