using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        // Tự huỷ sau 3 giây để tránh tồn tại mãi
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    // Nếu chạm player thì bỏ qua
    if (collision.gameObject.CompareTag("Player"))
        return;

    // Nếu chạm enemy
    if (collision.gameObject.CompareTag("Enemy"))
    {
        HelthEnemy enemy = collision.gameObject.GetComponent<HelthEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(1);
            Debug.Log("Enemy trúng đạn!");
        }
    }

    Destroy(gameObject); // huỷ đạn sau khi va chạm
}
}