using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float smoothTime = 0.1f;
	[SerializeField] private Transform cam;
	[SerializeField] private float jumpSpeed = 8.0f;
	[SerializeField] private float gravity = 20.0f;
	[SerializeField] private AudioClip[] walkSound;
	private CharacterController characterController;
	private float turnSmoothVelocity;
	private Animator animator;
	private float remainingJumpSpeed;
	private bool isJumping = false;
	private bool isWalking = false;
	private AudioSource audioSource;

	private void Start()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
		{
			isJumping = true;
			remainingJumpSpeed = jumpSpeed;
			animator.SetBool("isJumping", true);
		}

		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

		if (direction.magnitude >= 0.1f)
		{
			if (!isWalking)
			{
				StopAllCoroutines();

				StartCoroutine(PlayWalkSounds());
			}
			isWalking = true;
			animator.SetBool("isWalking", true);
			Vector3 moveDirection = GetDirection(direction);
			moveDirection *= speed;
			characterController.Move(moveDirection * Time.deltaTime);
		}
		else
		{
			audioSource.enabled = false;
			isWalking = false;
			audioSource.Stop();
			animator.SetBool("isWalking", false);
		}

		// не трогай работает на костылях
		if (isJumping)
		{
			characterController.Move(new Vector3(0, jumpSpeed * Time.deltaTime * 3, 0));
			remainingJumpSpeed -= jumpSpeed * Time.deltaTime * 7; 
			if (remainingJumpSpeed <= 0)
			{
				isJumping = false;
				animator.SetBool("isJumping", false);
			}
		}
		else
		{
			characterController.Move(new Vector3(0, -gravity * Time.deltaTime * 7, 0));
		}



	}
	private Vector3 GetDirection(Vector3 direction)
	{
		float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
		float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTime);
		transform.rotation = Quaternion.Euler(0, angle, 0);
		Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
		return moveDirection;
	}

	private IEnumerator PlayWalkSounds()
	{
		isWalking = true;
		while (isWalking)
		{
			audioSource.enabled = true;
			audioSource.clip = walkSound[Random.Range(0, walkSound.Length)];
			audioSource.Stop();
			audioSource.Play();
			yield return new WaitForSeconds(0.55f);
		}
		audioSource.enabled = false;
	}

}
