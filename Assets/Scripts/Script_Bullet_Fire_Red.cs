using UnityEngine;

public class Script_Bullet_Fire_Red : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    public float SpeedBullet;
    public float SpeedBulletx2;
    [SerializeField] Transform Player;
    [SerializeField] private float spawnOffset = 0.5f;
    public bool On_Off_Scale;
    public BulletPool bulletPool; 
    [SerializeField] SpriteRenderer spriteRenderer;
  
    void Start()
    {

    }

    // Update is called once per frame
 public void Update_Bullet()
{
    GameObject SpawnBullet = null;

    // Nếu có BulletPool, lấy từ pool, ngược lại instantiate mới
    if (bulletPool != null)
    {
        SpawnBullet = bulletPool.GetBullet();
        float dirForPos = On_Off_Scale ? -1f : 1f;
        Vector3 spawnPos = Player.position + new Vector3(dirForPos * spawnOffset, 0f, 0f);
        SpawnBullet.transform.position = spawnPos;
        SpawnBullet.transform.rotation = Quaternion.identity;
        SpawnBullet.SetActive(true);
        // Gán pool reference cho bullet để nó có thể trả về
        Bullet bulletComp = SpawnBullet.GetComponent<Bullet>();
        if (bulletComp != null)
            bulletComp.pool = bulletPool;
    }
    else
    {
        float dirForPos = On_Off_Scale ? -1f : 1f;
        Vector3 spawnPos = Player.position + new Vector3(dirForPos * spawnOffset, 0f, 0f);
        SpawnBullet = Instantiate(Bullet, spawnPos, Quaternion.identity);
    }

    // Đảm bảo đạn không parent với Player
    SpawnBullet.transform.parent = null;

    Rigidbody2D rb = SpawnBullet.GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        rb.linearVelocity = Vector2.zero;      // reset velocity cũ
        rb.angularVelocity = 0f;         // reset góc quay
        rb.gravityScale = 0f;            // tắt trọng lực để đạn bay thẳng

        // Tính hướng bắn dựa trên hướng nhân vật hiện tại
        float dir = On_Off_Scale ? -1f : 1f;
        rb.linearVelocity = new Vector2(dir * SpeedBullet * SpeedBulletx2, 0f);
    }

    // Bỏ va chạm giữa viên đạn và player (ngăn đạn bị đẩy/va chạm ngay khi spawn gần player)
    Collider2D playerCol = Player != null ? Player.GetComponent<Collider2D>() : null;
    Collider2D bulletCol = SpawnBullet.GetComponent<Collider2D>();
    if (playerCol != null && bulletCol != null)
    {
        Physics2D.IgnoreCollision(bulletCol, playerCol, true);
    }

    SpriteRenderer sr = SpawnBullet.GetComponent<SpriteRenderer>();
    if (sr != null)
        sr.flipX = On_Off_Scale;

    if (AudioManager.Instance != null && AudioManager.Instance.shootClip != null)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.shootClip);
    }
}

   
     
}
