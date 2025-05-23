using UnityEngine;

public class KnockBack : MonoBehaviour
{

    [SerializeField] private float Speed;
    public float KbCounter;
    public bool KnockFormRight;
    public float KbTotalTime;
    public float KbForce;
    Vector2 MoveDirection;
    public Rigidbody2D rb;
    public PlayerController playerController;
    // [SerializeField] Transform TargetPosition;
    private void Awake()
    {
    //    TargetPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
       // MoveDirection = new Vector2(MoveX, MoveY).normalized;
    }
    void FixedUpdate()
    {
        if (KbCounter <= 0)
        {
            playerController.HandleMovement();
        }
        else
        {
            if (KnockFormRight == true)
            {
                rb.linearVelocity = new Vector2(-KbForce, KbForce);
            }
            if (KnockFormRight == false)
            {
                rb.linearVelocity = new Vector2(KbForce, KbForce);
            }
            KbCounter -= Time.deltaTime;

        }
    }


}
