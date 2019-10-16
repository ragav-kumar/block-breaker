using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
#pragma warning disable 0649
    // Config params
    [SerializeField] bool autoplayEnabled = false;
    [Header("General")]
    [Range(0.01f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int baseBlockPointValue = 83;
    [Header("UI")]
    [SerializeField] TextMeshProUGUI extraBallsText;
    [SerializeField] TextMeshProUGUI scoreText;
    [Header("Ball")]
    [SerializeField] int startingExtraBalls = 3;
    [SerializeField] int maxExtraBalls = 9;
#pragma warning restore 0649
    // State variables
    private int currentScore;
    private int extraBalls;

    private void Awake()
    {
        int gameStateCount = FindObjectsOfType<GameState>().Length;
        if (gameStateCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject); 
        }
    }
    public bool isAutoplayEnabled() {
        return autoplayEnabled;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        AddScore(0f);
        extraBalls = startingExtraBalls;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddScore(float multiplier = 1f)
    {
        currentScore += (int) multiplier * baseBlockPointValue;
        scoreText.text = currentScore.ToString("000000");
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public void AddExtraBalls(int num)
    {
        extraBalls += num;
        extraBalls = Math.Min(maxExtraBalls, extraBalls);
        extraBallsText.text = extraBalls.ToString();
    }
    public void Die()
    {
        extraBalls--;
        if (extraBalls < 0)
        {
            SceneManager.LoadScene("Game Over");
        }
        else
        {
            extraBallsText.text = extraBalls.ToString();
        }
    }
}
