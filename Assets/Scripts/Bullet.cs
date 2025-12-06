using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f; // thời gian tồn tại trước khi trả về pool hoặc huỷ
    public BulletPool pool; // nếu không null thì đây là đạn dùng pool

    private Rigidbody2D rb;
    private Coroutine lifeCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // Bắt đầu đếm thời gian tồn tại khi active
        if (lifeCoroutine != null) StopCoroutine(lifeCoroutine);
        lifeCoroutine = StartCoroutine(LifeTimer());
        // Debug: in ra thông tin viên đạn khi spawn
        Debug.Log($"[Bullet] Spawned: name={gameObject.name}, tag={gameObject.tag}, layer={gameObject.layer}, rbType={(rb!=null?rb.bodyType.ToString():"null")}");

        // Kiểm tra overlap ngay khi spawn (trường hợp spawn chồng lên Ground/Tilemap)
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.05f);
        foreach (var h in hits)
        {
            if (h == null) continue;
            Debug.Log($"[Bullet] Overlap at spawn with: name={h.gameObject.name}, tag={h.gameObject.tag}, layer={h.gameObject.layer}");
            if (h.gameObject.CompareTag("Ground") || h.gameObject.CompareTag("Platform") || h.gameObject.CompareTag("Wall") || h.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("[Bullet] Immediate overlap detected -> ReturnToPoolOrDestroy");
                ReturnToPoolOrDestroy();
                return;
            }
        }
    }

    private void OnDisable()
    {
        if (lifeCoroutine != null) StopCoroutine(lifeCoroutine);
        // reset trạng thái vật lý để chuẩn bị tái sử dụng
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnToPoolOrDestroy();
    }

    private void ReturnToPoolOrDestroy()
    {
        if (pool != null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"[Bullet] OnCollisionEnter2D with: name={collision.gameObject.name}, tag={collision.gameObject.tag}, layer={collision.gameObject.layer}");
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

            ReturnToPoolOrDestroy();
            return;
        }

        // Nếu chạm đất hoặc tường thì trả về pool/huỷ
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("[Bullet] Hit ground/wall -> ReturnToPoolOrDestroy");
            ReturnToPoolOrDestroy();
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"[Bullet] OnTriggerEnter2D with: name={collision.gameObject.name}, tag={collision.gameObject.tag}, layer={collision.gameObject.layer}");
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
                Debug.Log("Enemy trúng đạn! (trigger)");
            }

            ReturnToPoolOrDestroy();
            return;
        }

        // Nếu chạm đất hoặc tường thì trả về pool/huỷ
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("[Bullet] Trigger hit ground/wall -> ReturnToPoolOrDestroy");
            ReturnToPoolOrDestroy();
            return;
        }
    }
}