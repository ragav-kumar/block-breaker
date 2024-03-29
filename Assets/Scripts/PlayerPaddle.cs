﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
#pragma warning disable 0649
    // config params
    [Header("Paddle Size")]
    [SerializeField] GameObject[] paddles = new GameObject[4];
    [Range(0, 3)] [SerializeField] int paddleSize = 1; // Index into paddles
    [SerializeField] float moveSpeed = 1.0f;

    [Header("Bounce Physics")]
    [Tooltip("Multiplier on how much paddle velocity contributes to ball angle change")]
    [SerializeField] float velocityContribution = 0;
    [Tooltip("Multiplier on how much position on paddle contributes to ball angle change")]
    [SerializeField] float positionContribution = 0;
#pragma warning restore 0649
    private float xMin, xMax;
    private float lastX;
    private float lastDirection = 1.0f;

    // cached references
    private SpriteRenderer sprite;
    private GameState state;
    private Level level;

    // Start is called before the first frame update
    void Start()
    {
        state = FindObjectOfType<GameState>();
        level = FindObjectOfType<Level>();
        // Start by disabling all paddles
        foreach (GameObject paddle in paddles)
        {
            paddle.SetActive(false);
        }
        SetPaddleSize(paddleSize);
        SetUpMoveBoundaries();
        lastX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        MovePaddle();
    }
    private void LateUpdate()
    {
        // last X
        lastX = transform.position.x;
    }

    public float GetVelocity()
    {
        return (transform.position.x - lastX) / Time.deltaTime;
    }
    public float GetLastMoveDirection()
    {
        return lastDirection;
    }

    private void SetPaddleSize(int newSize)
    {
        // deactivate current paddle, activate new one in its place
        paddles[paddleSize].SetActive(false);
        paddles[newSize].SetActive(true);
        paddleSize = newSize;
        sprite = paddles[paddleSize].GetComponent<SpriteRenderer>();
    }
    private void ChangeSize(int delta)
    {
        int newSize = paddleSize + delta;
        newSize = Math.Max(newSize, 0);
        newSize = Math.Min(paddles.Length - 1, newSize);
        SetPaddleSize(newSize);
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        var min = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        var max = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        xMin = min.x + sprite.bounds.extents.x;
        xMax = max.x - sprite.bounds.extents.x;
    }

    private void MovePaddle()
    {
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.x = Mathf.Clamp(targetPos.x, xMin, xMax);
        targetPos.y = transform.position.y;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        if (targetPos.x != transform.position.x)
        {
            lastDirection = Mathf.Sign(targetPos.x - transform.position.x);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PowerUp p = collision.gameObject.GetComponent<PowerUp>();
        if (p != null)
        {
            // this is a power up! get its type, then destroy it.
            var type = p.GetPowerUpType();
            p.Remove();
            ApplyPowerUp(type);
        }
    }

    private void ApplyPowerUp(PowerUpType type)
    {
        Debug.Log("called");
        switch (type)
        {
            case PowerUpType.SizeUp:
                ChangeSize(1);
                break;
            case PowerUpType.SizeDown:
                ChangeSize(-1);
                break;
            case PowerUpType.BallSpeedUp:
                //TODO
                break;
            case PowerUpType.BallSpeedDown:
                //TODO
                break;
            case PowerUpType.MultiBall:
                level.AddBall(3);
                break;
            case PowerUpType.BlastBall:
                //TODO
                break;
            case PowerUpType.Laser:
                //TODO
                break;
            case PowerUpType.ExtraBall:
                state.AddExtraBalls(1);
                break;
            case PowerUpType.ExtraPaddle:
                //TODO
                break;
            case PowerUpType.Score100:
                state.AddScore(100);
                break;
            case PowerUpType.Score250:
                state.AddScore(250);
                break;
            case PowerUpType.Inversion:
                // TODO
                break;
            case PowerUpType.None:
            default:
                break;
        }
    }
    /*private float getXPos() {
       if (state.isAutoplayEnabled())
       {
           return ball.transform.position.x;
       }
       else
       {
           return Input.mousePosition.x / Screen.width * screenWidthInUnits;
       }
    }*/
    public void SpawnAttachedBall(Ball baseBall)
    {
        var y = transform.position.y + sprite.bounds.extents.y;
        y += baseBall.GetComponent<SpriteRenderer>().bounds.extents.y;
        var x = transform.position.x;
        var ball = baseBall.Create(this, new Vector2(x,y));
    }
}
