using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameState : MonoBehaviour
{
    // Config params
    [Range(0.01f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int blockPointValue = 83;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool autoplayEnabled = false;

    // State variables
    [SerializeField] int currentScore;

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

    public void AddScore(float multiplier = 1f)
    {
        currentScore += (int) multiplier * blockPointValue;
        scoreText.text = currentScore.ToString("000000");
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
