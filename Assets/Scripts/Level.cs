using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] int blocks = 0;

    SceneLoader sceneLoader;
    GameState state;
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        state = FindObjectOfType<GameState>();
    }

    public void addBlock()
    {
        blocks++;
    }
    public void removeBlock()
    {
        blocks--;
        state.AddScore();
        if (blocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
