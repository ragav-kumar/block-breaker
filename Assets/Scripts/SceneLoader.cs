using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int max = SceneManager.sceneCountInBuildSettings;
        if (currentSceneIndex + 1 >= max)
        {
            FindObjectOfType<GameState>().DestroySelf();
        }
        SceneManager.LoadScene((currentSceneIndex + 1) % max);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
