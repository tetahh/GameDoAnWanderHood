using UnityEngine;

public class Transform_Position : MonoBehaviour
{
    [SerializeField] GameObject Player_Position;
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player_Position.transform.position = new(-9.71f, 3.7f, -0.7f);
        }
    }
}
