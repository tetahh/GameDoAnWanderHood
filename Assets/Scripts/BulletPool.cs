using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 20;
    public Transform bulletParent;
    public float bulletLifetime = 3f; // Thời gian tồn tại của đạn

    private List<GameObject> bulletPool;

    void Awake()
    {
        InitializePool();
    }

    void InitializePool()
    {
        bulletPool = new List<GameObject>();
        
        if (bulletParent == null)
        {
            GameObject parentObj = new GameObject("BulletPool_Parent");
            bulletParent = parentObj.transform;
        }

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewBullet();
        }
    }

    GameObject CreateNewBullet()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("BulletPrefab is not assigned!");
            return null;
        }

        GameObject obj = Instantiate(bulletPrefab);
        obj.transform.SetParent(bulletParent);
        obj.SetActive(false);
        
        // Thêm component tự động hủy
        BulletAutoDisable autoDisable = obj.GetComponent<BulletAutoDisable>();
        if (autoDisable == null)
        {
            autoDisable = obj.AddComponent<BulletAutoDisable>();
        }
        autoDisable.bulletPool = this;
        autoDisable.lifetime = bulletLifetime;
        
        // Đảm bảo đối tượng có component Bullet để xử lý va chạm/logic
        Bullet bulletComp = obj.GetComponent<Bullet>();
        if (bulletComp == null)
        {
            bulletComp = obj.AddComponent<Bullet>();
        }
        bulletComp.pool = this;
        bulletComp.lifeTime = bulletLifetime;
        
        bulletPool.Add(obj);
        return obj;
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            GameObject bullet = bulletPool[i];
            
            if (bullet == null)
            {
                bulletPool.RemoveAt(i);
                i--;
                continue;
            }

            if (!bullet.activeInHierarchy)
            {
                // Reset timer khi lấy đạn mới
                BulletAutoDisable autoDisable = bullet.GetComponent<BulletAutoDisable>();
                if (autoDisable != null)
                {
                    autoDisable.ResetTimer();
                }
                return bullet;
            }
        }

        Debug.Log("Bullet pool expanded! Creating new bullet.");
        return CreateNewBullet();
    }

    // Hàm để deactivate đạn (được gọi từ BulletAutoDisable)
    public void ReturnBulletToPool(GameObject bullet)
    {
        if (bullet != null)
        {
            // Reset vật lý trước khi tắt
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
            
            bullet.SetActive(false);
        }
    }

    public void DeactivateAllBullets()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (bullet != null && bullet.activeInHierarchy)
            {
                ReturnBulletToPool(bullet);
            }
        }
    }

    public int GetActiveBulletCount()
    {
        int count = 0;
        foreach (GameObject bullet in bulletPool)
        {
            if (bullet != null && bullet.activeInHierarchy)
                count++;
        }
        return count;
    }
}

// Component mới để tự động tắt đạn sau thời gian
public class BulletAutoDisable : MonoBehaviour
{
    public BulletPool bulletPool;
    public float lifetime = 3f;
    private float timer;

    void OnEnable()
    {
        ResetTimer();
    }

    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                if (bulletPool != null)
                {
                    bulletPool.ReturnBulletToPool(gameObject);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void ResetTimer()
    {
        timer = lifetime;
    }
}