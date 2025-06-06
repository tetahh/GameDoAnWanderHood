using System.Collections;
using TMPro;
using UnityEngine;

public class HelthEnemy : MonoBehaviour
{
    public int healthEnemy;
    private bool isDead = false;
    public Animator animator;
    public int maxHealthEnemy = 3;
    [SerializeField] CheckAttack checkAttack;
    [SerializeField] GameObject On_Off_Collider;
    [SerializeField] namEnemy Move_Enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthEnemy = maxHealthEnemy;
    }

    // Update is called once per frame
    public void TakeDamage(int damageAmount)
    {
    if (isDead) return;

    healthEnemy -= damageAmount;
    if (healthEnemy <= 0)
    {
        isDead = true;
        animator.SetTrigger("enemyDie");
            checkAttack.enabled = false;
        Debug.Log("Enemy đã chết!");
            Move_Enemy.enabled = false;
            On_Off_Collider.SetActive(false);
            Destroy(gameObject,7);
        }
    }  
    }
