using UnityEngine;

public class Collider_Knock_Back : MonoBehaviour
{
    public KnockBack player;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
  {
            player = other.gameObject.GetComponent<KnockBack>();
            player.KbCounter = player.KbTotalTime;
            if (other.transform.position.x <= transform.position.x)
            {
                player.KnockFormRight = true;
            }
            if (other.transform.position.x > transform.position.x)
            {
                player.KnockFormRight = false;
            }

        }

    }

}
