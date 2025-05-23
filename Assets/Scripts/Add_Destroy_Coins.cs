using UnityEngine;

public class Add_Destroy_Coins : MonoBehaviour
{
    [SerializeField] Script_Coins script_Coins;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            script_Coins.Add_Amount();
        }
    }
}
