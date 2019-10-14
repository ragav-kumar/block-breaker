using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	//config params
	[SerializeField] Paddle paddle;
	[SerializeField] Vector2 launchVector;
	[SerializeField] AudioClip[] collisionAudio;
	[SerializeField] float randomBounceFactor = .2f;

	//state
	private Vector2 paddleToBallVector;
	private bool ballLocked;

	//cached component references
	AudioSource myAudioSource;
	Rigidbody2D myRigidBody2D;
	// Start is called before the first frame update
	void Start()
	{
		paddleToBallVector = transform.position - paddle.transform.position;
		ballLocked = true;
		myAudioSource = GetComponent<AudioSource>();
		myRigidBody2D = GetComponent<Rigidbody2D>();
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
	public bool isLaunched()
	{
		return !ballLocked;
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
			myRigidBody2D.velocity = launchVector;
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
