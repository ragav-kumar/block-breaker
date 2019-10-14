using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // config params
    [SerializeField] float screenWidthInUnits;
    [SerializeField] float baseSizeInUnits = 1;
    [Tooltip("Center Piece is rendered this many times, plus 1")]
    [Range(1,4)][SerializeField] int paddleLength = 1;
    [Tooltip("Multiplier on how much paddle velocity contributes to ball angle change")]
    [SerializeField] float velocityContribution = 0;
    [Tooltip("Multiplier on how much position on paddle contributes to ball angle change")]
    [SerializeField] float positionContribution = 0;

    //state
    private SpriteRenderer sprite;
    private GameState state;
    //private Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        state = FindObjectOfType<GameState>();
        // Assumes unique ball. No good.
        //ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePaddle();
    }

    private void MovePaddle()
    {
        float mousePos = Mathf.Clamp(getXPos(), 0 + sprite.bounds.extents.x, screenWidthInUnits - sprite.bounds.extents.x);
        transform.position = new Vector2(mousePos, transform.position.y);
    }
    private float getXPos() {
        //if (state.isAutoplayEnabled())
        //{
        //    return ball.transform.position.x;
        //}
        //else
        //{
        //    return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        //}
        return Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
}
