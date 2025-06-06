using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public AudioManager audioManager;
    public float moveSpeed;
    public float jumpSpeed;
    public float traiPhai;

    public bool isOnGround = false;
    public bool isFacingRight = true;

    public float dashBoost;
    public float dashTime;
    private float _dashTime;
    bool isDashing = false;

    public Vector3 startPosition;

    [SerializeField] Script_Bullet_Fire_Red script_Bullet_Fire_Red;
    [SerializeField] private float ScaleBulletSmall;
    [SerializeField] private float ScaleBulletBig;
    [SerializeField] SpriteRenderer spriteRenderer;

    public static Action OnPlayerDeath { get; internal set; }


    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (audioManager == null)
        {
            audioManager = FindFirstObjectByType<AudioManager>();
        }
     
    }
    // Update is called once per frame
    void Update()
    {
        
        
            HandleMovement();
            HandleJump();
            HandleAttack();
            HandleDash();
          
       
    }
    public void HandleMovement()
    {
        traiPhai = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveSpeed * traiPhai, rb.linearVelocity.y);

        FlipCharacter(traiPhai);

        animator.SetFloat("dichuyen", Mathf.Abs(traiPhai));
    }

    void FlipCharacter(float direction)
    {
        if (isFacingRight && direction == -1)
        {
            spriteRenderer.flipX = true;
         script_Bullet_Fire_Red.On_Off_Scale = true;
            isFacingRight = false;
        }
        else if (!isFacingRight && direction == 1)
        {
            spriteRenderer.flipX = false;
            script_Bullet_Fire_Red.On_Off_Scale = false;
            isFacingRight = true;
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
            isOnGround = false;
            animator.SetTrigger("jump");

            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.jumpClip);
            }
        }
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
        }
    }

    void HandleDash()
    {
        if (Input.GetMouseButtonDown(1) && _dashTime <= 0 && !isDashing)
        {
            moveSpeed += dashBoost;
            _dashTime = dashTime;
            isDashing = true;
        }

        if (_dashTime <= 0 && isDashing)
        {
            moveSpeed -= dashBoost;
            isDashing = false;
        }
        else if (_dashTime > 0)
        {
            _dashTime -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.transform.CompareTag("Ground") || collision.transform.CompareTag("Platform"))
    {
        isOnGround = true;
    }
    
    if (collision.transform.CompareTag("Platform"))
    {
        StartCoroutine(SetParentNextFrame(collision.transform));
    }
}

    private IEnumerator SetParentNextFrame(Transform newParent)
    {
    yield return null; // đợi đến frame tiếp theo
    transform.SetParent(newParent);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Platform")
        {
            transform.SetParent(null);
        }
    }
    
}
