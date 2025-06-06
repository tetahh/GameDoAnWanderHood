using UnityEngine;
using TMPro;
public class Script_Coins : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] GameObject Collider_Scene;
    public int Index;
    void Update()
    {
        Update_Text_Coin(Index.ToString());
        if (Index <= 9)
        {
            Collider_Scene.gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            Collider_Scene.gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }
    public void Add_Amount()
    {
        Index++;
    }
    private void Update_Text_Coin(string text)
    {
        textComponent.gameObject.GetComponent<TextMeshProUGUI>().text = text;
    } 
}
