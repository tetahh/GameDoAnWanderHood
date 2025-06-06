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

    public SpriteRenderer playerSr;
    public PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        knockback = GetComponent<KnockBack>();
    }

    // Update is called once per frame
    public void TakeDamage(int amount)
    {
        if (isDead) return;
        health -= amount;

        if (health <= 0)
        {
            isDead = true;
            gameObject.tag = "Dead";
            // Kích hoạt hoạt ảnh Die
            animator.SetTrigger("die");

            playerController.enabled = false;
            Debug.Log("Player đã chết!");

            StartCoroutine(DelayBeforeHide(1.5f));
            
        }
        
    }
    private IEnumerator DelayBeforeHide(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerSr.enabled = false;
        // Gọi sau khi hoạt ảnh chết đã hoàn tất
        OnPlayerDeath?.Invoke();
    }
    
}
