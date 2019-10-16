﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    // State
    private int blocks = 0;
    private int balls;

    // cached references
    SceneLoader sceneLoader;
    GameState state;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        state = FindObjectOfType<GameState>();
    }

    public void AddBlock()
    {
        blocks++;
    }
    public void RemoveBlock()
    {
        blocks--;
        state.AddScore();
        if (blocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
    public void AddBall(int number = 1)
    {
        balls += number;
    }
    public void RemoveBall()
    {
        balls--;
        if (balls <= 0)
        {

        }
    }
}
