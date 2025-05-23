using System.Collections;
using UnityEngine;

public class win : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
   private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        StartCoroutine(WinSequence());
    }
}

private IEnumerator WinSequence()
{
    audioManager.PlaySFX(audioManager.winclip);

    // Chờ đúng thời lượng của clip
    yield return new WaitForSeconds(audioManager.winclip.length);

    // Sau khi tiếng win phát xong, mới chuyển scene
    SceneController.instance.NextLevel();
}
    
}
