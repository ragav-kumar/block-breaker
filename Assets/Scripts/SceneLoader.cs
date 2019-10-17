using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int max = SceneManager.sceneCountInBuildSettings;
        if (currentSceneIndex + 1 >= max)
        {
            FindObjectOfType<GameState>().DestroySelf();
        }
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int max = SceneManager.sceneCountInBuildSettings;
        if (currentSceneIndex + 1 >= max)
        {
            FindObjectOfType<GameState>().DestroySelf();
        }
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
