using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoseCollider : MonoBehaviour
{
    //cached references
    private Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collidingObject = collision.gameObject.GetComponent<Ball>();
        if (collidingObject != null) // collidingObject has a Ball component
        {
            level.RemoveBall();
        }
    }
}
