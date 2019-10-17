using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
#pragma warning disable 0649
    //config params
    [SerializeField] PlayerPaddle paddle;
	[SerializeField] Vector2 launchVector = new Vector2(2, 10);
	[SerializeField] AudioClip[] collisionAudio;
	[SerializeField] float randomBounceFactor = 0f;
#pragma warning restore 0649
    //state
    private Vector2 paddleToBallVector;
	private bool ballLocked;

	//cached component references
	AudioSource myAudioSource;
	Rigidbody2D myRigidBody2D;
    private Level level;
	// Start is called before the first frame update
	void Start()
	{
		paddleToBallVector = transform.position - paddle.transform.position;
		ballLocked = true;
		myAudioSource = GetComponent<AudioSource>();
		myRigidBody2D = GetComponent<Rigidbody2D>();
	}

    public void Freeze()
    {
        myRigidBody2D.velocity = new Vector2(0, 0);
        myRigidBody2D.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
	{
		if (ballLocked)
		{
			LockToPaddle();
			LaunchOnClick();
		}
	}
    public Ball Create(PlayerPaddle paddle, Vector2 position)
    {
        var newBall = Instantiate<Ball>(this, position, transform.rotation);
        newBall.AttachPaddle(paddle);
        newBall.Lock();
        return newBall;
    }
    public void AttachPaddle(PlayerPaddle paddle)
    {
        this.paddle = paddle;
    }
	public bool isLaunched()
	{
		return !ballLocked;
	}
    public void Lock()
    {
        ballLocked = true;
    }

	private void LockToPaddle()
	{
		Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
		transform.position = paddlePos + paddleToBallVector;
	}
	private void LaunchOnClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ballLocked = false;
            
            myRigidBody2D.velocity = new Vector2(launchVector.x * Mathf.Sign(paddle.GetLastMoveDirection()), launchVector.y);
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 velocityTweak = new Vector2(
			Random.Range(0f, randomBounceFactor),
			Random.Range(0f, randomBounceFactor)
		);
		if (!ballLocked)
		{
			AudioClip clip = collisionAudio[UnityEngine.Random.Range(0,collisionAudio.Length)];
			myAudioSource.PlayOneShot(clip);
			myRigidBody2D.velocity += velocityTweak;
		}
	}
}
