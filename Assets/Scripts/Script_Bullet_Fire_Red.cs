using UnityEngine;

public class Script_Bullet_Fire_Red : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    public float SpeedBullet;
    public float SpeedBulletx2;
    [SerializeField] Transform Player;
    public bool On_Off_Scale;
    [SerializeField] SpriteRenderer spriteRenderer;
    void Start()
    {

    }

    // Update is called once per frame
    public void Update_Bullet()
    {

        GameObject SpawnBullet = Instantiate(Bullet, Player.position, Quaternion.identity);
        Destroy(SpawnBullet, 2f);
        Rigidbody2D rb = SpawnBullet.GetComponent<Rigidbody2D>();


        if (On_Off_Scale == true)
        {
            rb.linearVelocity = Vector2.left * SpeedBullet * SpeedBulletx2;
            SpawnBullet.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            // spriteRenderer.flipX = true;
        }
        if (On_Off_Scale == false)
        {
            rb.linearVelocity = Vector2.right * SpeedBullet * SpeedBulletx2;
            SpawnBullet.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            // spriteRenderer.flipX = false;

        }

    }
   
     
}
