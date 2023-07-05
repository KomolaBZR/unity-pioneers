using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerMovementBackwarts : MonoBehaviour
{
	private CharacterController characterController;
	private Animator animator;

	[SerializeField] private float movementSpeed, rotationSpeed, jumpSpeed, gravity;
	private UnityEngine.Vector3 movementDirection = Vector3.zero;
	private bool playerGrounded;

	[SerializeField] private float accelerationDuration = 0.1f;
	private bool isAccelerating = false;

	void Start()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
        transform.Rotate(UnityEngine.Vector3.up * 180f);
	}

	void Update()
	{
		playerGrounded = characterController.isGrounded;

		//movement
		UnityEngine.Vector3 inputMovement = -1 * transform.forward * movementSpeed * Input.GetAxisRaw("Vertical");
		characterController.Move(inputMovement * Time.deltaTime);

		transform.Rotate(UnityEngine.Vector3.up * Input.GetAxisRaw("Horizontal") * rotationSpeed);

		//jumping
		if (Input.GetButton("Jump") && playerGrounded)
		{
			movementDirection.y = jumpSpeed;
		}
		movementDirection.y -= gravity * Time.deltaTime;
		characterController.Move(movementDirection * Time.deltaTime);

		//animations
		animator.SetBool("isRunning", Input.GetAxisRaw("Vertical") != 0);
		animator.SetBool("isJumping", !characterController.isGrounded);

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Coin"))
		{
			if (!isAccelerating)
			{
				StartCoroutine(AcceleratePlayer());
			}
		}
	}

	private IEnumerator AcceleratePlayer()
	{
		isAccelerating = true;
		float timer = 0f;

		while (timer < accelerationDuration)
		{
			characterController.Move(-1 * transform.forward * movementSpeed * 1.5f * Time.deltaTime);

			timer += Time.deltaTime;
			yield return null;
		}

		isAccelerating = false;
	}
}
