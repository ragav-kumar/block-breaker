using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoseCollider : MonoBehaviour
{
    //cached references
    private GameState state;

    private void Start()
    {
        state = FindObjectOfType<GameState>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collidingObject = collision.gameObject.GetComponent<Ball>();
        if (collidingObject != null) // collidingObject has a Ball component
        {
            state.LoseBallInPlay();
        }
    }
}
