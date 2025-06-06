using UnityEngine;

public class Transform_Position : MonoBehaviour
{
    [SerializeField] GameObject Player_Position;
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.position = other.GetComponent<PlayerController>().startPosition;
        }
    }
}
