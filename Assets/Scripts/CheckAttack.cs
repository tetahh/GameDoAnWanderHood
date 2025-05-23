using UnityEngine;

public class CheckAttack : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] private float Index;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] Color colorAttack;
    [SerializeField] Animator animator;
    [SerializeField] EnemyDamage enemyDamage;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        enemyDamage.enabled = false;
    }

    void Update()
    {

        GameObject[] Enimes = GameObject.FindGameObjectsWithTag("Player");
        float ShortDistance = Mathf.Infinity;
        GameObject ResetEnemy = null;
        foreach(GameObject Enemy in Enimes)
        {
            
            float distance = Vector3.Distance(Enemy.transform.position, transform.position);
            if(ShortDistance > distance)
            {
                ShortDistance = distance;
                ResetEnemy = Enemy;
            }
             if(ResetEnemy != null && ShortDistance < Index)
            {
                animator.SetTrigger("enemyAttack");
                enemyDamage.enabled = true;
            }
            else
            {
                enemyDamage.enabled = false;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = colorAttack;
        Gizmos.DrawWireSphere(transform.position, Index);
    }
}
