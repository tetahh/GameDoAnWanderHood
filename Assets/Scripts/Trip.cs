using UnityEngine;

public class Trip : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            KnockBack knockback = collision.GetComponent<KnockBack>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Ui đau quá");
            }
            if (knockback != null)
            {
                knockback.TriggerKnockback(transform); // Gọi knockback từ vị trí gai
            }
        }
    }
}
