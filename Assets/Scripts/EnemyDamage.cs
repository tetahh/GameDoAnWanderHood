using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;
    [SerializeField] private float Timer;
    [SerializeField] private float CurrentTimer;
    [SerializeField] private float SpeedKnockBack;
    [SerializeField] Transform Target;
    [SerializeField] private float Change;
    [SerializeField] KnockBack knockBack;
    void Update()
    {
        if(Timer > 0)
        {
            Timer -= Time.deltaTime; 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Timer <= 0)
            {

                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                   // knockBack = collision.GetComponent<KnockBack>();
                //    knockBack.Float_KnockBack();
                    playerHealth.TakeDamage(damage);
                    Debug.Log("Ui đau quá");
                }
                    Timer = CurrentTimer;
            }
        }
    }



}