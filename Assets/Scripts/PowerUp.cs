using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
#pragma warning disable 0649
    // config params
    [Tooltip("Fall speed. Valuable powerups should fall faster. Default: 2.0")]
    [SerializeField] float dropSpeed = 2f;
    [Tooltip("The effect of this power up")]
    [SerializeField] PowerUpType type;
    [SerializeField] AudioClip acquireSound;
#pragma warning restore 0649
    // cached refs
    private Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody2D.velocity = new Vector2(0, -1 * dropSpeed);
    }
    public void Remove()
    {
        //TODO: Add a sound effect here!
        Destroy(gameObject);
    }
    public PowerUpType GetPowerUpType()
    {
        return type;
    }
}
