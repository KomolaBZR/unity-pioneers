using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitDefaultMovement : MonoBehaviour
{
	public Transform targetObject;
	public GameObject gameOverWindow;
	public SoundManager soundManager;
	private CharacterController characterController;
	private Animator animator;

	[SerializeField] AudioSource deathSound;
	[SerializeField] private float moveSpeed = 4f;
	[SerializeField] private float rotationSpeed = 10f;
	[SerializeField] private float gravity = 20f;
	[SerializeField] private float minDistance = 1f;
	[SerializeField] private float maxDistance = 100f;
	[SerializeField] private float jumpForce = 8f;


	private UnityEngine.Vector3 movement = Vector3.zero;
	private bool gameOver = false;

	private void Start()
	{
		gameOverWindow.SetActive(false);

		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (!gameOver)
		{
			Vector3 direction = targetObject.position - transform.position;
			transform.LookAt(targetObject);

			// Rotate towards the player
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
			transform.rotation *= Quaternion.Euler(0f, 180f, 0f);

			float distance = Vector3.Distance(transform.position, targetObject.position);

			if (distance > minDistance && distance < maxDistance)
			{
				RaycastHit hit;
        		float barrierRaycastDistance = 2f;

				// Move towards the player
				movement = -transform.forward * moveSpeed * Time.deltaTime;

				// Jump over barriers
				if (Physics.Raycast(transform.position, -transform.forward, out hit, barrierRaycastDistance))
				{
					if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Barrier") || hit.collider.CompareTag("Barrier"))
					{
						// UnityEngine.Debug.DrawRay(transform.position, -transform.forward * barrierRaycastDistance, Color.red);
						movement.y = jumpForce;
					}
				}
				
				movement.y -= gravity * Time.deltaTime;
				characterController.Move(movement);

				// Play animation
				animator.SetBool("isRunning", true);
			}
			else
			{
				// Stop moving and play idle animation
				animator.SetBool("isRunning", false);
			}

		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && !gameOver)
		{
			other.gameObject.GetComponent<PlayerMovement2>().enabled = false;
			Die();
		}
	}
	private void Die()
	{
		if (soundManager.muted == false)
		{
			deathSound.Play();
		}
		gameOverWindow.SetActive(true);
		gameOver = true;
	}
}