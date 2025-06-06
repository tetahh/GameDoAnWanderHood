using UnityEngine;
using System.Collections;
public class namEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    bool moveToRight = true;
    Animator animator;
    public LayerMask layerMask;
    public LayerMask playerLayerMask;
    private bool isAttacking = false;
    [SerializeField]
    Vector2 raycastDirection;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private int IndexSmall;
    [SerializeField] private int IndexBig;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        HandleMove();
     
    }
    void HandleMove()
    {

        float moveDirection = moveToRight ? IndexBig : IndexSmall;
        raycastDirection = moveToRight ? Vector2.right : Vector2.left;
        rb.linearVelocity = new Vector2(moveSpeed * moveDirection, GetComponent<Rigidbody2D>().linearVelocity.y);
        animator.SetBool("enemyMoving", true);
        if (moveDirection > 0) spriteRenderer.flipX = true;
        else if (moveDirection < 0) spriteRenderer.flipX = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, 1f, layerMask);
        if (hit)
        {
            moveToRight = !moveToRight;
        }
       
    }
   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.right * 1f);
    }
}
