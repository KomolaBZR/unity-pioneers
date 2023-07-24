using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoxDefaultMovement : MonoBehaviour
{
	public Transform player;
	public GameObject gameOverWindow;
	public SoundManager soundManager;
	private CharacterController characterController;
	private Animator animator;
	[SerializeField] AudioSource deathSound;
	[SerializeField] private float moveSpeed = 4f;
	[SerializeField] private float rotationSpeed = 10f;
	[SerializeField] private float gravity = 20f;
	[SerializeField] private float minDistance = 1f;
	[SerializeField] private float maxDistance = 10f;
	[SerializeField] private float jumpForce = 8f;
	private bool gameOver = false;
	private UnityEngine.Vector3 movement = Vector3.zero;

	Vector3 startPositon;

	private void Start()
	{
		gameOverWindow.SetActive(false);

		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();

		startPositon = player.position;
	}

	private void Update()
	{
		Vector3 direction = player.position - transform.position;
		direction.y = 0f;

		if (!gameOver && startPositon != player.position)
		{
			// Rotate towards the player
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

			float distance = Vector3.Distance(transform.position, player.position);

			if (distance > minDistance && distance < maxDistance)
			{
				RaycastHit hit;
        		float barrierRaycastDistance = 2f;

				// Move towards the player
				movement = transform.forward * moveSpeed * Time.deltaTime;

				if (Physics.Raycast(transform.position, transform.forward, out hit, barrierRaycastDistance))
				{
					UnityEngine.Debug.Log(Physics.Raycast(transform.position, transform.forward, out hit, barrierRaycastDistance));
					UnityEngine.Debug.DrawRay(transform.position, transform.forward * barrierRaycastDistance, Color.red);
					UnityEngine.Debug.Log(hit.collider.gameObject.layer);
					UnityEngine.Debug.Log(LayerMask.NameToLayer("Barrier"));
					UnityEngine.Debug.Log("Tag matches to Berrier: " + hit.collider.CompareTag("Barrier"));

					// if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Barrier"))
					if(hit.collider.CompareTag("Barrier"))
					{
						UnityEngine.Debug.Log("in tag barrier..");

						UnityEngine.Debug.DrawRay(transform.position, transform.forward * barrierRaycastDistance, Color.red);
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
			other.gameObject.GetComponent<PlayerMovementBackwarts>().enabled = false;
			Die();
		}
	}

	private void Die()
	{
		if(soundManager.muted == false)
		{
			deathSound.Play();
		}
		gameOverWindow.SetActive(true);
		gameOver = true;
	}

}
