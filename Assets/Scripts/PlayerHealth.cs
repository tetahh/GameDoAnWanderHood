using UnityEngine;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 5;
    public Animator animator;
    private bool isDead = false;
    public static event Action OnPlayerDeath;

    private KnockBack knockback;

    [Header("Sprite / Flash settings")]
    public SpriteRenderer playerSr;                    // kéo SpriteRenderer vào Inspector (hoặc auto tìm)
    public bool autoFindSpriteRenderer = true;         // nếu true, script sẽ tự tìm SpriteRenderer con
    public int flashes = 4;                            // số lần nhấp nháy
    public float flashInterval = 0.1f;                 // thời gian mỗi trạng thái (đỏ hoặc bình thường)

    // Bảo vệ tránh chồng flash / bất tử tạm thời
    private bool isFlashing = false;
    private bool isInvulnerable = false;
    public bool useInvulnerabilityWhileFlashing = true; // nếu true, không nhận damage trong lúc flash

    public PlayerController playerController;

    void Start()
    {
        health = maxHealth;
        knockback = GetComponent<KnockBack>();

        if (playerSr == null && autoFindSpriteRenderer)
        {
            playerSr = GetComponentInChildren<SpriteRenderer>();
            if (playerSr == null)
                Debug.LogWarning("PlayerHealth: Không tìm thấy SpriteRenderer. Hãy kéo SpriteRenderer vào Inspector.");
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;
        if (useInvulnerabilityWhileFlashing && isInvulnerable) return;

        health -= amount;
        // bắt đầu hiệu ứng flash (và bất tử tạm thời nếu bật)
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            isDead = true;
            gameObject.tag = "Dead";
            animator.SetTrigger("die");
            if (playerController != null) playerController.enabled = false;
            Debug.Log("Player đã chết!");
            StartCoroutine(DelayBeforeHide(1.5f));
        }
    }

    private IEnumerator FlashRed()
    {
        if (playerSr == null)
        {
            // cố gắng lấy lại nếu chưa có
            playerSr = GetComponentInChildren<SpriteRenderer>();
            if (playerSr == null) yield break;
        }

        if (isFlashing) yield break; // tránh chạy chồng
        isFlashing = true;

        if (useInvulnerabilityWhileFlashing) isInvulnerable = true;

        Color original = playerSr.color;

        for (int i = 0; i < flashes; i++)
        {
            playerSr.color = Color.red;
            yield return new WaitForSeconds(flashInterval);
            playerSr.color = original;
            yield return new WaitForSeconds(flashInterval);
        }

        // đảm bảo trả màu gốc
        playerSr.color = original;

        isFlashing = false;
        isInvulnerable = false;
    }

    private IEnumerator DelayBeforeHide(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (playerSr != null) playerSr.enabled = false;
        OnPlayerDeath?.Invoke();
    }
}
