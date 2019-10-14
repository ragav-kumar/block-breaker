using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    // Config params
    [Range(0.01f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int blockPointValue = 83;
    [SerializeField] int maxExtraBalls = 9;
    [SerializeField] TextMeshProUGUI extraBallsText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool autoplayEnabled = false;

    // State variables
    private int currentScore;
    private int ballsInPlay = 1;
    private int extraBalls = 3;

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
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddScore(float multiplier = 1f)
    {
        currentScore += (int) multiplier * blockPointValue;
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
    public void AddBallsInPlay(int num)
    {
        // TODO: Ball spawn
        ballsInPlay += Math.Abs(num);
    }
    public void LoseBallInPlay()
    {
        ballsInPlay--;
        if (ballsInPlay <= 0)
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
}
