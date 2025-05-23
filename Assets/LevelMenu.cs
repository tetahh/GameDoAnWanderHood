using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
   public void OpenLevel(int levelId)
    {
        string levelName = "LV" + levelId;
        SceneManager.LoadScene(levelName);
    }
}
